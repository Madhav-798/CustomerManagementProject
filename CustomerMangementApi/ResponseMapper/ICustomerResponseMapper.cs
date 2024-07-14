using CustomerMangementApi.Models;

namespace CustomerMangementApi.ResponseMapper
{
    public interface ICustomerResponseMapper
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
    }
}

