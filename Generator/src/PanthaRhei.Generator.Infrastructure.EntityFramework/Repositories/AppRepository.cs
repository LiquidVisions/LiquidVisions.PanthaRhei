using System;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Repositories
{
    /// <summary>
    /// An implementation of the contract <seealso cref="IAppRepository"/>.
    /// </summary>
    internal class AppRepository : IAppRepository
    {
        private readonly Context context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppRepository"/> class.
        /// </summary>
        /// <param name="context"><seealso cref="Context"/></param>
        public AppRepository(Context context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public App GetById(Guid appId)
        {
            App app = context.Apps.Find(appId);

            return app;
        }
    }
}
