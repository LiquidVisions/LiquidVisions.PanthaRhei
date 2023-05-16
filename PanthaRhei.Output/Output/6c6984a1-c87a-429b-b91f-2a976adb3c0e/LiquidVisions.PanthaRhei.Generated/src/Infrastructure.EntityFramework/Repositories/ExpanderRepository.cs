using System;
using System.Linq;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LiquidVisions.PanthaRhei.Generated.Infrastructure.EntityFramework
{
    public class ExpanderRepository : IGetGateway<Expander>, IGetByIdGateway<Expander>, ICreateGateway<Expander>, IDeleteGateway<Expander>, IUpdateGateway<Expander>
    {
        private readonly Context context;

        public ExpanderRepository(Context context)
        {
            this.context = context;
        }

        public async Task<int> Update(Expander entity)
        {
            
            context.Set<Expander>().Add(entity);
            context.Entry(entity).State = EntityState.Modified;
            #region ns-custom-update
            #endregion ns-custom-update
            return await context.SaveChangesAsync();
            
        }

        public async Task<int> Create(Expander entity)
        {
            context.Set<Expander>().Add(entity);
            context.Entry(entity).State = EntityState.Added;

            #region ns-custom-create
            #endregion ns-custom-create

            return await context.SaveChangesAsync();
            
        }

        public IQueryable<Expander> Get()
        {
            IQueryable<Expander> result = context.Set<Expander>();

            #region ns-custom-get
            #endregion ns-custom-get

            return result;
        }

        public Expander GetById(Guid id)
        {
            
            Expander result = context.Set<Expander>()
                .SingleOrDefault(x => x.Id == id);

            #region ns-custom-getbyid
            #endregion ns-custom-getbyid

            return result;
        }

        public async Task<bool> Delete(Expander entity)
        {
            context.Set<Expander>().Remove(entity);
            context.Entry(entity).State = EntityState.Deleted;

            #region ns-custom-delete
            #endregion ns-custom-delete

            return await context.SaveChangesAsync() == 1;
        }

        public bool Exists(Guid id)
        {
            bool result = context.Set<Expander>().Any(x => x.Id == id);

            #region ns-custom-exists
            #endregion ns-custom-exists

            return result;
        }
    }
}
