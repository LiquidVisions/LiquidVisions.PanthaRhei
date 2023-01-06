using System.Linq;
using System.Text.RegularExpressions;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Harvesters
{
    /// <summary>
    /// An abstract implementation of the <see cref="IHarvester{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpander"/>.</typeparam>
    public sealed class RegionHarvester<TExpander> : Harvester<TExpander>
        where TExpander : class, IExpander
    {
        private readonly string regexPattern = @"#region ns-custom-(?'tag'.*)(?'content'(?s).*?)#endregion ns-custom-(?'tag'.*)";
        private readonly Parameters parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionHarvester{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyProvider"><seealso cref="IDependencyResolver"/></param>
        public RegionHarvester(IDependencyResolver dependencyProvider)
           : base(dependencyProvider)
        {
            parameters = dependencyProvider.Get<Parameters>();
        }

        /// <inheritdoc/>
        public override bool CanExecute => !parameters.GenerationMode.HasFlag(GenerationModes.Deploy) && DirectoryService.Exists(parameters.OutputFolder);

        /// <inheritdoc/>
        protected override string Extension => Resources.RegionHarvesterExtensionFile;

        /// <inheritdoc/>
        public override void Execute()
        {
            string[] files = DirectoryService.GetFiles(parameters.OutputFolder, "*.cs", System.IO.SearchOption.AllDirectories);

            ExecuteAllFiles(files);
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

        private void ExecuteAllFiles(string[] files)
        {
            foreach (string file in files)
            {
                ExecuteFile(file);
            }
        }

        private void ExecuteFile(string file)
        {
            string fileText = FileService.ReadAllText(file);

            MatchCollection result = Regex.Matches(fileText, regexPattern, RegexOptions.Multiline);
            if (result.Any())
            {
                Harvest harvest = new()
                {
                    Path = file,
                };

                foreach (Match match in result.Cast<Match>())
                {
                    HandleMatch(harvest, match);
                }

                DeserializeHarvestModelToFile(harvest, file);
            }
        }
    }
}
