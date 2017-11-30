using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.DataG.Contracts;
using Microsoft.Extensions.Logging;
using Dashboard.EntitiesG.EntitiesRev;

namespace Dashboard.APIG.Controllers
{

    [Route("api/dashboard/JobTitleAssignments")]
    public class JobTitleAssignmentsController : Controller
    {
        public IRepoJobTitleAssignment _repo;
        private ILogger<JobTitleAssignmentsController> _logger;

        public JobTitleAssignmentsController(IRepoJobTitleAssignment repo,
            ILogger<JobTitleAssignmentsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET api/dashboard/jobTitleAssignments
        [HttpGet("")]
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
                _logger.LogError($"Exception thrown white getting JobTitleAssignments: {ex}");
                return BadRequest($"Error ocurred");
            }
        }

        // GET api/dashboard/jobTitleAssignments/5
        [HttpGet("{id}", Name = "GetJobTitleAssignment")]
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

                _logger.LogError($"Exception thrown while getting commitment: {ex}");
                return BadRequest($"Error ocurred");
            }

        }

        // POST api/dashboard/jobTitleAssignments
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]JobTitleAssignment jobTitleAssignment)
        {
            if (ModelState.IsValid)
            {
                //var newCommitment = Mapper.Map<Commitment>(commitment);
                try
                {
                    var addedJobTitleAssignment = _repo.Create(jobTitleAssignment);


                    return Ok(addedJobTitleAssignment);

                }
                catch (Exception ex)
                {

                    _logger.LogError($"Exception thrown white getting clients: {ex}");

                }
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/dashboard/jobTitleAssignments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]JobTitleAssignment jobTitleAssignment)
        {
            if (ModelState.IsValid)
            {
                //var projectId = 0;
                //var userId = 0;
                try
                {
                    var jobTitleAssignmentFromRepo = await _repo.GetById(id);
                    //Mapper.Map(commitmentVM, commiFromRepo);

                    jobTitleAssignmentFromRepo.Assignment = jobTitleAssignment.Assignment ?? jobTitleAssignmentFromRepo.Assignment;
                    jobTitleAssignmentFromRepo.JobTitleId = jobTitleAssignment.JobTitleId != 0 ? jobTitleAssignment.JobTitleId : jobTitleAssignmentFromRepo.JobTitleId;
                    jobTitleAssignmentFromRepo.AssignmentId = jobTitleAssignment.AssignmentId != 0 ? jobTitleAssignment.AssignmentId : jobTitleAssignmentFromRepo.AssignmentId;

                    var commitmentUpdated = _repo.Update(jobTitleAssignmentFromRepo.JobTitleAssignmentId, jobTitleAssignmentFromRepo);
                    return Ok(/*Mapper.Map<CommitmentViewModel>(*/commitmentUpdated/*)*/);
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Exception thrown white updating jobTitleAssignment: {ex}");
                    BadRequest("Something when wrong while updating");
                }

            }
            return BadRequest("Error occured");
        }

        // DELETE api/dashboard/jobTitleAssignments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var JobTitleAssignmentToDel = _repo.GetById(id);
                await _repo.Delete(JobTitleAssignmentToDel.Id);

                return Ok($"jobTitleAssignment deleted!");
            }
            catch (Exception)
            {

                return BadRequest($"jobTitleAssignment wasn't deleted!");
            }


        }

    }
}