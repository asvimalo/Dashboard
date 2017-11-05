using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dashboard.DataG.EF.Contracts;
using Dashboard.Data.Controllers;
using Microsoft.Extensions.Logging;
using Dashboard.EntitiesG.EntitiesRev;


using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Dashboard.APIG.Controllers
{
    
    [Route("api/dashboard/[controller]")]
    public class EmployeesController : Controller
    {
        public IRepoEmployee _repo;
        private ILogger<EmployeesController> _logger;
        private IHostingEnvironment _env;

        public EmployeesController(IRepoEmployee repo, 
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
                var result = _repo.Include(x => x.AcquiredKnowledges, y => y.Assignments);

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
                var result =  _repo.GetById(id);
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
                try
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

                    var addedEmployee =  _repo.Create(newEmployee);
                    return CreatedAtRoute("GetEmployee", new { id = addedEmployee.Id }, addedEmployee);
                  
                    //return Ok(Mapper.Map<CommitmentViewModel>(result));
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Exception thrown while getting commitment: {ex}");
                    return BadRequest($"Error ocurred");
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
                var employeeFromRepo = await _repo.GetById(id);
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
                try
                {
                    var employeeUpdated = _repo.Update(employeeFromRepo.Id, employeeFromRepo);
                    return Ok(/*Mapper.Map<CommitmentViewModel>(*/employeeUpdated/*)*/);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Thrown exception when updating {ex}");
                    BadRequest("Something when wrong while updating"); 
                }    
            }
            return BadRequest("Error occured");

        }

        // DELETE api/dashboard/Commitments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var employeeToDel = await _repo.GetById(id);
                await _repo.Delete(employeeToDel.Id);

                return Ok($"Employee deleted!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Thrown exception when updating {ex}");
                return BadRequest($"Employee  wasn't deleted! ");
            }
           
                
        }

    }
}