using System;
using System.Linq;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LiquidVisions.PanthaRhei.Generated.Infrastructure.EntityFramework
{
    public class PackageRepository : IGetGateway<Package>, IGetByIdGateway<Package>, ICreateGateway<Package>, IDeleteGateway<Package>, IUpdateGateway<Package>
    {
        private readonly Context context;

        public PackageRepository(Context context)
        {
            this.context = context;
        }

        public async Task<int> Update(Package entity)
        {
            context.Set<Package>().Add(entity);
            context.Entry(entity).State = EntityState.Modified;
            #region ns-custom-update
            #endregion ns-custom-update
            return await context.SaveChangesAsync();
            
        }

        public async Task<int> Create(Package entity)
        {
            entity.Component = context.Components.Single(x => x.Id == entity.Component.Id);
            
            context.Set<Package>().Add(entity);
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

        public IQueryable<Package> Get()
        {
            IQueryable<Package> result = context.Set<Package>();

            #region ns-custom-get
            #endregion ns-custom-get

            return result;
        }

        public Package GetById(Guid id)
        {
            
            Package result = context.Set<Package>()
                .SingleOrDefault(x => x.Id == id);

            #region ns-custom-getbyid
            #endregion ns-custom-getbyid

            return result;
        }

        public async Task<bool> Delete(Package entity)
        {
            context.Set<Package>().Remove(entity);
            context.Entry(entity).State = EntityState.Deleted;

            #region ns-custom-delete
            #endregion ns-custom-delete

            return await context.SaveChangesAsync() == 1;
        }

        public bool Exists(Guid id)
        {
            bool result = context.Set<Package>().Any(x => x.Id == id);

            #region ns-custom-exists
            #endregion ns-custom-exists

            return result;
        }
    }
}
