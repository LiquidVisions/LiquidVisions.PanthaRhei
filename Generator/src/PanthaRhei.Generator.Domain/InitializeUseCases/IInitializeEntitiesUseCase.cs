namespace LiquidVisions.PanthaRhei.Generator.Domain.DataInitializers
{
    internal interface IInitializeEntitiesUseCase
    {
        void Initialize();

        void DeleteAll();
    }
}
