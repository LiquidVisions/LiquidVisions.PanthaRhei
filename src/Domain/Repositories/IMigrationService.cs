namespace LiquidVisions.PanthaRhei.Domain.Repositories
{
    /// <summary>
    /// Represents a contract for a generic Migration Service.
    /// </summary>
    public interface IMigrationService
    {
        /// <summary>
        /// Executes the service.
        /// </summary>
        void Migrate();
    }
}
