namespace LiquidVisions.PanthaRhei.Generator.Tests
{
    public abstract class AbstractTests
    {
        private readonly Fakes fakes = new();

        protected AbstractTests() { }

        protected Fakes Fakes => fakes;
    }
}
