using System;

namespace LiquidVisions.PanthaRhei.Domain.Repositories
{
    /// <summary>
    /// Represents a generic Get Repository used in a seeding usecase.
    /// </summary>
    public interface IEntitiesToSeedRepository : IGetRepository<Type>
    {
    }
}
