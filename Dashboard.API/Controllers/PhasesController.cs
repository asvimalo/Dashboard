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
    [Route("api/dashboard/phases")]
    public class PhasesController : Controller
    {
        public IRepo _repo;
        private ILogger<PhasesController> _logger;

        public PhasesController(IRepo repo,
            ILogger<PhasesController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET api/dashboard/phases
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetAll<Phase>();
                return Ok(result);
                //return Ok(Mapper.Map<IEnumerable<CommitmentViewModel>>(result));
            }
            catch (Exception ex)
            {
                // LOGGING TODO
                _logger.LogError($"Exception thrown white getting phases: {ex}");
                return BadRequest($"Error ocurred");
            }
        }

        // GET api/dashboard/phases/5
        [HttpGet("{id}", Name = "GetPhase")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = _repo.Get<Phase>(id);
                return Ok(result);
                //return Ok(Mapper.Map<CommitmentViewModel>(result));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown while getting commitment: {ex}");
                return BadRequest($"Error ocurred");
            }

        }

        // POST api/dashboard/phases
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]Phase phase)
        {
            if (ModelState.IsValid)
            {
                //var newCommitment = Mapper.Map<Commitment>(commitment);
                var addedPhase = await _repo.AddAsync(phase);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/dashboard/commitments/{addedPhase.PhaseId}", addedPhase);
                }
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/dashboard/phases/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Phase phase)
        {
            if (ModelState.IsValid)
            {
                //var projectId = 0;
                //var userId = 0;
                var phaseFromRepo = _repo.Get<Phase>(id);
                //Mapper.Map(commitmentVM, commiFromRepo);

                phaseFromRepo.PhaseName = phase.PhaseName ?? phaseFromRepo.PhaseName;
                phaseFromRepo.Project = phase.Project ?? phaseFromRepo.Project;
                phaseFromRepo.Comments = phase.Comments ?? phaseFromRepo.Comments;
                phaseFromRepo.Tasks = phase.Tasks ?? phaseFromRepo.Tasks;
                phaseFromRepo.ProjectId = phase.ProjectId != 0 ? phase.ProjectId : phaseFromRepo.ProjectId;



                var phaseUpdated = _repo.Update(phaseFromRepo);

                if (!await _repo.SaveChangesAsync())
                {
                    _logger.LogError($"Thrown exception when updating");
                    BadRequest("Something when wrong while updating");
                }
                return Ok(/*Mapper.Map<CommitmentViewModel>(*/phaseFromRepo/*)*/);
            }
            return BadRequest("Error occured");

        }

        // DELETE api/dashboard/Commitments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var phaseToDel = _repo.Get<Phase>(id);
            _repo.Delete(phaseToDel);
            if (await _repo.SaveChangesAsync())
                return Ok($"Commitment deleted!");
            else
                return BadRequest($"Commitment {phaseToDel.PhaseName } wasn't deleted!");
        }

    }
}