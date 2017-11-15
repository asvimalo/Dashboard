using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dashboard.DataG.Contracts;
using Microsoft.Extensions.Logging;
using Dashboard.EntitiesG.EntitiesRev;

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
        public async Task<IActionResult> Get()
        {
            try
            {
                var result =  _repo.Include(x => x.Project, y => y.Tasks);
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
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = _repo.Include(x => x.Project, y => y.Tasks).First(x=>x.PhaseId == id);
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
                try
                {
                    var addedPhase = _repo.Create(phase);
                    return Created($"api/dashboard/commitments/{addedPhase.Id}", addedPhase);
                    //return Ok(Mapper.Map<CommitmentViewModel>(result));
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Exception thrown while getting commitment: {ex}");
                    return BadRequest($"Error ocurred");
                }
                //var newCommitment = Mapper.Map<Commitment>(commitment);
                
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/dashboard/phases/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Phase phase)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var phaseFromRepo = await _repo.GetById(id);
                    //Mapper.Map(commitmentVM, commiFromRepo);

                    phaseFromRepo.PhaseName = phase.PhaseName ?? phaseFromRepo.PhaseName;
                    phaseFromRepo.Project = phase.Project ?? phaseFromRepo.Project;
                    phaseFromRepo.Comments = phase.Comments ?? phaseFromRepo.Comments;
                    phaseFromRepo.Tasks = phase.Tasks ?? phaseFromRepo.Tasks;
                    phaseFromRepo.ProjectId = phase.ProjectId != 0 ? phase.ProjectId : phaseFromRepo.ProjectId;



                    var phaseUpdated = _repo.Update(phaseFromRepo.PhaseId, phaseFromRepo);
                    return Ok(/*Mapper.Map<CommitmentViewModel>(*/phaseFromRepo/*)*/);
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Exception thrown while getting commitment: {ex}");
                    return BadRequest($"Error ocurred");
                }
                //var projectId = 0;
                //var userId = 0;
               

               
               
               
            }
            return BadRequest("Error occured");

        }

        // DELETE api/dashboard/Commitments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var phaseToDel = await _repo.GetById(id);
               await _repo.Delete(phaseToDel.PhaseId);

                return Ok($"Commitment deleted!");
            }
            catch (Exception)
            {

                return BadRequest($"Phase  wasn't deleted!");
            }                        
        }

    }
}