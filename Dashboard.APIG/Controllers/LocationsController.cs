using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.DataG.Contracts;
using Microsoft.Extensions.Logging;
using Dashboard.EntitiesG.EntitiesRev;
using Dashboard.APIG.Infrastructure;
using System.Collections.Generic;
using Dashboard.APIG.Models;

namespace Dashboard.API.Controllers
{
    
    [Route("api/dashboard/locations")]
    public class LocationsController : Controller
    {
        public IRepoLocation _repo;
        private ILogger<LocationsController> _logger;

        public LocationsController(IRepoLocation repo,
            ILogger<LocationsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET api/dashboard/locations
        [HttpGet("")]
        [NoCache]
        [ProducesResponseType(typeof(List<Location>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Location>), 400)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result =  _repo.GetAll();
                return Ok(result);
                
            }
            catch (Exception ex)
            {
                
                _logger.LogError($"Exception thrown white getting locations: {ex}");
                return BadRequest($"Error ocurred");
            }
        }

        // GET api/dashboard/locations/5
        [HttpGet("{id}", Name = "GetLocation")]
        [NoCache]
        [ProducesResponseType(typeof(Location), 200)]
        [ProducesResponseType(typeof(ApiResponse<Location>), 400)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _repo.GetById(id);
                return Ok(result);
               
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown while getting commitment: {ex}");
                return BadRequest($"Error ocurred");
            }

        }

        // POST api/dashboard/locations
        [HttpPost("")]
        [ProducesResponseType(typeof(ApiResponse<Location>), 201)]
        [ProducesResponseType(typeof(ApiResponse<Location>), 400)]
        public async Task<IActionResult> Post([FromBody]Location location)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var addedLocation =  _repo.Create(location);
                    return Ok(addedLocation);
                    
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception thrown while getting location: {ex}");
                    return BadRequest($"Error ocurred");
                }
                                         
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/dashboard/Commitments/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Location>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Location>), 400)]
        public async Task<IActionResult> Put(int id, [FromBody]Location location)
        {
            if (ModelState.IsValid)
            {
               
                try
                {
                    var locationFromRepo = await _repo.GetById(id);
                    

                    locationFromRepo.City = location.City ?? locationFromRepo.City;
                    locationFromRepo.Address = location.Address ?? locationFromRepo.Address;
                    locationFromRepo.Clients = location.Clients ?? locationFromRepo.Clients;
                    var locationUpdated = _repo.Update(locationFromRepo.LocationId, locationFromRepo);

                    return Ok(locationUpdated);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Thrown exception when updating {ex}");
                    BadRequest("Something when wrong while updating");
                }                                                         
            }
            return BadRequest("Error occured");

        }

        // DELETE api/dashboard/Commitments/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Location>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Location>), 400)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var locationToDel = await _repo.GetById(id);
                await _repo.Delete(locationToDel.LocationId);

                return Ok(locationToDel);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Thrown exception when updating {ex}");
                
                return BadRequest($"Location wasn't deleted!");
            }
           
                
        }

    }
}