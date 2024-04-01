using Microsoft.EntityFrameworkCore;
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

        public async Task AddCustomer(CustomerModel customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomer(CustomerModel customer)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task EditCustomer(CustomerModel customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public IQueryable<CustomerModel> GetAll()
        {
            IQueryable<CustomerModel> customers = _context.Customers.Include(c => c.CountryModel);
            return customers;
        }

        public CustomerModel? GetCustomerByEmail(string email)
        {
            CustomerModel? customer = _context.Customers.FirstOrDefault(c => c.Email == email);
            return customer;
        }

        public CustomerModel? GetCustomerById(int? id)
        {
            CustomerModel? customer = _context.Customers
                .Include(c => c.GameModels).FirstOrDefault(c => c.CustomerModelId == id);

            return customer;
        }
    }
}
