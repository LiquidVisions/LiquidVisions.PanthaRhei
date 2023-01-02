using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NS.Domain;

namespace NS.Client
{
    public interface IClient<TEntity>
        where TEntity : class, IEntity
    {
        Task<IEnumerable<TEntity>> GetAll();

        TEntity GetById(Guid id);
    }
}
