using System;
using System.Linq;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LiquidVisions.PanthaRhei.Generated.Infrastructure.EntityFramework
{
    public class EntityRepository : IGetGateway<Entity>, IGetByIdGateway<Entity>, ICreateGateway<Entity>, IDeleteGateway<Entity>, IUpdateGateway<Entity>
    {
        private readonly Context context;

        public EntityRepository(Context context)
        {
            this.context = context;
        }

        public async Task<int> Update(Entity entity)
        {
            
            context.Set<Entity>().Add(entity);
            context.Entry(entity).State = EntityState.Modified;
            #region ns-custom-update
            #endregion ns-custom-update
            return await context.SaveChangesAsync();
            
        }

        public async Task<int> Create(Entity entity)
        {
            context.Set<Entity>().Add(entity);
            context.Entry(entity).State = EntityState.Added;

            #region ns-custom-create
            #endregion ns-custom-create

            return await context.SaveChangesAsync();
            
        }

        public IQueryable<Entity> Get()
        {
            IQueryable<Entity> result = context.Set<Entity>();

            #region ns-custom-get
            #endregion ns-custom-get

            return result;
        }

        public Entity GetById(Guid id)
        {
            
            Entity result = context.Set<Entity>()
                .SingleOrDefault(x => x.Id == id);

            #region ns-custom-getbyid
            #endregion ns-custom-getbyid

            return result;
        }

        public async Task<bool> Delete(Entity entity)
        {
            context.Set<Entity>().Remove(entity);
            context.Entry(entity).State = EntityState.Deleted;

            #region ns-custom-delete
            #endregion ns-custom-delete

            return await context.SaveChangesAsync() == 1;
        }

        public bool Exists(Guid id)
        {
            bool result = context.Set<Entity>().Any(x => x.Id == id);

            #region ns-custom-exists
            #endregion ns-custom-exists

            return result;
        }
    }
}
