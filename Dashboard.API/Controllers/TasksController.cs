using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Data.EF.Contracts;
using Microsoft.Extensions.Logging;

namespace Dashboard.API.Controllers
{
    [Produces("application/json")]
    [Route("api/dashboard/tasks")]
    public class TasksController : Controller
    {
        public IRepo _repo;
        private ILogger<TasksController> _logger;

        public TasksController(IRepo repo,
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
                var result = await _repo.GetAll<Entities.Task>();
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
                var result = _repo.Get<Entities.Task>(id);
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
        public async Task<IActionResult> Post([FromBody]Entities.Task task)
        {
            if (ModelState.IsValid)
            {
                //var newCommitment = Mapper.Map<Commitment>(commitment);
                var addedTask = await _repo.AddAsync(task);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/dashboard/tasks/{addedTask.TaskId}", addedTask);
                }
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/dashboard/Commitments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Entities.Task task)
        {
            if (ModelState.IsValid)
            {
                //var projectId = 0;
                //var userId = 0;
                var taskFromRepo = _repo.Get<Entities.Task>(id);
                //Mapper.Map(commitmentVM, commiFromRepo);

                taskFromRepo.TaskName = task.TaskName ?? taskFromRepo.TaskName;
                taskFromRepo.Phase = task.Phase ?? taskFromRepo.Phase;
                taskFromRepo.PhaseId = task.PhaseId != 0 ? task.PhaseId : taskFromRepo.PhaseId;
                

                var taskUpdated = _repo.Update(taskFromRepo);

                if (!await _repo.SaveChangesAsync())
                {
                    _logger.LogError($"Thrown exception when updating");
                    BadRequest("Something when wrong while updating");
                }
                return Ok(/*Mapper.Map<CommitmentViewModel>(*/taskUpdated/*)*/);
            }
            return BadRequest("Error occured");

        }

        // DELETE api/dashboard/Commitments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var taskToDel = _repo.Get<Entities.Task>(id);
            _repo.Delete(taskToDel);
            if (await _repo.SaveChangesAsync())
                return Ok($"Commitment deleted!");
            else
                return BadRequest($"Commitment {taskToDel.TaskName } wasn't deleted!");
        }

    }
}