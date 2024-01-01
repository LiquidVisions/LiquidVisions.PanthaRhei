using System;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators;
using LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Expanders;

namespace LiquidVisions.PanthaRhei.Domain.Tests.Mocks
{
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public class FakeExpander : IExpander
    {
        public bool Enabled => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public int Order => throw new NotImplementedException();

        public virtual Expander Model => throw new NotImplementedException();

        public void Clean() => throw new NotImplementedException();

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public void Expand() => throw new NotImplementedException();
        public void Harvest() => throw new NotImplementedException();
        public void PostProcess() => throw new NotImplementedException();
        public void PreProcess() => throw new NotImplementedException();
        public void Rejuvenate() => throw new NotImplementedException();
    }

    public class FakeExpanderDependencyManager(Expander expander, IDependencyManager dependencyManager) : AbstractExpanderDependencyManager<FakeExpander>(expander, dependencyManager)
    {
    }

    public abstract class FakeExpanderTask : IExpanderTask<FakeExpander>
    {
        public string Name => throw new NotImplementedException();

        public int Order => throw new NotImplementedException();

        public FakeExpander Expander => throw new NotImplementedException();

        public bool Enabled => throw new NotImplementedException();

        public void Execute() => throw new NotImplementedException();
    }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
}


