using NS.Domain;

namespace NS.Application.Gateways
{
    public interface IGateway<TEntity>
        where TEntity : IEntity
    {
    }
}
