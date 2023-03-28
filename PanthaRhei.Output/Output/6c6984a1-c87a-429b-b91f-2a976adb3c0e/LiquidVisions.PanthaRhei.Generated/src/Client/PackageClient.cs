using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using RestSharp;
using RestSharp.Serializers.Json;

namespace LiquidVisions.PanthaRhei.Generated.Client
{
    public class PackageClient : IClient<Package>
    {
        public async Task<IEnumerable<Package>> GetAll()
        {
            #region ns-custom-get
            throw new NotImplementedException();
            #endregion ns-custom-get
        }

        public Package GetById(Guid id)
        {
            #region ns-custom-getbyid
            throw new NotImplementedException();
            #endregion ns-custom-getbyid
        }
    }
}