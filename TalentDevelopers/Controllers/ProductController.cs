﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TalentDevelopers.Interfaces;
using TalentDevelopers.Models;
using TalentDevelopers.ViewModels;

namespace TalentDevelopers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces(typeof(IEnumerable<ProductViewModel>))]
        public IActionResult GetProducts()
        {
            var products = _mapper.Map<List<ProductViewModel>>(_productRepository.GetProducts());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(typeof(ProductViewModel))]
        public IActionResult GetProduct(int productId)
        {
            if (!_productRepository.ProductExists(productId))
                return NotFound();

            var product = _mapper.Map<ProductViewModel>(_productRepository.GetProduct(productId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateProduct([FromBody] ProductViewModel productCreate)
        {
            if (productCreate == null)
                return BadRequest(ModelState);

            var products = _productRepository.GetProducts()
                .Where(x => x.Name.Trim().ToUpper() == productCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (products != null)
            {
                ModelState.AddModelError("", "Product already exists");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(productCreate);

            if (!_productRepository.CreateProduct(productMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Product successfully created");
        }

        [HttpPut("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateProduct(int productId, [FromBody] ProductViewModel updatedProduct)
        {
            if (updatedProduct == null)
                return BadRequest(ModelState);

            if (productId != updatedProduct.Id)
                return BadRequest(ModelState);

            if (!_productRepository.ProductExists(productId))
                return NotFound();

            var productMap = _mapper.Map<Product>(updatedProduct);

            if (!_productRepository.UpdateProduct(productMap))
            {
                ModelState.AddModelError("", "Something went wrong updating product");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return Ok("Product updated");
        }

        [HttpDelete("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteProduct(int productId)
        {
            if (!_productRepository.ProductExists(productId))
                return NotFound();

            var productToDelete = _productRepository.GetProduct(productId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productRepository.DeleteProduct(productToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting product");
            }

            return Ok("Product successfully deleted");
        }
    }
}