using RetroRealm.Models;

namespace RetroRealm.Data.Services
{
    public interface ICustomerService
    {
        IQueryable<CustomerModel> GetAll();
        CustomerModel? GetCustomerById(int id);
        CustomerModel? GetCustomerByEmail(string email);
        Task EditCustomer(CustomerModel customer);
        Task AddCustomer(CustomerModel customer);
        Task DeleteCustomer(CustomerModel customer);
    }
}
