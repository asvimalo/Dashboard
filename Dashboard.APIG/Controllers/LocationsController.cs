using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dashboard.DataG.EF.Contracts;
using Microsoft.Extensions.Logging;
using Dashboard.EntitiesG.EntitiesRev;

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
        public async Task<IActionResult> Get()
        {
            try
            {
                var result =  _repo.GetAll();
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
                var result = await _repo.GetById(id);
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
                try
                {
                    var addedLocation =  _repo.Create(location);
                    return Created($"api/dashboard/commitments/{addedLocation.Id}", addedLocation);
                    //return Ok(Mapper.Map<CommitmentViewModel>(result));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception thrown while getting location: {ex}");
                    return BadRequest($"Error ocurred");
                }
                //var newCommitment = Mapper.Map<Commitment>(commitment);                             
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
                try
                {
                    var locationFromRepo = await _repo.GetById(id);
                    //Mapper.Map(commitmentVM, commiFromRepo);

                    locationFromRepo.City = location.City ?? locationFromRepo.City;
                    locationFromRepo.Address = location.Address ?? locationFromRepo.Address;
                    locationFromRepo.Clients = location.Clients ?? locationFromRepo.Clients;
                    var locationUpdated = _repo.Update(locationFromRepo.Id, locationFromRepo);

                    return Ok(/*Mapper.Map<CommitmentViewModel>(*/locationUpdated/*)*/);
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var locationToDel = await _repo.GetById(id);
                await _repo.Delete(locationToDel.Id);

                return Ok($"Commitment deleted!");
            }
            catch (Exception ex)
            {

                _logger.LogError($"Thrown exception when updating {ex}");
                
                return BadRequest($"Location wasn't deleted!");
            }
           
                
        }

    }
}