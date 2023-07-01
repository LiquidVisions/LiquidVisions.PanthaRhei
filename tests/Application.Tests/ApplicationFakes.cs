using LiquidVisions.PanthaRhei.Application.Usecases.Generators;
using LiquidVisions.PanthaRhei.Application.Usecases.Initializers;
using LiquidVisions.PanthaRhei.Tests;
using Moq;

namespace LiquidVisions.PanthaRhei.Application.Tests
{
    public class ApplicationFakes : Fakes
    {
        public Mock<IExpanderPluginLoader> IExpanderPluginLoader { get; } = new();

        public Mock<ICodeGeneratorBuilder> ICodeGeneratorBuilder { get; } = new();

        public Mock<ICodeGenerator> ICodeGenerator { get; } = new();

        internal Mock<IAssemblyContext> IAssemblyContext { get; } = new();

        internal Mock<IObjectActivator> IObjectActivator { get; } = new();

        public override void Configure()
        {
            base.Configure();

            ICodeGeneratorBuilder.Setup(x => x.Build()).Returns(ICodeGenerator.Object);
        }

        public override void ConfigureIDependencyFactory()
        {
            base.ConfigureIDependencyFactory();

            IDependencyFactory.Setup(x => x.Get<IAssemblyContext>()).Returns(IAssemblyContext.Object);
            IDependencyFactory.Setup(x => x.Get<IObjectActivator>()).Returns(IObjectActivator.Object);
            IDependencyFactory.Setup(x => x.Get<ICodeGenerator>()).Returns(ICodeGenerator.Object);
            IDependencyFactory.Setup(x => x.Get<ICodeGeneratorBuilder>()).Returns(ICodeGeneratorBuilder.Object);
            IDependencyFactory.Setup(x => x.Get<IExpanderPluginLoader>()).Returns(IExpanderPluginLoader.Object);
        }
    }
}
