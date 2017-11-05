
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
        public IRepoProject _repo;
        private ILogger<ProjectsController> _logger;
        //private IMapper _mapper;

        public ProjectsController(IRepoProject repo, 
            ILogger<ProjectsController> logger/*,
            IMapper mapper*/)
        {
            _repo = repo;
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
                var result =  _repo.Include(x => x.Assignments, y => y.Client, z => z.Phases);
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
                var result = await _repo.GetById(id);
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
        public async Task<IActionResult> Post([FromBody]Project project)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var addedProject =  _repo.Create(project);
                    return Created($"api/dashboard/projects/{addedProject.Id}", /*_mapper.Map<ProjectViewModel>(newProject)*/ addedProject);
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
                    var projectFromRepo = await _repo.GetById(id);
                    //_mapper.Map(projectVM, projectFromRepo);
                    var projectUpdated = _repo.Update(projectFromRepo.Id, projectFromRepo);
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
                var projectToDel = await _repo.GetById(id);
                await _repo.Delete(projectToDel.Id);

                return Ok($"Project deleted!");

            }
            catch (Exception)
            {

                return BadRequest($"Project wasn't deleted!");
            };
          
                
        }
    }

}
