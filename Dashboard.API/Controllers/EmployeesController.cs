using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Data.EF.IRepository;
using Dashboard.Data.Controllers;
using Microsoft.Extensions.Logging;
using Dashboard.Data.Entities;
using Dashboard.Data.EF.Contracts;

namespace Dashboard.API.Controllers
{
    [Produces("application/json")]
    [Route("api/dashboard/[controller]")]
    public class EmployeesController : Controller
    {
        public IRepoEmployee _repo;
        private ILogger<EmployeesController> _logger;

        public EmployeesController(IRepoEmployee repo, 
            ILogger<EmployeesController> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        
        // GET api/dashboard/Commitments
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetAll<Employee>();
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
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result =  _repo.Get<Employee>(id);
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
        public async Task<IActionResult> Post([FromBody]Employee employee)
        {
            if (ModelState.IsValid)
            {
                //var newCommitment = Mapper.Map<Commitment>(commitment);
                var addedEmployee = await _repo.AddAsync(employee);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/dashboard/commitments/{addedEmployee.EmployeeId}", addedEmployee);
                }
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/dashboard/Commitments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Employee user)
        {
            if (ModelState.IsValid)
            {
                //var projectId = 0;
                //var userId = 0;
                var employeeFromRepo = _repo.Get<Employee>(id);
                //Mapper.Map(commitmentVM, commiFromRepo);

                employeeFromRepo.FirstName = user.FirstName ?? employeeFromRepo.FirstName;
                employeeFromRepo.LastName = user.LastName ?? employeeFromRepo.LastName;
                employeeFromRepo.PersonNr = user.PersonNr ?? employeeFromRepo.PersonNr;
                employeeFromRepo.PictureId = user.PictureId ?? employeeFromRepo.PictureId;
                employeeFromRepo.Picture = user.Picture ?? employeeFromRepo.Picture;

                var userUpdated = _repo.Update(employeeFromRepo);

                if (!await _repo.SaveChangesAsync())
                {
                    _logger.LogError($"Thrown exception when updating");
                    BadRequest("Something when wrong while updating");
                }
                return Ok(/*Mapper.Map<CommitmentViewModel>(*/userUpdated/*)*/);
            }
            return BadRequest("Error occured");

        }

        // DELETE api/dashboard/Commitments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employeeToDel = _repo.Get<Employee>(id);
            _repo.Delete(employeeToDel);
            if (await _repo.SaveChangesAsync())
                return Ok($"Commitment deleted!");
            else
                return BadRequest($"Commitment {employeeToDel.FirstName } wasn't deleted!");
        }

    }
}