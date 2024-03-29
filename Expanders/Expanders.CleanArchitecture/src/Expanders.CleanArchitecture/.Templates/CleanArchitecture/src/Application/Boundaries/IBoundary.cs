﻿using System.Threading.Tasks;
using NS.Application.RequestModels;

namespace NS.Application.Boundaries
{
    public interface IBoundary<TRequestModel>
        where TRequestModel : RequestModel, new()
    {
        Task Execute(TRequestModel requestModel, IPresenter presenter);
    }
}
