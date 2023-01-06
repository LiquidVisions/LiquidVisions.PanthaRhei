using LiquidVisions.PanthaRhei.Generator.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generator.Domain.InitializeUseCases
{
    internal interface IInitializeEntitiesUseCase
    {
        void Initialize(App app);

        void DeleteAll();
    }
}
