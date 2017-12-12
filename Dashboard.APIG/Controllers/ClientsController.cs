using Dashboard.APIG.Infrastructure;
using Dashboard.APIG.Models;
using Dashboard.DataG.Contracts;
using Dashboard.EntitiesG.EntitiesRev;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.APIG.Controllers
{
    
    [Route("api/dashboard/clients")]
    public class ClientsController : Controller
    {
        public IRepoClient _repo;
        private IRepoLocation _repoLoc;
        private ILogger<ClientsController> _logger;

        public ClientsController(IRepoClient repo, 
            IRepoLocation repoLoc,
            ILogger<ClientsController> logger)
        {
            _repo = repo;
            _repoLoc = repoLoc;
            _logger = logger;
        }

        // GET api/dashboard/clients
        [HttpGet("")]
        [NoCache]
        [ProducesResponseType(typeof(List<Client>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Client>), 400)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result =  _repo.Include(p => p.Projects, l => l.Location);
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
        [NoCache]
        [ProducesResponseType(typeof(Client), 200)]
        [ProducesResponseType(typeof(ApiResponse<Client>), 400)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {   
                var result = _repo.GetById(id);
                return Ok(result);
               
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown while getting client: {ex}");
                return BadRequest($"Error ocurred");
            }

        }

        // POST api/dashboard/clients
        [HttpPost("")]
        [ProducesResponseType(typeof(ApiResponse<Client>), 201)]
        [ProducesResponseType(typeof(ApiResponse<Client>), 400)]
        public async Task<IActionResult> Post([FromBody] ClientLocation client)
        {
            if (ModelState.IsValid)
            {
                var location = new Location
                {
                    Address = client.Address,
                    City = client.City
                };
               
                var newClient = new Client{ ClientName = client.ClientName};
                try
                {
                   
                    var locationAdded = await _repoLoc.Create(location);
                    newClient.LocationId = locationAdded.LocationId;
                    var addedClient = await _repo.Create(newClient);

                    return Ok(addedClient);
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Exception thrown while getting client: {ex}");
                    
                }


            }
            return BadRequest($"Error ocurred");

        }

        // PUT api/dashboard/clients/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Client>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Client>), 400)]
        public async Task<IActionResult> Put([FromBody]Client client)
        {
            if (ModelState.IsValid)
            {
                

                try
                {
                    var clientFromRepo = await _repo.GetById(client.ClientId);
              
                    
                    clientFromRepo.ClientName = client.ClientName ?? clientFromRepo.ClientName;
                    clientFromRepo.Description = client.Description ?? clientFromRepo.Description;
                    clientFromRepo.Location = client.Location ?? clientFromRepo.Location;
                    clientFromRepo.Projects = client.Projects ?? clientFromRepo.Projects;
                    clientFromRepo.LocationId = client.LocationId != 0 ? client.LocationId : clientFromRepo.LocationId;
                    var clientUpdated = await _repo.Update(clientFromRepo.ClientId, clientFromRepo);
                    return Ok(clientUpdated);
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Thrown exception when updating: {ex}" );
                }
                
            }
            return BadRequest("Error occured");

        }

        // DELETE api/dashboard/Commitments/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Client>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Client>), 400)]
        public async Task<IActionResult> Delete(int id)
        {
            var clientToDel = await _repo.GetById(id);
            if (clientToDel != null)
            {
                try
                {
                    await _repo.Delete(clientToDel.ClientId);
                    return Ok(clientToDel);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Thrown exception when updating: {ex}");
                }
            }
            return BadRequest($"Client {clientToDel.ClientName } wasn't deleted!");
        }
        

    }
}
