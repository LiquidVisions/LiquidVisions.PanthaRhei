using LiquidVisions.PanthaRhei.Generator.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generator.Domain.InitializeUseCases
{
    internal interface IInitializeAppUseCase
    {
        App Initialize();

        void DeleteAll();
    }
}
