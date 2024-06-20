﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TalentDevelopers.Interfaces;
using TalentDevelopers.Models;
using TalentDevelopers.ViewModels;

namespace TalentDevelopers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;

        public StoreController(IStoreRepository storeRepository, IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces(typeof(IEnumerable<StoreViewModel>))]
        public IActionResult GetStores()
        {
            var stores = _mapper.Map<List<StoreViewModel>>(_storeRepository.GetStores());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(stores);
        }

        [HttpGet("{storeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(typeof(StoreViewModel))]
        public IActionResult GetStore(int storeId)
        {
            if (!_storeRepository.StoreExists(storeId))
                return NotFound();

            var store = _mapper.Map<StoreViewModel>(_storeRepository.GetStore(storeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(store);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateStore([FromBody] StoreViewModel storeCreate)
        {
            if (storeCreate == null)
                return BadRequest(ModelState);

            var stores = _storeRepository.GetStores()
                .Where(x => x.Name.Trim().ToUpper() == storeCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (stores != null)
            {
                ModelState.AddModelError("", "Store already exists");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var storeMap = _mapper.Map<Store>(storeCreate);

            if (!_storeRepository.CreateStore(storeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Store successfully created");
        }

        [HttpPut("{storeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateStore(int storeId, [FromBody] StoreViewModel updatedStore)
        {
            if (updatedStore == null)
                return BadRequest(ModelState);

            if (storeId != updatedStore.Id)
                return BadRequest(ModelState);

            if (!_storeRepository.StoreExists(storeId))
                return NotFound();

            var storeMap = _mapper.Map<Store>(updatedStore);

            if (!_storeRepository.UpdateStore(storeMap))
            {
                ModelState.AddModelError("", "Something went wrong updating store");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return Ok("Store updated");
        }

        [HttpDelete("{storeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteStore(int storeId)
        {
            if (!_storeRepository.StoreExists(storeId))
                return NotFound();

            var storeToDelete = _storeRepository.GetStore(storeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_storeRepository.DeleteStore(storeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting store");
            }

            return Ok("Store successfully deleted");
        }
    }
}