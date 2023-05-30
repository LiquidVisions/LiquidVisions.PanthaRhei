using System;
using System.Linq;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LiquidVisions.PanthaRhei.Generated.Infrastructure.EntityFramework
{
    public class RelationshipRepository : IGetGateway<Relationship>, IGetByIdGateway<Relationship>, ICreateGateway<Relationship>, IDeleteGateway<Relationship>, IUpdateGateway<Relationship>
    {
        private readonly Context context;

        public RelationshipRepository(Context context)
        {
            this.context = context;
        }

        public async Task<int> Update(Relationship entity)
        {
            context.Set<Relationship>().Add(entity);
            context.Entry(entity).State = EntityState.Modified;
            #region ns-custom-update
            #endregion ns-custom-update
            return await context.SaveChangesAsync();
            
        }

        public async Task<int> Create(Relationship entity)
        {
            entity.WithForeignEntityKey = context.Fields.Single(x => x.Id == entity.WithForeignEntityKey.Id);
            entity.Key = context.Fields.Single(x => x.Id == entity.Key.Id);
            entity.Entity = context.Entities.Single(x => x.Id == entity.Entity.Id);
            entity.WithForeignEntity = context.Entities.Single(x => x.Id == entity.WithForeignEntity.Id);
            
            context.Set<Relationship>().Add(entity);
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

        public IQueryable<Relationship> Get()
        {
            IQueryable<Relationship> result = context.Set<Relationship>();

            #region ns-custom-get
            #endregion ns-custom-get

            return result;
        }

        public Relationship GetById(Guid id)
        {
            
            Relationship result = context.Set<Relationship>()
                .SingleOrDefault(x => x.Id == id);

            #region ns-custom-getbyid
            #endregion ns-custom-getbyid

            return result;
        }

        public async Task<bool> Delete(Relationship entity)
        {
            context.Set<Relationship>().Remove(entity);
            context.Entry(entity).State = EntityState.Deleted;

            #region ns-custom-delete
            #endregion ns-custom-delete

            return await context.SaveChangesAsync() == 1;
        }

        public bool Exists(Guid id)
        {
            bool result = context.Set<Relationship>().Any(x => x.Id == id);

            #region ns-custom-exists
            #endregion ns-custom-exists

            return result;
        }
    }
}
