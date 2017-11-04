using Dashboard.DataG.EF.Contracts;
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
        private ILogger<ClientsController> _logger;

        public ClientsController(IRepoClient repo,
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
                var result =  _repo.GetAll();
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
                var result = _repo.GetById(id);
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
                
                try
                {
                    await _repo.Create(client);
                    return Ok($"Client created");
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Exception thrown while getting client: {ex}");
                    
                }


            }
            return BadRequest($"Error ocurred");

        }

        // PUT api/dashboard/Commitments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Client client)
        {
            if (ModelState.IsValid)
            {
                //var projectId = 0;
                //var userId = 0;
                


                try
                {
                    var clientFromRepo = await _repo.GetById(id);
                    //Mapper.Map(commitmentVM, commiFromRepo);

                    clientFromRepo.ClientName = client.ClientName ?? clientFromRepo.ClientName;
                    clientFromRepo.Description = client.Description ?? clientFromRepo.Description;
                    clientFromRepo.Location = client.Location ?? clientFromRepo.Location;
                    clientFromRepo.Projects = client.Projects ?? clientFromRepo.Projects;
                    clientFromRepo.LocationId = client.LocationId != 0 ? client.LocationId : clientFromRepo.LocationId;
                    var clientUpdated = _repo.Update(clientFromRepo.Id, clientFromRepo);
                    return Ok(/*Mapper.Map<CommitmentViewModel>(*/clientUpdated/*)*/);
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
        public async Task<IActionResult> Delete(int id)
        {
            var clientToDel = await _repo.GetById(id);
            if (clientToDel != null)
            {
                try
                {
                    await _repo.Delete(clientToDel.Id);
                    return Ok($"Commitment deleted!");
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
