using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dashboard.DataG.EF.Contracts;
using Microsoft.Extensions.Logging;
using Dashboard.EntitiesG.EntitiesRev;

namespace Dashboard.APIG.Controllers
{
   
    [Route("api/dashboard/commitments")]
    public class CommitmentsController : Controller
    {
        public IRepoCommitment _repo;
        private ILogger<CommitmentsController> _logger;

        public CommitmentsController(IRepoCommitment repo,
            ILogger<CommitmentsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET api/dashboard/commitments
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result =  _repo.Include(x => x.Assignment);
                return Ok(result);
                //return Ok(Mapper.Map<IEnumerable<CommitmentViewModel>>(result));
            }
            catch (Exception ex)
            {
                // LOGGING TODO
                _logger.LogError($"Exception thrown white getting commitments: {ex}");
                return BadRequest($"Error ocurred");
            }
        }

        // GET api/dashboard/Commitments/5
        [HttpGet("{id}", Name = "GetCommitment")]
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

        // POST api/dashboard/Commitments
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]Commitment commitment)
        {
            if (ModelState.IsValid)
            {
                //var newCommitment = Mapper.Map<Commitment>(commitment);
                try
                {
                    var addedCommitment = _repo.Create(commitment);
                    
                    
                        return Created($"api/dashboard/commitments/{addedCommitment.Id}", addedCommitment);
                    
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Exception thrown white getting clients: {ex}");
                    
                }
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/dashboard/Commitments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Commitment commitment)
        {
            if (ModelState.IsValid)
            {
                //var projectId = 0;
                //var userId = 0;
                try
                {
                    var commitmentFromRepo = await _repo.GetById(id);
                    //Mapper.Map(commitmentVM, commiFromRepo);

                    commitmentFromRepo.Assignment = commitment.Assignment ?? commitmentFromRepo.Assignment;
                    commitmentFromRepo.Hours = commitment.Hours != 0 ? commitment.Hours : commitmentFromRepo.Hours;
                    commitmentFromRepo.AssigmentId = commitment.AssigmentId != 0 ? commitment.AssigmentId : commitmentFromRepo.AssigmentId;

                    var commitmentUpdated = _repo.Update(commitmentFromRepo.CommitmentId, commitmentFromRepo);
                    return Ok(/*Mapper.Map<CommitmentViewModel>(*/commitmentUpdated/*)*/);
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Exception thrown white getting clients: {ex}");
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
                var commitmentToDel = _repo.GetById(id);
                await _repo.Delete(commitmentToDel.Id);

                return Ok($"Commitment deleted!");
            }
            catch (Exception)
            {

                return BadRequest($"Commitment wasn't deleted!");
            }
           
                
        }

    }
}