using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Data.EF.Contracts;
using Dashboard.Data.Controllers;
using Microsoft.Extensions.Logging;
using Dashboard.Entities;

using Dashboard.Entities.Entities;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Dashboard.API.Controllers
{
    
    [Route("api/dashboard/[controller]")]
    public class EmployeesController : Controller
    {
        public IRepo _repo;
        private ILogger<EmployeesController> _logger;
        private IHostingEnvironment _env;

        public EmployeesController(IRepo repo, 
            ILogger<EmployeesController> logger,
            IHostingEnvironment env)
        {
            _repo = repo;
            _logger = logger;
            _env = env;
        }
        
        // GET api/dashboard/employees
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
        [HttpGet("{id}", Name = "GetEmployee")]
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

        // POST api/dashboard/employees
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]EmployeePost employee)
        {
            if (ModelState.IsValid)
            {
                var newEmployee = new Employee
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    PersonNr = employee.PersonNr,
                    Assignments = employee.Assignments,
                    AcquiredKnowledges = employee.AcquiredKnowledges
                    
                };
                #region Write picture to Image folder
                //var webRootPath = _env.WebRootPath;
                //var fileName = newEmployee.FirstName + ".jpg";
                //var filePath = Path.Combine($"{webRootPath}/Images/{fileName}");
                //await System.IO.File.WriteAllBytesAsync(filePath, employee.Bytes);


                //var newCommitment = Mapper.Map<Commitment>(commitment);
                //newEmployee.ImageName = fileName;
                //newEmployee.ImagePath = filePath; 
                #endregion

                var addedEmployee = await _repo.AddAsync(newEmployee);
                if (await _repo.SaveChangesAsync())
                {
                    return CreatedAtRoute("GetEmployee", new { id = addedEmployee.EmployeeId}, addedEmployee);
                }
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/dashboard/Commitments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]EmployeePost employee)
        {
            if (ModelState.IsValid)
            {               
                //var projectId = 0;
                //var userId = 0;
                var employeeFromRepo = _repo.Get<Employee>(id);
                //Mapper.Map(commitmentVM, commiFromRepo);
                if (employeeFromRepo == null)
                {
                    return NotFound();
                }
                
                //var webRootPath = _env.WebRootPath;               
                //var newCommitment = Mapper.Map<Commitment>(commitment);
                
                employeeFromRepo.FirstName = employee.FirstName ?? employeeFromRepo.FirstName;
                employeeFromRepo.LastName = employee.LastName ?? employeeFromRepo.LastName;
                employeeFromRepo.PersonNr = employee.PersonNr ?? employeeFromRepo.PersonNr;

                #region file handling
                //if(employee.File != null)
                //{
                //    System.IO.File.Delete(Path.Combine($"{webRootPath}/Images/{employeeFromRepo.FirstName}" + "jpg"));
                //    var newEmployee = new Employee
                //    {
                //        FirstName = employee.FirstName,
                //        LastName = employee.LastName,
                //        PersonNr = employee.PersonNr

                //    };
                //    var fileName = newEmployee.FirstName + ".jpg";
                //    var filePath = Path.Combine($"{webRootPath}/Images/{fileName}");

                //    //await System.IO.File.WriteAllBytesAsync(filePath, employee.Bytes);

                //    newEmployee.ImageName = fileName;
                //    newEmployee.ImagePath = filePath;
                //} 
                #endregion

                employeeFromRepo.Assignments = employee.Assignments ?? employeeFromRepo.Assignments;
                employeeFromRepo.AcquiredKnowledges = employee.AcquiredKnowledges ?? employeeFromRepo.AcquiredKnowledges;

                //employeeFromRepo.ImageName = employee.ImageName ?? employeeFromRepo.ImageName;
                //employeeFromRepo.ImagePath = employee.ImagePath ?? employeeFromRepo.ImagePath;
                //employeeFromRepo.Assignments = employee.Assignments ?? employeeFromRepo.Assignments;
                //employeeFromRepo.AcquiredKnowledges = employee.AcquiredKnowledge ?? employeeFromRepo.AcquiredKnowledges;

                var employeeUpdated = _repo.Update(employeeFromRepo);

                if (!await _repo.SaveChangesAsync())
                {
                    _logger.LogError($"Thrown exception when updating");
                    BadRequest("Something when wrong while updating");
                }
                return Ok(/*Mapper.Map<CommitmentViewModel>(*/employeeUpdated/*)*/);
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
                return Ok($"Employee deleted!");
            else
                return BadRequest($"Commitment {employeeToDel.FirstName } wasn't deleted!");
        }

    }
}