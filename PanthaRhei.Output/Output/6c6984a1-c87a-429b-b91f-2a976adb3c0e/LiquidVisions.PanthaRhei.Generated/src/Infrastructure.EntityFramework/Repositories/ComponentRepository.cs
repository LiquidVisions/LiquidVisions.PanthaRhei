using System;
using System.Linq;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LiquidVisions.PanthaRhei.Generated.Infrastructure.EntityFramework
{
    public class ComponentRepository : IGetGateway<Component>, IGetByIdGateway<Component>, ICreateGateway<Component>, IDeleteGateway<Component>, IUpdateGateway<Component>
    {
        private readonly Context context;

        public ComponentRepository(Context context)
        {
            this.context = context;
        }

        public async Task<int> Update(Component entity)
        {
            context.Set<Component>().Add(entity);
            context.Entry(entity).State = EntityState.Modified;
            #region ns-custom-update
            #endregion ns-custom-update
            return await context.SaveChangesAsync();
            
        }

        public async Task<int> Create(Component entity)
        {
            entity.Expander = context.Expanders.Single(x => x.Id == entity.Expander.Id);
            
            context.Set<Component>().Add(entity);
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

        public IQueryable<Component> Get()
        {
            IQueryable<Component> result = context.Set<Component>();

            #region ns-custom-get
            #endregion ns-custom-get

            return result;
        }

        public Component GetById(Guid id)
        {
            
            Component result = context.Set<Component>()
                .SingleOrDefault(x => x.Id == id);

            #region ns-custom-getbyid
            #endregion ns-custom-getbyid

            return result;
        }

        public async Task<bool> Delete(Component entity)
        {
            context.Set<Component>().Remove(entity);
            context.Entry(entity).State = EntityState.Deleted;

            #region ns-custom-delete
            #endregion ns-custom-delete

            return await context.SaveChangesAsync() == 1;
        }

        public bool Exists(Guid id)
        {
            bool result = context.Set<Component>().Any(x => x.Id == id);

            #region ns-custom-exists
            #endregion ns-custom-exists

            return result;
        }
    }
}
