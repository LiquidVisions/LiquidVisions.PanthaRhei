namespace LiquidVisions.PanthaRhei.Generator.Domain.ModelInitializers
{
    internal interface IInitializeHandlerUseCase
    {
        void Initialize();

        void DeleteAll();
    }
}
