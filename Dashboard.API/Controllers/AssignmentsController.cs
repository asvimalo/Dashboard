using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Data.EF.IRepository;
using Dashboard.Entities;
using Dashboard.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Dashboard.Data.EF.Contracts;

namespace Dashboard.Data.Controllers
{
    [Produces("application/json")]
    [Route("api/dashboard/[controller]")]
    public class AssignmentsController : Controller
    {
        public IRepo _repo;
        private ILogger<AssignmentsController> _logger;

        public AssignmentsController(IRepo repo, 
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
                var result = await _repo.GetAll<Assignment>();
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
                var result =  _repo.Get<Assignment>(id);
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
                var addedAssignment =  await _repo.AddAsync(assignment);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/dashboard/assignments/{addedAssignment.AssignmentId}", addedAssignment);
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
                var assignFromRepo =  _repo.Get<Assignment>(id);
                //Mapper.Map(commitmentVM, commiFromRepo);
                //assignFromRepo.Location = assignment.Location ?? assignFromRepo.Location;
                //assignFromRepo.JobTitle = assignment.JobTitle ?? assignFromRepo.JobTitle;
                assignFromRepo.ProjectId = projectId;
                Int32.TryParse((assignment.ProjectId.ToString() ?? assignFromRepo.ProjectId.ToString()), out projectId);
                assignFromRepo.EmployeeId = employeeId;
                Int32.TryParse((assignment.EmployeeId.ToString() ?? assignFromRepo.EmployeeId.ToString()), out employeeId);             
                

                var commitUpdated =  _repo.Update(assignFromRepo);
                if (!await _repo.SaveChangesAsync())
                {
                    _logger.LogError($"Thrown exception when updating");
                    BadRequest("Something when wrong while updating");
                }
                return Ok(/*Mapper.Map<CommitmentViewModel>(*/commitUpdated/*)*/); 
            }
            return BadRequest("Error occured");
            
        }

        // DELETE api/dashboard/Assignments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var assignmentToDel = _repo.Get<Assignment>(id);
            _repo.Delete(assignmentToDel);
            if (await _repo.SaveChangesAsync())
                return Ok($"Assignment deleted!");
            else
                return BadRequest($"Assignment {assignmentToDel.AssignmentId} wasn't deleted!");
        }

        
    }
}
