using RetroRealm.Models;

namespace RetroRealm.Data.Services
{
    public class CustomerService : ICustomerService
    {
        private ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<CustomerModel> GetAll()
        {
            IQueryable<CustomerModel> customers = _context.Customers;
            return customers;
        }

        public CustomerModel? GetCustomerByEmail(string email)
        {
            CustomerModel? customer = _context.Customers.Find(email);
            return customer;
        }

        public CustomerModel GetCustomerById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
