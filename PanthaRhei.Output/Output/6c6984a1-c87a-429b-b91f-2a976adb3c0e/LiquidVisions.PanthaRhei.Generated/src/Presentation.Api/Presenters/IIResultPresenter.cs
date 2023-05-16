using LiquidVisions.PanthaRhei.Generated.Application;
using Microsoft.AspNetCore.Http;

namespace LiquidVisions.PanthaRhei.Generated.Presentation.Api.Presenters
{
    public interface IIResultPresenter : IPresenter
    {
        IResult GetResult(HttpRequest request = null);
    }
}
