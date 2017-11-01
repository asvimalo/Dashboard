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
    [Produces("application/json")]
    [Route("api/dashboard/commitments")]
    public class CommitmentsController : Controller
    {
        public IRepo _repo;
        private ILogger<CommitmentsController> _logger;

        public CommitmentsController(IRepo repo,
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
                var result = await _repo.GetAll<Commitment>();
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
                var result = _repo.Get<Commitment>(id);
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
                var addedCommitment = await _repo.AddAsync(commitment);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/dashboard/commitments/{addedCommitment.CommitmentId}", addedCommitment);
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
                var commitmentFromRepo = _repo.Get<Commitment>(id);
                //Mapper.Map(commitmentVM, commiFromRepo);

                commitmentFromRepo.Assignment = commitment.Assignment ?? commitmentFromRepo.Assignment;
                commitmentFromRepo.Hours = commitment.Hours != 0 ? commitment.Hours : commitmentFromRepo.Hours;
                commitmentFromRepo.AssigmentId = commitment.AssigmentId != 0 ? commitment.AssigmentId : commitmentFromRepo.AssigmentId;

                var commitmentUpdated = _repo.Update(commitmentFromRepo);

                if (!await _repo.SaveChangesAsync())
                {
                    _logger.LogError($"Thrown exception when updating");
                    BadRequest("Something when wrong while updating");
                }
                return Ok(/*Mapper.Map<CommitmentViewModel>(*/commitmentUpdated/*)*/);
            }
            return BadRequest("Error occured");

        }

        // DELETE api/dashboard/Commitments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var commitmentToDel = _repo.Get<Commitment>(id);
            _repo.Delete(commitmentToDel);
            if (await _repo.SaveChangesAsync())
                return Ok($"Commitment deleted!");
            else
                return BadRequest($"Commitment {commitmentToDel.CommitmentId } wasn't deleted!");
        }

    }
}