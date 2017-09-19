using AutoMapper;
using Dashboard.Data.EF.IRepository;
using Dashboard.Data.Entities;
using Dashboard.Data.ViewModelsAPI;
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
        public IRepositoryDashboard _repo;
        private ILogger<ProjectsController> _logger;
        private IMapper _mapper;

        public ProjectsController(IRepositoryDashboard repo, 
            ILogger<ProjectsController> logger,
            IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
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
                var result = await _repo.GetProjects();
                return Ok(result);
                //return Ok(_mapper.Map<IEnumerable<ProjectViewModel>>(result));
            }
            catch (Exception ex)
            {
                // LOGGING TODO
                _logger.LogError($"Exception thrown white getting commitments: {ex}");
                return BadRequest($"Error ocurred");
            }
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetProject")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _repo.GetProject(id);
                return Ok(result);
                //return Ok(_mapper.Map<ProjectViewModel>(result));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown while getting commitment: {ex}");
                return BadRequest($"Error ocurred");
            }

        }

        // POST api/values
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]Project project)
        {
            if (ModelState.IsValid)
            {
                //var newProject = _mapper.Map<Project>(project);
                var addedProject = await _repo.AddAsync(project);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/dashboard/projects/{addedProject.Title}", /*_mapper.Map<ProjectViewModel>(newProject)*/ addedProject);
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
                var projectFromRepo = await _repo.GetProject(id);
                //_mapper.Map(projectVM, projectFromRepo);
                var projectUpdated = _repo.Update(projectFromRepo);
                if (!await _repo.SaveChangesAsync())
                {
                    _logger.LogError($"Thrown exception when updating");
                    BadRequest("Something when wrong while updating");
                }
                return Ok(/*_mapper.Map<ProjectViewModel>(projectUpdated)*/projectUpdated);
            }
            return BadRequest("Error occured");

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var projectToDel = await _repo.GetProject(id);
            _repo.Delete(projectToDel);
            if (await _repo.SaveChangesAsync())
                return Ok($"Project deleted!");
            else
                return BadRequest($"Project {projectToDel.Title} wasn't deleted!");
        }
    }

}
