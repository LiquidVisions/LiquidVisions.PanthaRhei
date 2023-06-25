using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Gateways;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Expanders;
using LiquidVisions.PanthaRhei.Domain.IO;

namespace LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Harvesters
{
    /// <summary>
    /// An abstract implementation of the <see cref="IHarvesterInteractor{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpanderInteractor"/>.</typeparam>
    internal sealed class RegionHarvesterInteractor<TExpander> : IHarvesterInteractor<TExpander>
        where TExpander : class, IExpanderInteractor
    {
        private readonly string regexPattern = @"#region ns-custom-(?'tag'.*)(?'content'(?s).*?)#endregion ns-custom-(?'tag'.*)";
        private readonly GenerationOptions options;
        private readonly IDirectory directory;
        private readonly IFile file;
        private readonly TExpander expander;
        private readonly ICreateGateway<Harvest> gateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionHarvesterInteractor{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyProvider"><seealso cref="IDependencyFactory"/></param>
        public RegionHarvesterInteractor(IDependencyFactory dependencyProvider)
        {
            options = dependencyProvider.Get<GenerationOptions>();
            directory = dependencyProvider.Get<IDirectory>();
            file = dependencyProvider.Get<IFile>();
            expander = dependencyProvider.Get<TExpander>();
            gateway = dependencyProvider.Get<ICreateGateway<Harvest>>();
        }

        /// <inheritdoc/>
        public bool CanExecute => options.Modes.HasFlag(GenerationModes.Harvest)
            && directory.Exists(options.OutputFolder);

        public TExpander Expander => expander;

        /// <inheritdoc/>
        public void Execute()
        {
            string[] filePaths = directory.GetFiles(options.OutputFolder, "*.cs", SearchOption.AllDirectories);

            ExecuteAllFiles(filePaths);
        }

        private static void HandleMatch(Harvest harvest, Match match)
        {
            if (match.Success && !string.IsNullOrEmpty(match.Value))
            {
                string content = match.Groups["content"].Value;
                if (HasContent(content))
                {
                    harvest.Items.Add(new HarvestItem
                    {
                        Content = content,
                        Tag = match.Groups["tag"].Value,
                    });
                }
            }
        }

        private static bool HasContent(string content)
        {
            string str = content.Trim();
            return !string.IsNullOrWhiteSpace(str.Trim());
        }

        private void ExecuteAllFiles(string[] pathToFiles)
        {
            foreach (string filePath in pathToFiles)
            {
                ExecuteFile(filePath);
            }
        }

        private void ExecuteFile(string pathToFile)
        {
            string fileText = file.ReadAllText(pathToFile);

            MatchCollection result = Regex.Matches(fileText, regexPattern, RegexOptions.Multiline);
            if (result.Any())
            {
                Harvest harvest = new(Resources.RegionHarvesterExtensionFile)
                {
                    Path = pathToFile,
                };

                foreach (Match match in result.Cast<Match>())
                {
                    HandleMatch(harvest, match);
                }

                gateway.Create(harvest);
            }
        }
    }
}
