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
    [Route("api/dashboard/acquiredKnowledges")]
    public class AcquiredKnowledgesController : Controller
    {
        public IRepo _repo;
        private ILogger<AcquiredKnowledgesController> _logger;

        public AcquiredKnowledgesController(IRepo repo,
            ILogger<AcquiredKnowledgesController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET api/dashboard/acquiredKnowledges
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetAll<AcquiredKnowledge>();
                return Ok(result);
                //return Ok(Mapper.Map<IEnumerable<CommitmentViewModel>>(result));
            }
            catch (Exception ex)
            {
                // LOGGING TODO
                _logger.LogError($"Exception thrown white getting Acquired Knowledges: {ex}");
                return BadRequest($"Error ocurred");
            }
        }

        // GET api/dashboard/acquiredKnowledges/5
        [HttpGet("{id}", Name = "GetAcquiredKnowledge")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = _repo.Get<AcquiredKnowledge>(id);
                return Ok(result);
                //return Ok(Mapper.Map<CommitmentViewModel>(result));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown while getting Acquired Knowledge: {ex}");
                return BadRequest($"Error ocurred");
            }

        }

        // POST api/dashboard/acquiredKnowledges
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]AcquiredKnowledge acquiredKnowledge)
        {
            if (ModelState.IsValid)
            {
                //var newCommitment = Mapper.Map<Commitment>(commitment);
                var addedacquiredKnowledge = await _repo.AddAsync(acquiredKnowledge);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/dashboard/commitments/{addedacquiredKnowledge.EmployeeId}", addedacquiredKnowledge);
                }
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/dashboard/addedacquiredKnowledges/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]AcquiredKnowledge acquiredKnowledge)
        {
            if (ModelState.IsValid)
            {
                //var projectId = 0;
                //var userId = 0;
                var acquiredKnowledgeFromRepo = _repo.Get<AcquiredKnowledge>(id);
                //Mapper.Map(commitmentVM, commiFromRepo);

                acquiredKnowledgeFromRepo.Employee = acquiredKnowledge.Employee ?? acquiredKnowledgeFromRepo.Employee;
                acquiredKnowledgeFromRepo.EmployeeId = acquiredKnowledge.EmployeeId != 0 ? acquiredKnowledge.EmployeeId : acquiredKnowledgeFromRepo.EmployeeId;
                acquiredKnowledgeFromRepo.Knowledge = acquiredKnowledge.Knowledge ?? acquiredKnowledgeFromRepo.Knowledge;
                acquiredKnowledgeFromRepo.KnowledgeId = acquiredKnowledge.KnowledgeId != 0 ? acquiredKnowledge.KnowledgeId : acquiredKnowledgeFromRepo.KnowledgeId;


                var acquiredKnowledgeUpdated = _repo.Update(acquiredKnowledgeFromRepo);

                if (!await _repo.SaveChangesAsync())
                {
                    _logger.LogError($"Thrown exception when updating");
                    BadRequest("Something when wrong while updating");
                }
                return Ok(/*Mapper.Map<CommitmentViewModel>(*/acquiredKnowledgeUpdated/*)*/);
            }
            return BadRequest("Error occured");

        }

        // DELETE api/dashboard/Commitments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var acquiredKnowledgeToDel = _repo.Get<AcquiredKnowledge>(id);
            _repo.Delete(acquiredKnowledgeToDel);
            if (await _repo.SaveChangesAsync())
                return Ok($"AcquiredKnowledge deleted!");
            else
                return BadRequest($"AcquiredKnowledge {acquiredKnowledgeToDel.AcquiredKnowledgeId } wasn't deleted!");
        }

    }
}