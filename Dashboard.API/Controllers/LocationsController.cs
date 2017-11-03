using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Data.EF.Contracts;
using Microsoft.Extensions.Logging;
using Dashboard.Entities;

namespace Dashboard.API.Controllers
{
    
    [Route("api/dashboard/locations")]
    public class LocationsController : Controller
    {
        public IRepo _repo;
        private ILogger<LocationsController> _logger;

        public LocationsController(IRepo repo,
            ILogger<LocationsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET api/dashboard/locations
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetAll<Location>();
                return Ok(result);
                //return Ok(Mapper.Map<IEnumerable<CommitmentViewModel>>(result));
            }
            catch (Exception ex)
            {
                // LOGGING TODO
                _logger.LogError($"Exception thrown white getting locations: {ex}");
                return BadRequest($"Error ocurred");
            }
        }

        // GET api/dashboard/locations/5
        [HttpGet("{id}", Name = "GetLocation")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = _repo.Get<Location>(id);
                return Ok(result);
                //return Ok(Mapper.Map<CommitmentViewModel>(result));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown while getting commitment: {ex}");
                return BadRequest($"Error ocurred");
            }

        }

        // POST api/dashboard/locations
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]Location location)
        {
            if (ModelState.IsValid)
            {
                //var newCommitment = Mapper.Map<Commitment>(commitment);
                var addedLocation = await _repo.AddAsync(location);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/dashboard/commitments/{addedLocation.LocationId}", addedLocation);
                }
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/dashboard/Commitments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Location location)
        {
            if (ModelState.IsValid)
            {
                //var projectId = 0;
                //var userId = 0;
                var locationFromRepo = _repo.Get<Location>(id);
                //Mapper.Map(commitmentVM, commiFromRepo);

                locationFromRepo.City = location.City ?? locationFromRepo.City;
                locationFromRepo.Address = location.Address ?? locationFromRepo.Address;
                locationFromRepo.Clients = location.Clients ?? locationFromRepo.Clients;
                
               

                var locationUpdated = _repo.Update(locationFromRepo);

                if (!await _repo.SaveChangesAsync())
                {
                    _logger.LogError($"Thrown exception when updating");
                    BadRequest("Something when wrong while updating");
                }
                return Ok(/*Mapper.Map<CommitmentViewModel>(*/locationUpdated/*)*/);
            }
            return BadRequest("Error occured");

        }

        // DELETE api/dashboard/Commitments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var locationToDel = _repo.Get<Location>(id);
            _repo.Delete(locationToDel);
            if (await _repo.SaveChangesAsync())
                return Ok($"Commitment deleted!");
            else
                return BadRequest($"Location {locationToDel.LocationId } wasn't deleted!");
        }

    }
}