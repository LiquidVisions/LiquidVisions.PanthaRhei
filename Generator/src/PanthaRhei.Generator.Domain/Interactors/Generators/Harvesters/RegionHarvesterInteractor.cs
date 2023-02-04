using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Harvesters
{
    /// <summary>
    /// An abstract implementation of the <see cref="IHarvesterInteractor{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpanderInteractor"/>.</typeparam>
    internal sealed class RegionHarvesterInteractor<TExpander> : IHarvesterInteractor<TExpander>
        where TExpander : class, IExpanderInteractor
    {
        private readonly string regexPattern = @"#region ns-custom-(?'tag'.*)(?'content'(?s).*?)#endregion ns-custom-(?'tag'.*)";
        private readonly Parameters parameters;
        private readonly IDirectory directory;
        private readonly IFile file;
        private readonly TExpander expander;
        private readonly IHarvestSerializerInteractor serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionHarvesterInteractor{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyProvider"><seealso cref="IDependencyFactoryInteractor"/></param>
        public RegionHarvesterInteractor(IDependencyFactoryInteractor dependencyProvider)
        {
            serializer = dependencyProvider.Get<IHarvestSerializerInteractor>();
            parameters = dependencyProvider.Get<Parameters>();
            directory = dependencyProvider.Get<IDirectory>();
            file = dependencyProvider.Get<IFile>();
            expander = dependencyProvider.Get<TExpander>();
        }

        /// <inheritdoc/>
        public bool CanExecute => !parameters.GenerationMode.HasFlag(GenerationModes.Deploy)
            && directory.Exists(parameters.OutputFolder);

        public TExpander Expander => expander;

        /// <inheritdoc/>
        public void Execute()
        {
            string[] filePaths = directory.GetFiles(parameters.OutputFolder, "*.cs", System.IO.SearchOption.AllDirectories);

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
                Harvest harvest = new()
                {
                    Path = pathToFile,
                };

                foreach (Match match in result.Cast<Match>())
                {
                    HandleMatch(harvest, match);
                }

                string fullPathToHarvestLocation = Path.Combine(
                    parameters.HarvestFolder,
                    Expander.Model.Name,
                    $"{file.GetFileNameWithoutExtension(pathToFile)}.{Resources.RegionHarvesterExtensionFile}");

                serializer.Deserialize(harvest, fullPathToHarvestLocation);
            }
        }
    }
}
