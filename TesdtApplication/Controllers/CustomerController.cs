using Microsoft.AspNetCore.Mvc;
using TesdtApplication.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TesdtApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
       private readonly Services.CustomerService _customerService;
        public CustomerController(Services.CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomerAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCustomer([FromBody]Customer customer)
        {
            if (customer==null)
            {
                return BadRequest();
            }

            var createdCustomer = await _customerService.CreateCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomer.Id }, createdCustomer);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCustomer(int id, 
            [FromBody] Customer customer)
        {
            if (customer == null || customer.Id != id)
            {
                return BadRequest();
            }
            var existingCustomer = await _customerService.GetCustomerAsync(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            // Update the customer details
            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            // Update the cache
            await _customerService.CreateCustomerAsync(existingCustomer);
            return NoContent();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var existingCustomer = await _customerService.GetCustomerAsync(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            // Remove the customer from the cache
            await _customerService.CreateCustomerAsync(new Customer { Id = id, Name = string.Empty, Email = string.Empty });
            return NoContent();
        }
    }
}
