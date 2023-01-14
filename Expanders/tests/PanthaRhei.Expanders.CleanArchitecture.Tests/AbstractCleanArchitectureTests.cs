namespace LiquidVisions.PanthaRhei.Generator.CleanArchitecture.Tests
{

    public abstract class AbstractCleanArchitectureTests
    {
        private readonly CleanArchitectureFakes fakes = new();

        protected AbstractCleanArchitectureTests() { }

        protected CleanArchitectureFakes Fakes => fakes;
    }

}
