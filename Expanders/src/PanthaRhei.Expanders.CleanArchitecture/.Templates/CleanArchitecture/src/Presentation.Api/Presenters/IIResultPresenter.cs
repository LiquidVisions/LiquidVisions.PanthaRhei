using NS.Application;
using Microsoft.AspNetCore.Http;

namespace NS.Presentation.Api.Presenters
{
    public interface IIResultPresenter : IPresenter
    {
        IResult GetResult(HttpRequest request = null);
    }
}
