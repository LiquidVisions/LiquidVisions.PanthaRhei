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
    public class EntityClient : IClient<Entity>
    {
        public async Task<IEnumerable<Entity>> GetAll()
        {
            #region ns-custom-get
            throw new NotImplementedException();
            #endregion ns-custom-get
        }

        public Entity GetById(Guid id)
        {
            #region ns-custom-getbyid
            throw new NotImplementedException();
            #endregion ns-custom-getbyid
        }
    }
}