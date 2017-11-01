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
    [Route("api/dashboard/knowledges")]
    public class KnowledgesController : Controller
    {
        public IRepo _repo;
        private ILogger<KnowledgesController> _logger;

        public KnowledgesController(IRepo repo,
            ILogger<KnowledgesController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET api/dashboard/knowledges
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetAll<Knowledge>();
                return Ok(result);
                //return Ok(Mapper.Map<IEnumerable<CommitmentViewModel>>(result));
            }
            catch (Exception ex)
            {
                // LOGGING TODO
                _logger.LogError($"Exception thrown white getting knowledges: {ex}");
                return BadRequest($"Error ocurred");
            }
        }

        // GET api/dashboard/Commitments/5
        [HttpGet("{id}", Name = "GetKnowledge")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = _repo.Get<Knowledge>(id);
                return Ok(result);
                //return Ok(Mapper.Map<CommitmentViewModel>(result));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown while getting knowledge: {ex}");
                return BadRequest($"Error ocurred");
            }

        }

        // POST api/dashboard/knowledges
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]Knowledge knowledge)
        {
            if (ModelState.IsValid)
            {
                //var newCommitment = Mapper.Map<Commitment>(commitment);
                var addedKnowledgee = await _repo.AddAsync(knowledge);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/dashboard/commitments/{addedKnowledgee.KnowledgeId}", addedKnowledgee);
                }
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/dashboard/knowledges/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Knowledge knowledge)
        {
            if (ModelState.IsValid)
            {
                //var projectId = 0;
                //var userId = 0;
                var knowledgeFromRepo = _repo.Get<Knowledge>(id);
                //Mapper.Map(commitmentVM, commiFromRepo);

                knowledgeFromRepo.KnowledgeName = knowledge.KnowledgeName ?? knowledgeFromRepo.KnowledgeName;
                knowledgeFromRepo.Description = knowledge.Description ?? knowledgeFromRepo.Description;
                knowledgeFromRepo.AcquiredKnowledges = knowledge.AcquiredKnowledges ?? knowledgeFromRepo.AcquiredKnowledges;
                

                var knowledgeUpdated = _repo.Update(knowledgeFromRepo);

                if (!await _repo.SaveChangesAsync())
                {
                    _logger.LogError($"Thrown exception when updating");
                    BadRequest("Something when wrong while updating");
                }
                return Ok(/*Mapper.Map<CommitmentViewModel>(*/knowledgeUpdated/*)*/);
            }
            return BadRequest("Error occured");

        }

        // DELETE api/dashboard/Commitments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var knowledgeToDel = _repo.Get<Knowledge>(id);
            _repo.Delete(knowledgeToDel);
            if (await _repo.SaveChangesAsync())
                return Ok($"Commitment deleted!");
            else
                return BadRequest($"Commitment {knowledgeToDel.KnowledgeName } wasn't deleted!");
        }

    }
}