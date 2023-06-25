namespace LiquidVisions.PanthaRhei.Domain.Usecases.Generators.Harvesters
{
    public interface IHarvestSerializerInteractor
    {
        void Serialize(Harvest harvest, string fullPath);
    }
}
