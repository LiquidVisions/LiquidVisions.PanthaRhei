using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using Microsoft.EntityFrameworkCore;

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Repositories
{

    internal class GenericRepository<TEntity> : GenericRepository, ICreateRepository<TEntity>, IGetRepository<TEntity>, IUpdateRepository<TEntity>, IDeleteRepository<TEntity>
        where TEntity : class
    {
        private readonly IDependencyFactory dependencyFactory;

        public GenericRepository(IDependencyFactory dependencyFactory)
            : base(dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
        }

        public Type ContextType => Context.GetType();

        public Type ConfigurationType => dependencyFactory
            .Get<IEntityTypeConfiguration<TEntity>>()
            .GetType();

        public bool Create(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Context.Entry(entity).State = EntityState.Added;

            return Context.SaveChanges() >= 0;
        }

        public bool Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            Context.Entry(entity).State = EntityState.Deleted;

            return Context.SaveChanges() >= 0;
        }

        public bool DeleteAll()
        {
            int result = Context.Set<TEntity>().ExecuteDelete();

            return result >= 0;
        }

        public bool DeleteById(object id)
        {
            TEntity entity = GetById(id);

            return Delete(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>();
        }

        public TEntity GetById(object id)
        {
            return Context.Set<TEntity>()
                .Find(id);
        }

        public bool Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            Context.Entry(entity).State = EntityState.Modified;

            return Context.SaveChanges() >= 0;
        }
    }
}
