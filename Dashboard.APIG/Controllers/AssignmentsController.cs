using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.DataG.Contracts;
using Dashboard.EntitiesG.EntitiesRev;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Dashboard.APIG.Models;
using Dashboard.APIG.Infrastructure;

namespace Dashboard.DataG.Controllers
{
   
    [Route("api/dashboard/[controller]")]
    public class AssignmentsController : Controller
    {
        public IRepoProject _repoPro;
        public IRepoEmployee _repoEmp;
        public IRepoJobTitle _repoJob;
        public IRepoJobTitleAssignment _repoJobA;
        public IRepoAssignment _repo;
        private ILogger<AssignmentsController> _logger;

        public AssignmentsController(IRepoAssignment repo, 
            IRepoEmployee repoEmp,
            IRepoProject repoPro,
            IRepoJobTitle repoJob,
            IRepoJobTitleAssignment repoJobA,
            ILogger<AssignmentsController> logger)
        {
            _repoPro = repoPro;
            _repoEmp = repoEmp;
            _repoJob = repoJob;
            _repoJobA = repoJobA;
            _repo = repo;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        // GET api/dashboard/Assignments
        [HttpGet("")]
        [NoCache]
        [ProducesResponseType(typeof(List<Assignment>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Assignment>), 400)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result =  _repo.Include(x=>x.Commitments, y=> y.Employee, w => w.Project);
                return Ok(result);
                
            }
            catch (Exception ex)
            {
             
                _logger.LogError($"Exception thrown white getting commitments: {ex}");
                return BadRequest($"Error ocurred");
            }
        }
        [HttpGet("load")]
        [NoCache]
        [ProducesResponseType(typeof(List<Assignment>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Assignment>), 400)]
        public async Task<IActionResult> Load()
        {
            try
            {
                var result = _repo.GetAll();

                return Ok(result);
               
            }
            catch (Exception ex)
            {
                
                _logger.LogError($"Exception thrown white getting commitments: {ex}");
                return BadRequest($"Error ocurred");
            }
        }


        [HttpGet("{id}", Name = "GetEmployeeAssigment")]
        [NoCache]
        [ProducesResponseType(typeof(List<Project>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Project>), 400)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result =  await _repo.GetProjectsByEmployeeId(id);
                return Ok(result);
                
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown while getting projects: {ex}");
                return BadRequest($"Error ocurred");
            }

        }
         

        // POST api/dashboard/assigments
        [HttpPost("")]
        [ProducesResponseType(typeof(ApiResponse<Assignment>), 201)]
        [ProducesResponseType(typeof(ApiResponse<Assignment>), 400)]
        public async Task<IActionResult> Post([FromBody]AssignmentPost assignment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newAssignment = new Assignment
                    {
                        EmployeeId = assignment.EmployeeId,
                        ProjectId = assignment.ProjectId,
                        Location = assignment.Location,
                        Commitments = assignment.Commitments
                    };

                    var addedAssignment = await _repo.Create(newAssignment);

                    if (assignment.newJobTitles != null) {
                        foreach (var newJobTitle in assignment.newJobTitles)
                        {
                            var AddedJobTitle = await _repoJob.Create(new JobTitle
                            {
                                TitleName = newJobTitle
                            });

                            var addedJobTitleAssignment = await _repoJobA.Create(new JobTitleAssignment
                            {
                                AssignmentId = addedAssignment.AssignmentId,
                                JobTitleId = AddedJobTitle.JobTitleId
                            });
                        }
                    }

                    if (assignment.jobTitles != null) {
                        foreach (var jobTitle in assignment.jobTitles)
                        {
                            var addedJobTitleAssignment = await _repoJobA.Create(new JobTitleAssignment
                            {
                                AssignmentId = addedAssignment.AssignmentId,
                                JobTitleId = jobTitle.JobTitleId
                            });
                        }
                    }

                    return Ok(addedAssignment);
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Exception thrown while posting assignment: {ex.Message}");
                    return BadRequest($"Error ocurred");
                }
            }
            return BadRequest("Failed to save changes to the database");

        }

        // PUT api/dashboard/Commitments/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Assignment>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Assignment>), 400)]
        public async Task<IActionResult> Put(int id, [FromBody]Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                //var projectId = 0;
                //var employeeId = 0;
                var assignFromRepo = await _repo.GetAssignment(id);

                try
                {
                     
                    if (assignment.JobTitleAssignments != null)
                    {
                        var assignmentRepo = assignFromRepo.First();
                        var a = assignment.JobTitleAssignments.First().JobTitleId;

                        var result = assignmentRepo.JobTitleAssignments.FirstOrDefault(x => x.JobTitleId == a);

                        if (result == null)
                        {
                            assignmentRepo.JobTitleAssignments = assignment.JobTitleAssignments;
                            await _repoJobA.Delete(assignFromRepo.First().JobTitleAssignments.First().JobTitleAssignmentId);

                            assignmentRepo.ProjectId = assignment.ProjectId;
                            assignmentRepo.EmployeeId = assignment.EmployeeId;
                            assignmentRepo.Location = assignment.Location;
                            assignmentRepo.StartDate = assignment.StartDate;
                            assignmentRepo.StopDate = assignment.StopDate;

                            var AssignUpdated = _repo.Update(id, assignmentRepo);
                            return Ok(AssignUpdated);
                        }
                        else
                        {
                            return BadRequest($"You've already chosen that job title.");

                        }
                    }
                    else
                    {
                        var assignmentRepo = assignFromRepo.First();

                        assignmentRepo.ProjectId = assignment.ProjectId;
                        assignmentRepo.EmployeeId = assignment.EmployeeId;
                        assignmentRepo.Location = assignment.Location;
                        assignmentRepo.StartDate = assignment.StartDate;
                        assignmentRepo.StopDate = assignment.StopDate;

                        var AssignUpdated = _repo.Update(id, assignmentRepo);
                        return Ok(AssignUpdated);
                    }
                     

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
        [ProducesResponseType(typeof(ApiResponse<Assignment>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Assignment>), 400)]
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

        [HttpGet("projectsemployeeslist")]
        [NoCache]
        [ProducesResponseType(typeof(ProjectsEmployeesListNames), 200)]
        [ProducesResponseType(typeof(ApiResponse<ProjectsEmployeesListNames>), 400)]
        public async Task<IActionResult> GetProjectsEmployees()
        {
                try
                {
                    var employees =  _repoEmp.GetAll();
                    var projectsTmp = _repoPro.GetAll();

                    DateTime today = DateTime.Today;
                    var projects = new List<Project>();
                    foreach (Project p in projectsTmp) 
                    {
                        if(p.StopDate < today) 
                            continue;
                        projects.Add(p);
                    }

                    var both = new ProjectsEmployeesListNames { Employees = employees, Projects = projects.AsQueryable() };
                    return Ok(both);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Thrown exception when updating: {ex}");
                    return BadRequest($"Client wasn't deleted!");
                }
            
        }

    }
}
