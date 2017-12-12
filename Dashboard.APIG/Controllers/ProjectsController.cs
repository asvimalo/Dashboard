
using Dashboard.APIG.Infrastructure;
using Dashboard.APIG.Models;
using Dashboard.DataG.Contracts;

using Dashboard.EntitiesG.EntitiesRev;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Data.Controllers
{
    
    [Route("api/dashboard/[controller]")]
    public class ProjectsController : Controller
    {
        public IRepoProject _repoProject;
        private IRepoClient _repoClient;
        private IRepoEmployee _repoEmp;
        private IRepoAssignment _repoAssignment;
        private ILogger<ProjectsController> _logger;
        //private IMapper _mapper;

        public ProjectsController(IRepoProject repoProject,
            IRepoClient repoClient,
            IRepoAssignment repoAssignment,
            IRepoEmployee repoEmp,
        ILogger<ProjectsController> logger
            )
        {
            _repoEmp = repoEmp;
            _repoProject = repoProject;
            _repoClient = repoClient;
            _repoAssignment = repoAssignment;
            _logger = logger;
           
        }
        
      
        [HttpGet("")]
        [NoCache]
        [ProducesResponseType(typeof(List<Project>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Project>), 400)]
        public async Task<IActionResult> Get()
        {
            try
            {
                
                var result = _repoProject.GetAllForReal();
                return Ok(result);
               
            }
            catch (Exception ex)
            {
                 
                _logger.LogError($"Exception thrown white getting projects: {ex}");
                return BadRequest($"Error ocurred");
            }
        }

       
        [HttpGet("{id}", Name = "GetProject")]
        [NoCache]
        [ProducesResponseType(typeof(Project), 200)]
        [ProducesResponseType(typeof(ApiResponse<Project>), 400)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _repoProject.GetProjectById(id);
                return Ok(result.First());
                
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown while getting project: {ex}");
                return BadRequest($"Error ocurred");
            }

        }

        // POST api/values
        [HttpPost("")]
        [ProducesResponseType(typeof(ApiResponse<Project>), 201)]
        [ProducesResponseType(typeof(ApiResponse<Project>), 400)]
        public async Task<IActionResult> Post([FromBody]ProjectForm project)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newProject = new Project
                    {
                        ProjectName = project.ProjectName,
                        StartDate = project.StartDate,
                        StopDate = project.StopDate,
                        TimeBudget = project.TimeBudget,
                        Notes = project.Notes,
                        ClientId = project.ClientId
                    };
                    
                    var addedProject = await _repoProject.Create(newProject);

                   
                    return Ok(addedProject);
                        
                   
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Exception thrown while getting project: {ex}");
                    return BadRequest($"Error ocurred");
                }
                
            }
            return BadRequest("Failed to save changes to the database");
        }

        
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Project>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Project>), 400)]
        public async Task<IActionResult> Put(int id, [FromBody]Project project)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var projectFromRepo = await _repoProject.GetById(id);
   

                    projectFromRepo.ProjectName = project.ProjectName ?? projectFromRepo.ProjectName;
                    projectFromRepo.StartDate = project.StartDate;
                    projectFromRepo.StopDate = project.StopDate;
                    projectFromRepo.TimeBudget = project.TimeBudget;
                    projectFromRepo.Notes = project.Notes ?? projectFromRepo.Notes;

                    var projectUpdated = _repoProject.Update(projectFromRepo.ProjectId, projectFromRepo);
                    return Ok(projectUpdated);
                    
                }
                catch (Exception)
                {

                    _logger.LogError($"Thrown exception when updating");
                    BadRequest("Something when wrong while updating");
                }
            }
            return BadRequest("Error occured");

        }

 
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Project>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Project>), 400)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var projectToDel = await _repoProject.GetById(id);
                await _repoProject.Delete(projectToDel.ProjectId);

                return Ok(projectToDel);

            }
            catch (Exception)
            {

                return BadRequest($"Project wasn't deleted!");
            };
          
                
        }
        [HttpGet("employeesclientslist")]
        [NoCache]
        [ProducesResponseType(typeof(ClientsEmployeesListNames), 200)]
        [ProducesResponseType(typeof(ApiResponse<ClientsEmployeesListNames>), 400)]
        public async Task<IActionResult> GetList(int id)
        {
            try
            {
                var employees = _repoEmp.GetAll();
                var clients = _repoClient.GetAll();
                var both = new ClientsEmployeesListNames { Employees = employees, Clients = clients };
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
