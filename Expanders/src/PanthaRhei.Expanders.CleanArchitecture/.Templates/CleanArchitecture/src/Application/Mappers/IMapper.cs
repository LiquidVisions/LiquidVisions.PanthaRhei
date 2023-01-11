namespace NS.Application.Mappers
{
    public interface IMapper<TSource, TTarget>
    {
        void Map(TSource source, TTarget target);

        TTarget Map(TSource source);
    }
}
