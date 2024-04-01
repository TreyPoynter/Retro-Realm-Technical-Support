namespace RetroRealm.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> List(QueryOptions<T> queryOptions);
        T? GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
