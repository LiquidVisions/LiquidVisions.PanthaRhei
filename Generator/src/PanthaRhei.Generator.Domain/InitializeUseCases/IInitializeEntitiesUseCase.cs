using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.InitializeUseCases
{
    internal interface IInitializeEntitiesUseCase
    {
        void Initialize(App app);

        void DeleteAll();
    }
}
