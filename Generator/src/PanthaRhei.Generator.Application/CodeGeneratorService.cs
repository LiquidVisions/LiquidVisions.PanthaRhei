using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.UseCases;

namespace LiquidVisions.PanthaRhei.Generator.Application
{
    /// <summary>
    /// An implemented type of the contract <seealso cref="ICodeGeneratorService"/>.
    /// </summary>
    internal class CodeGeneratorService : ICodeGeneratorService
    {
        private readonly ICodeGeneratorBuilder builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGeneratorService"/> class.
        /// </summary>
        /// <param name="factory"><seealso cref="IDependencyResolver"/>.</param>
        public CodeGeneratorService(IDependencyResolver factory)
        {
            builder = factory.Get<ICodeGeneratorBuilder>();
        }

        /// <inheritdoc/>
        public void Execute()
        {
            ICodeGenerator codeGenerator = builder.Build();

            codeGenerator.Execute();
        }
    }
}
