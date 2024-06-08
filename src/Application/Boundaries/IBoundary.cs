using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Application.RequestModels;
using LiquidVisions.PanthaRhei.Domain;

namespace LiquidVisions.PanthaRhei.Application.Boundaries
{
    /// <summary>
    /// Represents the contract for the boundary of the application.
    /// </summary>
    public interface IBoundary
    {
        /// <summary>
        /// Creates a new expander.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Response> CreateNewExpander(NewExpanderRequestModel model);

        /// <summary>
        /// Updates the PanthaRhei.Core packages in the project.
        /// </summary>
        /// <param name="root">The root of where the action should search for projects to update.</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<Response> UpdateCorePackages(string root);
    }
}
