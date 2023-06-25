using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Domain.Gateways;
using LiquidVisions.PanthaRhei.Domain.Interactors.Dependencies;
using Microsoft.EntityFrameworkCore;

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Repositories
{
    internal class GenericRepository<TEntity> : ICreateGateway<TEntity>, IGetGateway<TEntity>, IUpdateGateway<TEntity>, IDeleteGateway<TEntity>
        where TEntity : class
    {
        private readonly Context context;
        private readonly IDependencyFactoryInteractor dependencyFactory;

        public GenericRepository(IDependencyFactoryInteractor dependencyFactory)
        {
            context = dependencyFactory.Get<Context>();
            this.dependencyFactory = dependencyFactory;
        }

        public Type ContextType => context.GetType();

        public Type ConfigurationType => dependencyFactory
            .Get<IEntityTypeConfiguration<TEntity>>()
            .GetType();

        public bool Create(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            context.Entry(entity).State = EntityState.Added;

            return context.SaveChanges() >= 0;
        }

        public bool Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
            context.Entry(entity).State = EntityState.Deleted;

            return context.SaveChanges() >= 0;
        }

        public bool DeleteAll()
        {
            int result = context.Set<TEntity>().ExecuteDelete();

            return result >= 0;
        }

        public bool DeleteById(object id)
        {
            TEntity entity = GetById(id);

            return Delete(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return context.Set<TEntity>();
        }

        public TEntity GetById(object id)
        {
            return context.Set<TEntity>()
                .Find(id);
        }

        public bool Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            context.Entry(entity).State = EntityState.Modified;

            return context.SaveChanges() >= 0;
        }
    }
}
