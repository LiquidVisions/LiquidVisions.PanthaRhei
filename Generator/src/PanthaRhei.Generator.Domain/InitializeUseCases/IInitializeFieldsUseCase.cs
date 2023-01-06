using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Domain.InitializeUseCases
{
    internal interface IInitializeFieldsUseCase
    {
        void Initialize(App app);

        void DeleteAll();
    }

}
