namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators.Harvesters
{
    public interface IHarvestSerializerInteractor
    {
        void Deserialize(Harvest harvest, string fullPath);
    }
}
