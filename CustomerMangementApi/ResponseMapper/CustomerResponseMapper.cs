using CustomerMangementApi.Models;
using CustomerMangementApi.Repositories;
using CustomerMangementApi.ResponseMapper;

namespace CustomerMangementApi.ResponseMapper
{
        public class CustomerResponseMapper : ICustomerResponseMapper
        {
            private readonly ICustomerRepository _repository;
            private readonly ILogger<CustomerResponseMapper> _logger;

            public CustomerResponseMapper(ICustomerRepository repository, ILogger<CustomerResponseMapper> logger)
            {
                _repository = repository;
                _logger = logger;
            }

            public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
            {
                try
                {
                    return await _repository.GetAllAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while getting all customers.");
                    throw;
                }
            }

            public async Task<Customer> GetCustomerByIdAsync(int id)
            {
                try
                {
                    return await _repository.GetByIdAsync(id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error occurred while getting customer by id: {id}");
                    throw;
                }
            }

            public async Task AddCustomerAsync(Customer customer)
            {
                try
                {
                    await _repository.AddAsync(customer);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while adding a new customer.");
                    throw;
                }
            }

            public async Task UpdateCustomerAsync(Customer customer)
            {
                try
                {
                    await _repository.UpdateAsync(customer);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error occurred while updating customer with id: {customer.Id}");
                    throw;
                }
            }

            public async Task DeleteCustomerAsync(int id)
            {
                try
                {
                    await _repository.DeleteAsync(id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error occurred while deleting customer with id: {id}");
                    throw;
                }
            }
        }
    }
