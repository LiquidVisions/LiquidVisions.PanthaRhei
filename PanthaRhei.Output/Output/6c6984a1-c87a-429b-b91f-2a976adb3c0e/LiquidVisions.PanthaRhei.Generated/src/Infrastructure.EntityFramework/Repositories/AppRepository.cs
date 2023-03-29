using System;
using System.Linq;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LiquidVisions.PanthaRhei.Generated.Infrastructure.EntityFramework
{
    public class AppRepository : IGetGateway<App>, IGetByIdGateway<App>, ICreateGateway<App>, IDeleteGateway<App>, IUpdateGateway<App>
    {
        private readonly Context context;

        public AppRepository(Context context)
        {
            this.context = context;
        }

        public async Task<int> Update(App entity)
        {
            
            context.Set<App>().Add(entity);
            context.Entry(entity).State = EntityState.Modified;
            #region ns-custom-update
            #endregion ns-custom-update
            return await context.SaveChangesAsync();
            
        }

        public async Task<int> Create(App entity)
        {
            context.Set<App>().Add(entity);
            context.Entry(entity).State = EntityState.Added;

            #region ns-custom-create
            #endregion ns-custom-create

            return await context.SaveChangesAsync();
            
        }

        public IQueryable<App> Get()
        {
            IQueryable<App> result = context.Set<App>();

            #region ns-custom-get
            #endregion ns-custom-get

            return result;
        }

        public App GetById(Guid id)
        {
            
            App result = context.Set<App>()
                .SingleOrDefault(x => x.Id == id);

            #region ns-custom-getbyid
            #endregion ns-custom-getbyid

            return result;
        }

        public async Task<bool> Delete(App entity)
        {
            context.Set<App>().Remove(entity);
            context.Entry(entity).State = EntityState.Deleted;

            #region ns-custom-delete
            #endregion ns-custom-delete

            return await context.SaveChangesAsync() == 1;
        }

        public bool Exists(Guid id)
        {
            bool result = context.Set<App>().Any(x => x.Id == id);

            #region ns-custom-exists
            #endregion ns-custom-exists

            return result;
        }
    }
}
