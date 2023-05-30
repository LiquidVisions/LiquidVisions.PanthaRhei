using System;
using System.Linq;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LiquidVisions.PanthaRhei.Generated.Infrastructure.EntityFramework
{
    public class ConnectionStringRepository : IGetGateway<ConnectionString>, IGetByIdGateway<ConnectionString>, ICreateGateway<ConnectionString>, IDeleteGateway<ConnectionString>, IUpdateGateway<ConnectionString>
    {
        private readonly Context context;

        public ConnectionStringRepository(Context context)
        {
            this.context = context;
        }

        public async Task<int> Update(ConnectionString entity)
        {
            context.Set<ConnectionString>().Add(entity);
            context.Entry(entity).State = EntityState.Modified;
            #region ns-custom-update
            #endregion ns-custom-update
            return await context.SaveChangesAsync();
            
        }

        public async Task<int> Create(ConnectionString entity)
        {
            entity.App = context.Apps.Single(x => x.Id == entity.App.Id);
            
            context.Set<ConnectionString>().Add(entity);
            context.Entry(entity).State = EntityState.Added;

            #region ns-custom-create
            #endregion ns-custom-create

            try
            {
                int result = await context.SaveChangesAsync();

                return result;
            }
            catch(Exception)
            {
                throw;
            }
            
            
        }

        public IQueryable<ConnectionString> Get()
        {
            IQueryable<ConnectionString> result = context.Set<ConnectionString>();

            #region ns-custom-get
            #endregion ns-custom-get

            return result;
        }

        public ConnectionString GetById(Guid id)
        {
            
            ConnectionString result = context.Set<ConnectionString>()
                .SingleOrDefault(x => x.Id == id);

            #region ns-custom-getbyid
            #endregion ns-custom-getbyid

            return result;
        }

        public async Task<bool> Delete(ConnectionString entity)
        {
            context.Set<ConnectionString>().Remove(entity);
            context.Entry(entity).State = EntityState.Deleted;

            #region ns-custom-delete
            #endregion ns-custom-delete

            return await context.SaveChangesAsync() == 1;
        }

        public bool Exists(Guid id)
        {
            bool result = context.Set<ConnectionString>().Any(x => x.Id == id);

            #region ns-custom-exists
            #endregion ns-custom-exists

            return result;
        }
    }
}
