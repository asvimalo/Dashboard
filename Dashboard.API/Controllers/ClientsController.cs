using Dashboard.Data.EF.Contracts;
using Dashboard.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.Controllers
{
    
    [Route("api/dashboard/clients")]
    public class ClientsController : Controller
    {
        public IRepo _repo;
        private ILogger<ClientsController> _logger;

        public ClientsController(IRepo repo,
            ILogger<ClientsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET api/dashboard/clients
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetAll<Client>();
                return Ok(result);
                //return Ok(Mapper.Map<IEnumerable<CommitmentViewModel>>(result));
            }
            catch (Exception ex)
            {
                // LOGGING TODO
                _logger.LogError($"Exception thrown white getting clients: {ex}");
                return BadRequest($"Error ocurred");
            }
        }

        // GET api/dashboard/clients/5
        [HttpGet("{id}", Name = "GetClient")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = _repo.Get<Client>(id);
                return Ok(result);
                //return Ok(Mapper.Map<CommitmentViewModel>(result));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown while getting client: {ex}");
                return BadRequest($"Error ocurred");
            }

        }

        // POST api/dashboard/clients
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]Client client)
        {
            if (ModelState.IsValid)
            {
                //var newCommitment = Mapper.Map<Commitment>(commitment);
                var addeClient = await _repo.AddAsync(client);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/dashboard/commitments/{addeClient.ClientName}", addeClient);
                }
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/dashboard/Commitments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Client client)
        {
            if (ModelState.IsValid)
            {
                //var projectId = 0;
                //var userId = 0;
                var clientFromRepo = _repo.Get<Client>(id);
                //Mapper.Map(commitmentVM, commiFromRepo);

                clientFromRepo.ClientName = client.ClientName ?? clientFromRepo.ClientName;
                clientFromRepo.Description = client.Description ?? clientFromRepo.Description;
                clientFromRepo.Location = client.Location ?? clientFromRepo.Location;
                clientFromRepo.Projects = client.Projects ?? clientFromRepo.Projects;
                clientFromRepo.LocationId = client.LocationId != 0 ? client.LocationId : clientFromRepo.LocationId;
           

                var clientUpdated = _repo.Update(clientFromRepo);

                if (!await _repo.SaveChangesAsync())
                {
                    _logger.LogError($"Thrown exception when updating");
                    BadRequest("Something when wrong while updating");
                }
                return Ok(/*Mapper.Map<CommitmentViewModel>(*/clientUpdated/*)*/);
            }
            return BadRequest("Error occured");

        }

        // DELETE api/dashboard/Commitments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var clientToDel = _repo.Get<Client>(id);
            _repo.Delete(clientToDel);
            if (await _repo.SaveChangesAsync())
                return Ok($"Commitment deleted!");
            else
                return BadRequest($"Client {clientToDel.ClientName } wasn't deleted!");
        }

    }
}
