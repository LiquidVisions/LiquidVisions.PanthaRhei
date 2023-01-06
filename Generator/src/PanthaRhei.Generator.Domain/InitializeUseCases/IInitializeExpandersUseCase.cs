using LiquidVisions.PanthaRhei.Generator.Domain.Entities;

namespace LiquidVisions.PanthaRhei.Generator.Domain.InitializeUseCases
{
    public interface IInitializeExpandersUseCase
    {
        void Initialize(App app);

        void DeleteAll();
    }
}
