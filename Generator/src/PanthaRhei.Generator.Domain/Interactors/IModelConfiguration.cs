using System;

namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors
{
    public interface IModelConfiguration
    {
        string[] GetIndexes(Type entityType);

        public string[] GetKeys(Type entityType);
    }
}
