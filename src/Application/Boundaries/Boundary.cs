using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Application.RequestModels;
using LiquidVisions.PanthaRhei.Domain;
using LiquidVisions.PanthaRhei.Domain.Usecases.NewExpanderUseCase;

namespace LiquidVisions.PanthaRhei.Application.Boundaries
{
    /// <summary>
    /// Represents the boundary of the application.
    /// </summary>
    /// <param name="useCase"><seealso cref="NewExpanderRequestModel"/></param>
    internal class Boundary(INewExpanderUseCase useCase) : IBoundary
    {
        private readonly INewExpanderUseCase useCase = useCase;

        public async Task<Response> CreateNewExpander(NewExpanderRequestModel model)
        {
            NewExpander dto = new()
            {
                ShortName = model.ShortName,
                FullName = model.fullName,
                Path = model.Path,
                BuildPath = model.BuildPath,
                Build = model.Build,
            };

            Response response = await useCase.Execute(dto)
                .ConfigureAwait(false);

            return response;
        }
    }
}
