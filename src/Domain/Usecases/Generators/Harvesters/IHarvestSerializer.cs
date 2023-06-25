namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters
{
    public interface IHarvestSerializer
    {
        void Serialize(Harvest harvest, string fullPath);
    }
}
