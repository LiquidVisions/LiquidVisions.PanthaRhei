using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Domain;

namespace LiquidVisions.PanthaRhei.Generated.Client
{
    public interface IClient<TEntity>
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();

        TEntity GetById(Guid id);
    }
}
