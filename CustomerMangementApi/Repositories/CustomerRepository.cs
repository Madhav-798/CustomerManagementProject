using CustomerMangementApi.Models;

namespace CustomerMangementApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customers;

        public CustomerRepository()
        {
            _customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "John Doe", Email = "john.doe@example.com" },
                new Customer { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com" },
                new Customer { Id = 3, Name = "Taylor Swift", Email = "t.smith@example.com" }
            };
        }

        public async Task<IEnumerable<Customer>> GetAllAsync() => await Task.FromResult(_customers);

        public async Task<Customer> GetByIdAsync(int id) => await Task.FromResult(_customers.FirstOrDefault(c => c.Id == id));

        public async Task AddAsync(Customer customer)
        {
            customer.Id = _customers.Count > 0 ? _customers.Max(c => c.Id) + 1 : 1;
            _customers.Add(customer);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Customer customer)
        {
            var existing = await GetByIdAsync(customer.Id);
            if (existing != null)
            {
                existing.Name = customer.Name;
                existing.Email = customer.Email;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await GetByIdAsync(id);
            if (customer != null)
            {
                _customers.Remove(customer);
            }
            await Task.CompletedTask;
        }
    }
}
