using Microsoft.EntityFrameworkCore;
using RetroRealm.Models;
using System.Linq;

namespace RetroRealm.Data.Repository
{
    public class CustomerRepository : IRepository<CustomerModel>
    {
        private readonly ApplicationDbContext _context;
        private DbSet<CustomerModel> _dbSet;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<CustomerModel>();
        }
        public void Add(CustomerModel entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(CustomerModel entity)
        {
            _dbSet.Remove(entity);
        }

        public CustomerModel? GetById(int id)
        {
            return _dbSet.Include(c => c.GameModels).FirstOrDefault(c => c.CustomerModelId == id);
        }

        public IEnumerable<CustomerModel> List(QueryOptions<CustomerModel> queryOptions)
        {
            IQueryable<CustomerModel> query = _dbSet;

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

        public void Update(CustomerModel entity)
        {
            _dbSet.Update(entity);
        }
    }
}
