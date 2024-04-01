
using Microsoft.EntityFrameworkCore;

namespace RetroRealm.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public T? GetById(int id)
        {
            T? entity = _dbSet.Find(id);
            return entity;
        }

        public IEnumerable<T> List(QueryOptions<T> queryOptions)
        {
            IQueryable<T> query = _dbSet;

            foreach (string incldue in queryOptions.GetIncludes())
            {
                query = query.Include(incldue);
            }
            if (queryOptions.HasWhere)
                query = query.Where(queryOptions.Where);
            if (queryOptions.HasOrderBy)
                query = query.OrderBy(queryOptions.OrderBy);
            if (queryOptions.HasPaging)
                query = query.PageBy(queryOptions.PageNumber, queryOptions.PageSize);

            return query;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
