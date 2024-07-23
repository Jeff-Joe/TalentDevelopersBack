using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TalentDevelopers.Interfaces;
using TalentDevelopers.Models;
using TalentDevelopers.ViewModels;

namespace TalentDevelopers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IMapper _mapper;

        public SalesController(ISalesRepository salesRepository, IMapper mapper)
        {
            _salesRepository = salesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces(typeof(IEnumerable<SalesViewModelGet>))]
        public async Task<IActionResult> GetSalesAsync()
        {
            var saless = _mapper.Map<List<SalesViewModelGet>>(await _salesRepository.GetSales());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(saless);
        }

        [HttpGet("{salesId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(typeof(SalesViewModelGet))]
        public async Task<IActionResult> GetSaleAsync(int salesId)
        {
            if (!_salesRepository.SalesExists(salesId))
            {
                return NotFound();
            }

            var sales = _mapper.Map<SalesViewModelGet>(await _salesRepository.GetSale(salesId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(sales);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSalesAsync([FromBody] SalesViewModelPost salesCreate)
        {
            if (salesCreate == null)
            {
                return BadRequest(ModelState);
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var salesMap = _mapper.Map<Sales>(salesCreate);

            if (!(await _salesRepository.CreateSales(salesMap)))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Sale successfully created");
        }

        [HttpPut("{salesId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSalesAsync(int salesId, [FromBody] SalesViewModelPost updatedSales)
        {
            if (updatedSales == null)
            {
                return BadRequest(ModelState);
            }

            if (salesId != updatedSales.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_salesRepository.SalesExists(salesId))
            {
                return NotFound();
            }

            var salesMap = _mapper.Map<Sales>(updatedSales);

            if (!(await _salesRepository.UpdateSales(salesMap)))
            {
                ModelState.AddModelError("", "Something went wrong updating sale");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return Ok("Sales updated");
        }

        [HttpDelete("{salesId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSalesAsync(int salesId)
        {
            if (!_salesRepository.SalesExists(salesId))
            {
                return NotFound();
            }

            var saleToDelete = await _salesRepository.GetSale(salesId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!(await _salesRepository.DeleteSales(saleToDelete)))
            {
                ModelState.AddModelError("", "Something went wrong deleting sale");
            }

            return Ok("Sale successfully deleted");
        }
    }
}
