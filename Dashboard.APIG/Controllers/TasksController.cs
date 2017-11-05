using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dashboard.DataG.EF.Contracts;
using Microsoft.Extensions.Logging;

namespace Dashboard.APIG.Controllers
{
    
    [Route("api/dashboard/tasks")]
    public class TasksController : Controller
    {
        public IRepoTask _repo;
        private ILogger<TasksController> _logger;

        public TasksController(IRepoTask repo,
            ILogger<TasksController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET api/dashboard/tasks
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result =  _repo.Include(x => x.Phase).ToList();
                return Ok(result);
                //return Ok(Mapper.Map<IEnumerable<CommitmentViewModel>>(result));
            }
            catch (Exception ex)
            {
                // LOGGING TODO
                _logger.LogError($"Exception thrown white getting tasks: {ex}");
                return BadRequest($"Error ocurred");
            }
        }

        // GET api/dashboard/tasks/5
        [HttpGet("{id}", Name = "GetTask")]
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

                _logger.LogError($"Exception thrown while getting task: {ex}");
                return BadRequest($"Error ocurred");
            }

        }

        // POST api/dashboard/tasks
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]EntitiesG.EntitiesRev.Task task)
        {
            if (ModelState.IsValid)
            {
                //var newCommitment = Mapper.Map<Commitment>(commitment);
                try
                {
                    var addedTask = _repo.Create(task);

                    return Created($"api/dashboard/tasks/{addedTask.Id}", addedTask);
                }
                catch (Exception)
                {

                    throw;
                }
               
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/dashboard/Commitments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]EntitiesG.EntitiesRev.Task task)
        {
            if (ModelState.IsValid)
            {
           
                try
                {
                    var taskFromRepo = await _repo.GetById(id);
                    //Mapper.Map(commitmentVM, commiFromRepo);

                    taskFromRepo.TaskName = task.TaskName ?? taskFromRepo.TaskName;
                    taskFromRepo.Phase = task.Phase ?? taskFromRepo.Phase;
                    taskFromRepo.PhaseId = task.PhaseId != 0 ? task.PhaseId : taskFromRepo.PhaseId;
                    var taskUpdated = _repo.Update(taskFromRepo.Id, taskFromRepo);
                    return Ok(/*Mapper.Map<CommitmentViewModel>(*/taskUpdated/*)*/);
                }
                catch (Exception)
                {

                    _logger.LogError($"Thrown exception when updating");
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
                var taskToDel = await _repo.GetById(id);
                await _repo.Delete(taskToDel.Id);

                return Ok($"Commitment deleted!");
            }
            catch (Exception)
            {

                return BadRequest($"Task wasn't deleted!");
            }
            
                
        }

    }
}