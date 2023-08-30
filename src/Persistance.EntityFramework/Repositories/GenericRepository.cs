using LiquidVisions.PanthaRhei.Domain.Repositories;
using LiquidVisions.PanthaRhei.Domain.Usecases.Dependencies;
using Microsoft.EntityFrameworkCore;

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Repositories
{
    internal class GenericRepository : IMigrationService
    {
        private readonly Context _context;

        public GenericRepository(IDependencyFactory dependencyFactory)
        {
            _context = dependencyFactory.Resolve<Context>();
        }

        internal Context Context => _context;

        public void Migrate()
        {
            _context.Database.Migrate();
        }
    }
}
