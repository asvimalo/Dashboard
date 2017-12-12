using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dashboard.DataG.Contracts;
using Microsoft.Extensions.Logging;
using Dashboard.EntitiesG.EntitiesRev;
using Dashboard.APIG.Models;
using Dashboard.APIG.Infrastructure;

namespace Dashboard.API.Controllers
{
    
    [Route("api/dashboard/phases")]
    public class PhasesController : Controller
    {
        public IRepoPhase _repo;
        private ILogger<PhasesController> _logger;

        public PhasesController(IRepoPhase repo,
            ILogger<PhasesController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET api/dashboard/phases
        [HttpGet("")]
        [NoCache]
        [ProducesResponseType(typeof(List<Phase>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Phase>), 400)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result =  _repo.Include(x => x.Project, y => y.Tasks);
                return Ok(result);
                
            }
            catch (Exception ex)
            {
                // LOGGING TODO
                _logger.LogError($"Exception thrown white getting phases: {ex}");
                return BadRequest($"Error ocurred");
            }
        }

        // GET api/dashboard/phases/5
        [HttpGet("{id}")]
        [NoCache]
        [ProducesResponseType(typeof(Phase), 200)]
        [ProducesResponseType(typeof(ApiResponse<Phase>), 400)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = _repo.Include(x => x.Project, y => y.Tasks).First(x=>x.PhaseId == id);
                return Ok(result);
                
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown while getting commitment: {ex}");
                return BadRequest($"Error ocurred");
            }

        }

        // POST api/dashboard/phases
        [HttpPost("")]
        [ProducesResponseType(typeof(ApiResponse<Phase>), 201)]
        [ProducesResponseType(typeof(ApiResponse<Phase>), 400)]
        public async Task<IActionResult> Post([FromBody]Phase phase)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newPhase = new Phase
                    {
                        PhaseName = phase.PhaseName,
                        StartDate = phase.StartDate,
                        EndDate = phase.EndDate,
                        TimeBudget = phase.TimeBudget,
                        ProjectId = phase.ProjectId,
                        Comments = phase.Comments
                    };

                    var addedPhase = await _repo.Create(newPhase);
                    
                    return Ok(addedPhase);
                    
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Exception thrown while getting commitment: {ex}");
                    return BadRequest($"Error ocurred");
                }
                
                
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/dashboard/phases/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Phase>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Phase>), 400)]
        public async Task<IActionResult> Put(int id, [FromBody]Phase phase)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var phaseFromRepo = await _repo.GetById(id);
                    

                    phaseFromRepo.PhaseName = phase.PhaseName ?? phaseFromRepo.PhaseName;
                    phaseFromRepo.TimeBudget = phase.TimeBudget;
                    phaseFromRepo.Progress = phase.Progress;
                    phaseFromRepo.StartDate = phase.StartDate;
                    phaseFromRepo.EndDate = phase.EndDate;
                    phaseFromRepo.Comments = phase.Comments ?? phaseFromRepo.Comments;
                    phaseFromRepo.ProjectId = phase.ProjectId != 0 ? phase.ProjectId : phaseFromRepo.ProjectId;

                    var phaseUpdated = await _repo.Update(phaseFromRepo.PhaseId, phaseFromRepo);
                    return Ok(phaseUpdated);
                    
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Exception thrown while getting commitment: {ex}");
                    return BadRequest($"Error ocurred");
                }
               
            }
            return BadRequest("Error occured");

        }

        // DELETE api/dashboard/Commitments/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Phase>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Phase>), 400)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var phaseToDel = await _repo.GetById(id);
               await _repo.Delete(phaseToDel.PhaseId);

                return Ok(phaseToDel);
            }
            catch (Exception)
            {

                return BadRequest($"Phase  wasn't deleted!");
            }                        
        }

    }
}