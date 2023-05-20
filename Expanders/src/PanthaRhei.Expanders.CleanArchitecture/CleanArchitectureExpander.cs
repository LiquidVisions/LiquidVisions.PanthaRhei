using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Expanders;
using LiquidVisions.PanthaRhei.Generator.Domain.IO;

namespace LiquidVisions.PanthaRhei.Expanders.CleanArchitecture
{
    /// <summary>
    /// The <seealso cref="CleanArchitectureExpander"/> expanders.
    /// </summary>
    public class CleanArchitectureExpander : AbstractExpander<CleanArchitectureExpander>
    {
        private readonly ICommandLineInteractor commandLine;
        private readonly GenerationOptions options;
        private readonly IDirectory directory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CleanArchitectureExpander"/> class.
        /// </summary>
        public CleanArchitectureExpander()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CleanArchitectureExpander"/> class.
        /// </summary>
        /// <param name="dependencyFactory"><seealso cref="IDependencyFactoryInteractor"/></param>
        public CleanArchitectureExpander(IDependencyFactoryInteractor dependencyFactory)
            : base(dependencyFactory)
        {
            commandLine = dependencyFactory.Get<ICommandLineInteractor>();
            options = dependencyFactory.Get<GenerationOptions>();
            directory = dependencyFactory.Get<IDirectory>();
        }

        public override void Clean()
        {
            IEnumerable<string> paths = GetComponentPaths(Resources.Api, Resources.EntityFramework);
            foreach (string path in paths)
            {
                if(directory.Exists(path))
                {
                    try
                    {
                        App.ConnectionStrings
                            .ToList()
                            .ForEach(x => commandLine.Start($"dotnet user-secrets clear", path));
                    }
                    catch(Exception e)
                    {
                        Logger.Fatal("Failed to remove secret, but still continuing: ", e.Message);
                    }
                }
            }
        }

        internal virtual string GetComponentOutputFolder(Component component)
        {
            return Path.Combine(options.OutputFolder, App.FullName, "src", $"{component.Name}");
        }

        internal virtual IEnumerable<string> GetComponentPaths(params string[] componentNames)
        {
            foreach (string componentName in componentNames)
            {
                Component components = GetComponentByName(componentName);
                yield return this.GetComponentOutputFolder(components);
            }
        }

        internal virtual Component GetComponentByName(string name) => Model
                .Components
                .Single(x => x.Name == name);

        internal virtual string GetComponentProjectFile(Component component)
        {
            return Path.Combine(GetComponentOutputFolder(component), $"{component.Name}.csproj");
        }

        protected override int GetOrder() => 1;
    }
}