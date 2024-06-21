using System.Threading.Tasks;

namespace LiquidVisions.PanthaRhei.Domain.Usecases.UpdateCore
{
    /// <summary>
    /// Represents the use case for updating the core.
    /// </summary>
    public interface IUpdateCoreUseCase
    {
        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <returns><seealso cref="Response"/></returns>
        Task<Response> Update();
    }
}
