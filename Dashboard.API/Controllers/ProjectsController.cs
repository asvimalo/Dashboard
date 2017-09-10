using AutoMapper;
using Dashboard.API.EF.IRepository;
using Dashboard.Data.Entities;
using Dashboard.Data.ViewModel;
using Dashboard.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.Controllers
{

    [Route("api/dashboard/[controller]")]
    public class ProjectsController : Controller
    {
        public IRepository<Project> _repo;
        private ILogger<ProjectsController> _logger;

        public ProjectsController(IRepository<Project> repo, 
            ILogger<ProjectsController> logger)
        {
            _repo = repo;
            _logger = logger;
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
                var result = await _repo.GetAll();

                return Ok(Mapper.Map<IEnumerable<ProjectViewModel>>(result));
            }
            catch (Exception ex)
            {
                // LOGGING TODO
                _logger.LogError($"Exception thrown white getting commitments: {ex}");
                return BadRequest($"Error ocurred");
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _repo.Get(id);
                return Ok(Mapper.Map<ProjectViewModel>(result));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown while getting commitment: {ex}");
                return BadRequest($"Error ocurred");
            }

        }

        // POST api/values
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                var newProject = Mapper.Map<Project>(project);
                _repo.Add(newProject);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/dashboard/projects/{project.Title}", Mapper.Map<ProjectViewModel>(newProject));
                }
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]ProjectViewModel projectVM)
        {
            if (ModelState.IsValid)
            {
                var projectFromRepo = await _repo.Get(id);
                Mapper.Map(projectVM, projectFromRepo);
                var projectUpdated = _repo.Update(projectFromRepo);
                if (!await _repo.SaveChangesAsync())
                {
                    _logger.LogError($"Thrown exception when updating");
                    BadRequest("Something when wrong while updating");
                }
                return Ok(Mapper.Map<ProjectViewModel>(projectUpdated));
            }
            return BadRequest("Error occured");

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var projectToDel = await _repo.Get(id);
            _repo.Delete(projectToDel);
            if (await _repo.SaveChangesAsync())
                return Ok($"Project deleted!");
            else
                return BadRequest($"Project {projectToDel.Title} wasn't deleted!");
        }
    }

}
