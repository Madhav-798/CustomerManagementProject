using CustomerMangementApi.Models;
using CustomerMangementApi.ResponseMapper;
using Microsoft.AspNetCore.Mvc;

namespace CustomerMangementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerResponseMapper _responseMapper;

        public CustomersController(ICustomerResponseMapper responseMapper)
        {
            _responseMapper = responseMapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            try
            {
                var customers = await _responseMapper.GetAllCustomersAsync();
                return Ok(customers);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            try
            {
                var customer = await _responseMapper.GetCustomerByIdAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }
                return Ok(customer);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Post([FromBody] Customer customer)
        {
            try
            {
                await _responseMapper.AddCustomerAsync(customer);
                return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer customer)
        {
            try
            {
                var existing = await _responseMapper.GetCustomerByIdAsync(id);
                if (existing == null)
                {
                    return NotFound();
                }
                await _responseMapper.UpdateCustomerAsync(customer);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var customer = await _responseMapper.GetCustomerByIdAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }
                await _responseMapper.DeleteCustomerAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
