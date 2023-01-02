using NS.Application;
using Microsoft.AspNetCore.Http;

namespace NS.Infrastructure.Api.Presenters
{
    public interface IIResultPresenter : IPresenter
    {
        IResult GetResult(HttpRequest request = null);
    }
}
