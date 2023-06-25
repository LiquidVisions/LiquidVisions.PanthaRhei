namespace LiquidVisions.PanthaRhei.Domain.Interactors.Generators.Harvesters
{
    public interface IHarvestSerializerInteractor
    {
        void Serialize(Harvest harvest, string fullPath);
    }
}
