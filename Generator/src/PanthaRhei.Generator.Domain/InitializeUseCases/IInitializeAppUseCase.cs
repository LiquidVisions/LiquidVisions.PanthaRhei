using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.ModelInitializers
{
    internal interface IInitializeAppUseCase
    {
        App Initialize();

        void DeleteAll();
    }
}
