using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.DataG.Contracts;
using Microsoft.Extensions.Logging;
using Dashboard.EntitiesG.EntitiesRev;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;
using Dashboard.APIG.Helpers;
using Dashboard.APIG.Infrastructure;
using System.Collections.Generic;
using Dashboard.APIG.Models;

namespace Dashboard.APIG.Controllers
{
    
    [Route("api/dashboard/[controller]")]
    public class EmployeesController : Controller
    {
        public IRepoEmployee _repo;
        private IRepoEmployee _empRepo;
        private ILogger<EmployeesController> _logger;
        private IHostingEnvironment _env;
        private IRepoKnowledge _KnRepo;
        private IRepoAcquiredKnowledge _AcqRepo;

        public EmployeesController(IRepoEmployee empRepo, 
            IRepoAcquiredKnowledge AcqRepo,
            IRepoKnowledge KnRepo,
            ILogger<EmployeesController> logger,
            IHostingEnvironment env)
        {
            _empRepo = empRepo;
            _logger = logger;
            _env = env;
            _KnRepo = KnRepo;
            _AcqRepo = AcqRepo;
        }
        
        // GET api/dashboard/employees
        [HttpGet("")]
        [NoCache]
        [ProducesResponseType(typeof(List<Employee>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Employee>), 400)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = _empRepo.Include(x => x.AcquiredKnowledges, y => y.Assignments);

                return Ok(result);
                
            }
            catch (Exception ex)
            {
                // LOGGING TODO
                _logger.LogError($"Exception thrown white getting commitments: {ex}");
                return BadRequest($"Error ocurred");
            }
        }
        [HttpGet("load")]
        [NoCache]
        [ProducesResponseType(typeof(List<Employee>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Employee>), 400)]
        public async Task<IActionResult> Load()
        {
            try
            {
                var result = _empRepo.GetAll();

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
        [NoCache]
        [ProducesResponseType(typeof(Employee), 200)]
        [ProducesResponseType(typeof(ApiResponse<Employee>), 400)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = _empRepo.Include(a => a.AcquiredKnowledges, x => x.Assignments).Where(x => x.EmployeeId == id).First();
                return Ok(result);
                
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown while getting commitment: {ex}");
                return BadRequest($"Error ocurred");
            }

        }

        // POST api/dashboard/employees
        [HttpPost("")]
        [ProducesResponseType(typeof(ApiResponse<Employee>), 201)]
        [ProducesResponseType(typeof(ApiResponse<Employee>), 400)]
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
                        PersonNr = employee.PersonNr
                    };
                     
                    var addedEmployee = await _empRepo.Create(newEmployee);
                                        

                    if (employee.knowledges == null && employee.newKnowledges != null)
                        foreach (var newKnowledge in employee.newKnowledges)
                        {
                            var AddedKnowledges = await _KnRepo.Create(new Knowledge
                            {
                                KnowledgeName = newKnowledge
                            });

                            var addedAssignment = await _AcqRepo.Create(new AcquiredKnowledge
                            {
                                EmployeeId = addedEmployee.EmployeeId,
                                KnowledgeId = AddedKnowledges.KnowledgeId
                            });
                        }
                    else if (employee.knowledges != null && employee.newKnowledges == null)
                    {
                        foreach (var knowledge in employee.knowledges)
                        {
                            var addedAssignment = await _AcqRepo.Create(new AcquiredKnowledge
                            {
                                EmployeeId = addedEmployee.EmployeeId,
                                KnowledgeId = knowledge.KnowledgeId
                            });
                        }
                    }
                    else if (employee.knowledges != null && employee.newKnowledges != null)
                    {
                        foreach (var newKnowledge in employee.newKnowledges)
                        {
                            var AddedKnowledges = await _KnRepo.Create(new Knowledge
                            {
                                KnowledgeName = newKnowledge
                            });

                            var addedAssignment = await _AcqRepo.Create(new AcquiredKnowledge
                            {
                                EmployeeId = addedEmployee.EmployeeId,
                                KnowledgeId = AddedKnowledges.KnowledgeId
                            });
                        }
                        foreach (var knowledge in employee.knowledges)
                        {
                            var addedAssignment = await _AcqRepo.Create(new AcquiredKnowledge
                            {
                                EmployeeId = addedEmployee.EmployeeId,
                                KnowledgeId = knowledge.KnowledgeId
                            });
                        }
                    }

                    return Ok(addedEmployee);
                    
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Exception thrown while adding employee: {ex.Message}");
                    return BadRequest($"Error ocurred");
                }
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/dashboard/employees/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Employee>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Employee>), 400)]
        public async Task<IActionResult> Put(int id, [FromBody]EmployeePost employee)
        {
            if (ModelState.IsValid)
            {   
                var employeeFromRepo = await _empRepo.GetById(id);
                
                if (employeeFromRepo == null)
                {
                    return NotFound();
                }
                
                
                
                employeeFromRepo.FirstName = employee.FirstName ?? employeeFromRepo.FirstName;
                employeeFromRepo.LastName = employee.LastName ?? employeeFromRepo.LastName;
                employeeFromRepo.PersonNr = employee.PersonNr ?? employeeFromRepo.PersonNr;

              
                try
                {
                    var employeeUpdated = _empRepo.Update(employeeFromRepo.EmployeeId, employeeFromRepo);
                    return Ok(employeeUpdated);
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
        [ProducesResponseType(typeof(ApiResponse<Assignment>), 200)]
        [ProducesResponseType(typeof(ApiResponse<Assignment>), 400)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var employeeToDel = await _empRepo.GetById(id);
                await _empRepo.Delete(employeeToDel.EmployeeId);

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