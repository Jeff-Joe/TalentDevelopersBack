using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TalentDevelopers.Interfaces;
using TalentDevelopers.Models;
using TalentDevelopers.ViewModels;

namespace TalentDevelopers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces(typeof(IEnumerable<CustomerViewModel>))]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var customers = _mapper.Map<List<CustomerViewModel>>(await _customerRepository.GetCustomers());

            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState); 
            }

            return Ok(customers);
        }

        [HttpGet("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(typeof(CustomerViewModel))]
        public async Task<IActionResult> GetCustomerAsync(int customerId)
        {
            if (!_customerRepository.CustomerExists(customerId))
            {
                return NotFound();
            }

            var customer = _mapper.Map<CustomerViewModel>(await _customerRepository.GetCustomer(customerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(customer);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CustomerViewModel customerCreate)
        {
            if (customerCreate == null)
            {
                return BadRequest(ModelState);
            }

            var customers = await _customerRepository.GetCustomers();
            var filteredCustomer = customers
                .Where(x => x.Name.Trim().ToUpper() == customerCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(filteredCustomer != null)
            {
                ModelState.AddModelError("", "Customer already exists");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerMap = _mapper.Map<Customer>(customerCreate);

            if(!(await _customerRepository.CreateCustomer(customerMap)))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Customer successfully created");
        }

        [HttpPut("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCustomerAsync(int customerId, [FromBody] CustomerViewModel updatedCustomer)
        {
            if (updatedCustomer == null)
            {
                return BadRequest(ModelState);
            }

            if (customerId != updatedCustomer.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_customerRepository.CustomerExists(customerId))
            {
                return NotFound();
            }

            var customerMap = _mapper.Map<Customer>(updatedCustomer);

            if(!(await _customerRepository.UpdateCustomer(customerMap)))
            {
                ModelState.AddModelError("", "Something went wrong updating customer");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return Ok("Customer updated");
        }

        [HttpDelete("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCustomerAsync(int customerId)
        {
            if (!_customerRepository.CustomerExists(customerId))
            {
                return NotFound();
            }

            var customerToDelete = await _customerRepository.GetCustomer(customerId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!(await _customerRepository.DeleteCustomer(customerToDelete)))
            {
                ModelState.AddModelError("", "Something went wrong deleting customer");
            }

            return Ok("Customer successfully deleted");
        }
    }
}
