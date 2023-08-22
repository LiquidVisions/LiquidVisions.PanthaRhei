using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using Microsoft.EntityFrameworkCore;

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Repositories
{
    internal class GenericRepository : IMigrationService
    {
        private readonly Context context;

        public GenericRepository(IDependencyFactory dependencyFactory)
        {
            context = dependencyFactory.Get<Context>();
        }

        internal Context Context => context;

        public void Migrate()
        {
            context.Database.Migrate();
        }
    }
}
