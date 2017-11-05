using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.DataG.EF.Contracts;
using Dashboard.EntitiesG.EntitiesRev;


using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;


namespace Dashboard.DataG.Controllers
{
   
    [Route("api/dashboard/[controller]")]
    public class AssignmentsController : Controller
    {
        public IRepoAssignment _repo;
        private ILogger<AssignmentsController> _logger;

        public AssignmentsController(IRepoAssignment repo, 
            ILogger<AssignmentsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        // GET api/dashboard/Assignments
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = _repo.Include(x=>x.Commitments,y=> y.Employee, z => z.Location, w => w.Project);
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
        [HttpGet("{id}", Name = "GetAssigment")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result =  _repo.GetById(id);
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
        public async Task<IActionResult> Post([FromBody]Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                //var newCommitment = Mapper.Map<Commitment>(commitment);
                try
                {
                    var addedAssignment = _repo.Create(assignment);

                    return Created($"api/dashboard/assignments/{addedAssignment.Id}", addedAssignment);
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Thrown exception when updating: {ex}");
                }
                
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/dashboard/Commitments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                var projectId = 0;
                var employeeId = 0;
                var assignFromRepo = await _repo.GetById(id);

                try
                {
                    assignFromRepo.ProjectId = projectId;
                    Int32.TryParse((assignment.ProjectId.ToString() ?? assignFromRepo.ProjectId.ToString()), out projectId);
                    assignFromRepo.EmployeeId = employeeId;
                    Int32.TryParse((assignment.EmployeeId.ToString() ?? assignFromRepo.EmployeeId.ToString()), out employeeId);


                    var AssignUpdated = _repo.Update(assignFromRepo.AssignmentId, assignFromRepo);
                    return Ok(/*Mapper.Map<CommitmentViewModel>(*/AssignUpdated/*)*/);

                }
                catch (Exception ex)
                {

                    _logger.LogError($"Thrown exception when updating: {ex}");                   
                    BadRequest("Something when wrong while updating");
                }
                
                //Mapper.Map(commitmentVM, commiFromRepo);
                //assignFromRepo.Location = assignment.Location ?? assignFromRepo.Location;
                //assignFromRepo.JobTitle = assignment.JobTitle ?? assignFromRepo.JobTitle;
                
                
                   
               
                
            }
            return BadRequest("Error occured");
            
        }

        // DELETE api/dashboard/Assignments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var assingmentToDel = await _repo.GetById(id);
            if (assingmentToDel != null)
            {
                try
                {
                    await _repo.Delete(assingmentToDel.AssignmentId);
                    return Ok($"Commitment deleted!");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Thrown exception when updating: {ex}");
                }
            }
            return BadRequest($"Client {assingmentToDel.AssignmentId } wasn't deleted!");
        }

        
    }
}
