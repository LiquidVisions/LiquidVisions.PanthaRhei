using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.ModelInitializers
{
    public interface IInitializeExpandersUseCase
    {
        void Initialize(App app);

        void DeleteAll();
    }
}
