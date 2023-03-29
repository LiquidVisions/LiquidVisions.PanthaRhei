namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Harvesters
{
    public interface IHarvestSerializerInteractor
    {
        void Serialize(Harvest harvest, string fullPath);
    }
}
