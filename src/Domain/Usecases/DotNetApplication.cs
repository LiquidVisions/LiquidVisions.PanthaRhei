﻿using System.IO;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;

namespace LiquidVisions.PanthaRhei.Domain.Usecases
{
    internal class DotNetApplication(IDependencyFactory dependencyFactory) : IApplication
    {
        private readonly GenerationOptions _options = dependencyFactory.Resolve<GenerationOptions>();
        private readonly ILogger _logger = dependencyFactory.Resolve<ILogger>();
        private readonly IDirectory _directory = dependencyFactory.Resolve<IDirectory>();
        private readonly IFile _file = dependencyFactory.Resolve<IFile>();
        private readonly ICommandLine _cli = dependencyFactory.Resolve<ICommandLine>();
        private readonly App _app = dependencyFactory.Resolve<App>();

        public void MaterializeProject()
        {
            string root = Path.Combine(_options.OutputFolder, _app.FullName);

            CreateFolderIfNeeded(root);

            _cli.Start($"dotnet new liquidvisions-expanders-{_app.Name} --NAME {_app.Name} --NS {_app.FullName}", root);
        }

        public void MaterializeComponent(Component component)
        {
            string solutionRoot = Path.Combine(_options.OutputFolder, _app.FullName);
            string componentRoot = GetComponentRoot(component);

            CreateFolderIfNeeded(solutionRoot);
            CreateFolderIfNeeded(componentRoot);

            if (!_file.Exists(Path.Combine(solutionRoot, $"{_app.FullName}.sln")))
            {
                _cli.Start($"dotnet new sln", solutionRoot);
            }

            _cli.Start($"dotnet new liquidvisions-expanders-{component.Name} --NAME {component.Name} --NS {_app.FullName}", componentRoot);
            _cli.Start($"dotnet sln {Path.Combine(solutionRoot, $"{_app.FullName}.sln")} add {GetComponentConfigurationFile(component)}");
        }

        public virtual string GetComponentRoot(Component component)
        {
            return Path.Combine(_options.OutputFolder, _app.FullName, "src", $"{component.Name}");
        }

        public virtual string GetComponentConfigurationFile(Component component)
        {
            return Path.Combine(GetComponentRoot(component), $"{component.Name}.csproj");
        }

        public void AddPackages(Component component)
        {
            throw new System.NotImplementedException();
        }

        private void CreateFolderIfNeeded(string root)
        {
            if (!_directory.Exists(root))
            {
                _logger.Info($"Creating directory {root}");
                _cli.Start($"mkdir {root}");
            }
        }
    }
}
