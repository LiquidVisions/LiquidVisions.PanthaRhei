using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.InitializeUseCases
{
    internal interface IInitializeHandlerUseCase
    {
        void Initialize(App app);

        void DeleteAll();
    }
}
