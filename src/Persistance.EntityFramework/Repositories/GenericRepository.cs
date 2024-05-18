using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using Microsoft.EntityFrameworkCore;

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Repositories
{
    internal class GenericRepository(IDependencyFactory dependencyFactory) : IMigrationService
    {
         internal Context Context => dependencyFactory.Resolve<Context>();

        internal IDependencyFactory DependencyFactory => dependencyFactory;

        public void Migrate()
        {
            Context.Database.Migrate();
        }
    }
}
