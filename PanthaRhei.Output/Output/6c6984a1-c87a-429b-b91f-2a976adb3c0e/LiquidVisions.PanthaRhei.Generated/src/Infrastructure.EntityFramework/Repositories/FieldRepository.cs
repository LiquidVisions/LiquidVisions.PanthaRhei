using System;
using System.Linq;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generated.Application.Gateways;
using LiquidVisions.PanthaRhei.Generated.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LiquidVisions.PanthaRhei.Generated.Infrastructure.EntityFramework
{
    public class FieldRepository : IGetGateway<Field>, IGetByIdGateway<Field>, ICreateGateway<Field>, IDeleteGateway<Field>, IUpdateGateway<Field>
    {
        private readonly Context context;

        public FieldRepository(Context context)
        {
            this.context = context;
        }

        public async Task<int> Update(Field entity)
        {
            context.Set<Field>().Add(entity);
            context.Entry(entity).State = EntityState.Modified;
            #region ns-custom-update
            #endregion ns-custom-update
            return await context.SaveChangesAsync();
            
        }

        public async Task<int> Create(Field entity)
        {
            entity.Entity = context.Entities.Single(x => x.Id == entity.Entity.Id);
            entity.Reference = context.Entities.Single(x => x.Id == entity.Reference.Id);
            
            context.Set<Field>().Add(entity);
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

        public IQueryable<Field> Get()
        {
            IQueryable<Field> result = context.Set<Field>();

            #region ns-custom-get
            #endregion ns-custom-get

            return result;
        }

        public Field GetById(Guid id)
        {
            
            Field result = context.Set<Field>()
                .SingleOrDefault(x => x.Id == id);

            #region ns-custom-getbyid
            #endregion ns-custom-getbyid

            return result;
        }

        public async Task<bool> Delete(Field entity)
        {
            context.Set<Field>().Remove(entity);
            context.Entry(entity).State = EntityState.Deleted;

            #region ns-custom-delete
            #endregion ns-custom-delete

            return await context.SaveChangesAsync() == 1;
        }

        public bool Exists(Guid id)
        {
            bool result = context.Set<Field>().Any(x => x.Id == id);

            #region ns-custom-exists
            #endregion ns-custom-exists

            return result;
        }
    }
}
