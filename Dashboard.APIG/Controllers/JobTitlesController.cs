using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.DataG.Contracts;
using Microsoft.Extensions.Logging;
using Dashboard.EntitiesG.EntitiesRev;
using System.Linq;
using Dashboard.APIG.Infrastructure;
using System.Collections.Generic;
using Dashboard.APIG.Models;

namespace Dashboard.APIG.Controllers
{

    [Route("api/dashboard/[controller]")]
    public class JobTitlesController : Controller
    {
        public IRepoJobTitle _repo;
        private ILogger<JobTitlesController> _logger;

        public JobTitlesController(IRepoJobTitle repo,
            ILogger<JobTitlesController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET api/dashboard/jobTitleAssignments
        [HttpGet("")]
        [NoCache]
        [ProducesResponseType(typeof(List<JobTitle>), 200)]
        [ProducesResponseType(typeof(ApiResponse<JobTitle>), 400)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = _repo.getAllOfThem();

                ;
                return Ok(result);

            }
            catch (Exception ex)
            {
                // LOGGING TODO
                _logger.LogError($"Exception thrown white getting JobTitles: {ex}");
                return BadRequest($"Error ocurred");
            }
        }

        // GET api/dashboard/jobTitles/5
        [HttpGet("{id}", Name = "GetJobTitle")]
        [NoCache]
        [ProducesResponseType(typeof(JobTitle), 200)]
        [ProducesResponseType(typeof(ApiResponse<JobTitle>), 400)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _repo.GetById(id);
                return Ok(result);
               
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown while getting JobTitle: {ex}");
                return BadRequest($"Error ocurred");
            }

        }

        // POST api/dashboard/jobTitles
        [HttpPost("")]
        [ProducesResponseType(typeof(ApiResponse<JobTitle>), 201)]
        [ProducesResponseType(typeof(ApiResponse<JobTitle>), 400)]
        public async Task<IActionResult> Post([FromBody]JobTitle jobTitle)
        {
            if (ModelState.IsValid)
            {
                
                try
                {
                    var result = _repo.Include(x => x.JobTitleAssignments).FirstOrDefault(t => t.TitleName == jobTitle.TitleName);

                    if (result != null)
                    {
                        return BadRequest("Failed to save changes to the database");
                    }
                    else
                    {
                        var addedJobTitle = _repo.Create(jobTitle);

                        return Ok(addedJobTitle);
                    }
                     
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Exception thrown white getting JobTitles: {ex}");

                }
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/dashboard/jobTitles/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<JobTitle>), 200)]
        [ProducesResponseType(typeof(ApiResponse<JobTitle>), 400)]
        public async Task<IActionResult> Put(int id, [FromBody]JobTitle jobTitle)
        {
            if (ModelState.IsValid)
            {
                //var projectId = 0;
                //var userId = 0;
                try
                {
                    var jobTitleFromRepo = await _repo.GetById(id);
                    //Mapper.Map(commitmentVM, commiFromRepo);

                    jobTitleFromRepo.TitleName = jobTitle.TitleName ?? jobTitleFromRepo.TitleName;
                                       
                    var jobTitleUpdated = _repo.Update(jobTitleFromRepo.JobTitleId, jobTitleFromRepo);
                    return Ok(jobTitleUpdated);
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Exception thrown white updating jobTitle: {ex}");
                    BadRequest("Something when wrong while updating");
                }

            }
            return BadRequest("Error occured");
        }

        // DELETE api/dashboard/jobTitleAssignments/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<JobTitle>), 200)]
        [ProducesResponseType(typeof(ApiResponse<JobTitle>), 400)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var JobTitleToDel = _repo.GetById(id);
                await _repo.Delete(JobTitleToDel.Id);

                return Ok($"jobTitle deleted!");
            }
            catch (Exception)
            {

                return BadRequest($"jobTitle wasn't deleted!");
            }


        }

    }
}