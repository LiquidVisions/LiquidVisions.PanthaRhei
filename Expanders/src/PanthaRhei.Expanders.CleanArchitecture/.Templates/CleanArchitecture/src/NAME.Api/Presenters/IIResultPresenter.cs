using NS.Application;
using Microsoft.AspNetCore.Http;

namespace NS.Api.Presenters
{
    public interface IIResultPresenter : IPresenter
    {
        IResult GetResult(HttpRequest request = null);
    }
}
