namespace LiquidVisions.PanthaRhei.Generator.Domain.DataInitializers
{
    internal interface IInitializeDataTypesUseCase
    {
        void Initialize();

        void DeleteAll();
    }
}
