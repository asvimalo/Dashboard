
using Dashboard.APIG.Models;
using Dashboard.DataG.EF.Contracts;

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
        private IRepoAssignment _repoAssignment;
        private ILogger<ProjectsController> _logger;
        //private IMapper _mapper;

        public ProjectsController(IRepoProject repoProject,
            IRepoClient repoClient,
            IRepoAssignment repoAssignment,
        ILogger<ProjectsController> logger/*,
            IMapper mapper*/)
        {
            _repoProject = repoProject;
            _repoClient = repoClient;
            _repoAssignment = repoAssignment;
            _logger = logger;
            //_mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        // GET api/values
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result =  _repoProject.Include(x => x.Assignments, y => y.Client, z => z.Phases);
                return Ok(result);
                //return Ok(_mapper.Map<IEnumerable<ProjectViewModel>>(result));
            }
            catch (Exception ex)
            {
                // LOGGING TODO
                _logger.LogError($"Exception thrown white getting projects: {ex}");
                return BadRequest($"Error ocurred");
            }
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetProject")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _repoProject.GetById(id);
                return Ok(result);
                //return Ok(_mapper.Map<ProjectViewModel>(result));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown while getting project: {ex}");
                return BadRequest($"Error ocurred");
            }

        }

        // POST api/values
        [HttpPost("")]
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
                   
                    var addedAssignment = await _repoAssignment.Create(new Assignment
                    {
                        ProjectId = addedProject.ProjectId, // products table
                        Project = addedProject,
                        EmployeeId = project.EmployeeId
                    });

                  
                    return Created($"api/dashboard/projects/{addedAssignment}", /*_mapper.Map<ProjectViewModel>(newProject)*/ addedAssignment);
                    //return Ok(_mapper.Map<ProjectViewModel>(result));
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Exception thrown while getting project: {ex}");
                    return BadRequest($"Error ocurred");
                }
                
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Project project)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var projectFromRepo = await _repoProject.GetById(id);
                    //_mapper.Map(projectVM, projectFromRepo);
                    var projectUpdated = _repoProject.Update(projectFromRepo.ProjectId, projectFromRepo);
                    return Ok(/*_mapper.Map<ProjectViewModel>(projectUpdated)*/projectUpdated);
                }
                catch (Exception)
                {

                    _logger.LogError($"Thrown exception when updating");
                    BadRequest("Something when wrong while updating");
                }
            }
            return BadRequest("Error occured");

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var projectToDel = await _repoProject.GetById(id);
                await _repoProject.Delete(projectToDel.ProjectId);

                return Ok($"Project deleted!");

            }
            catch (Exception)
            {

                return BadRequest($"Project wasn't deleted!");
            };
          
                
        }
    }

}
