using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters
{
    /// <summary>
    /// An abstract implementation of the <see cref="IHarvester{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpander"/>.</typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RegionHarvester{TExpander}"/> class.
    /// </remarks>
    /// <param name="dependencyProvider"><seealso cref="IDependencyFactory"/></param>
    internal sealed class RegionHarvester<TExpander>(IDependencyFactory dependencyProvider) : IHarvester<TExpander>
        where TExpander : class, IExpander
    {
        private readonly string _regexPattern = @"#region ns-custom-(?'tag'.*)(?'content'(?s).*?)#endregion ns-custom-(?'tag'.*)";
        private readonly GenerationOptions _options = dependencyProvider.Resolve<GenerationOptions>();
        private readonly IDirectory _directory = dependencyProvider.Resolve<IDirectory>();
        private readonly IFile _file = dependencyProvider.Resolve<IFile>();
        private readonly ICreateRepository<Harvest> _gateway = dependencyProvider.Resolve<ICreateRepository<Harvest>>();

        /// <inheritdoc/>
        public bool Enabled => _options.Modes.HasFlag(GenerationModes.Harvest)
            && _directory.Exists(_options.OutputFolder);

        public TExpander Expander { get; } = dependencyProvider.Resolve<TExpander>();

        /// <inheritdoc/>
        public void Execute()
        {
            string[] filePaths = _directory.GetFiles(_options.OutputFolder, "*.cs", SearchOption.AllDirectories);

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
            string fileText = _file.ReadAllText(pathToFile);

            MatchCollection result = Regex.Matches(fileText, _regexPattern, RegexOptions.Multiline);
            if (result.Count != 0)
            {
                Harvest harvest = new(Resources.RegionHarvesterExtensionFile)
                {
                    Path = pathToFile,
                };

                foreach (Match match in result.Cast<Match>())
                {
                    HandleMatch(harvest, match);
                }

                _gateway.Create(harvest);
            }
        }
    }
}
