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
    }
}
