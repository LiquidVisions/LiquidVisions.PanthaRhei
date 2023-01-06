using System.Linq;
using System.Text.RegularExpressions;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Harvesters
{
    /// <summary>
    /// An abstract implementation of the <see cref="IHarvesterInteractor{TExpander}"/>.
    /// </summary>
    /// <typeparam name="TExpander">A deriveded type of <see cref="IExpanderInteractor"/>.</typeparam>
    internal sealed class RegionHarvesterInteractor<TExpander> : HarvesterInteractor<TExpander>
        where TExpander : class, IExpanderInteractor
    {
        private readonly string regexPattern = @"#region ns-custom-(?'tag'.*)(?'content'(?s).*?)#endregion ns-custom-(?'tag'.*)";
        private readonly Parameters parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionHarvesterInteractor{TExpander}"/> class.
        /// </summary>
        /// <param name="dependencyProvider"><seealso cref="IDependencyFactoryInteractor"/></param>
        public RegionHarvesterInteractor(IDependencyFactoryInteractor dependencyProvider)
           : base(dependencyProvider)
        {
            parameters = dependencyProvider.Get<Parameters>();
        }

        /// <inheritdoc/>
        public override bool CanExecute => !parameters.GenerationMode.HasFlag(GenerationModes.Deploy) && Directory.Exists(parameters.OutputFolder);

        /// <inheritdoc/>
        protected override string Extension => Resources.RegionHarvesterExtensionFile;

        /// <inheritdoc/>
        public override void Execute()
        {
            string[] files = Directory.GetFiles(parameters.OutputFolder, "*.cs", System.IO.SearchOption.AllDirectories);

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
            string fileText = File.ReadAllText(file);

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
