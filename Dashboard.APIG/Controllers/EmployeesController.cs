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
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = _empRepo.Include(x => x.AcquiredKnowledges, y => y.Assignments);

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
        [HttpGet("load")]
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
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = _empRepo.Include(a => a.AcquiredKnowledges, x => x.Assignments).Where(x => x.EmployeeId == id).First();
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

                    return Ok();
                    //#region Write picture to Image folder
                    //var webRootPath = _env.WebRootPath;
                    //var fileName = newEmployee.FirstName + ".jpg";
                    //var filePath = Path.Combine($"{webRootPath}/Images/{fileName}");
                    //await System.IO.File.WriteAllBytesAsync(filePath, employee.Bytes);


                    //var newCommitment = Mapper.Map<Commitment>(commitment);
                    //newEmployee.ImageName = fileName;
                    //newEmployee.ImagePath = filePath; 
                    //#endregion
                    //return Ok(Mapper.Map<CommitmentViewModel>(result));
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Exception thrown while getting employee: {ex.Message}");
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
                var employeeFromRepo = await _empRepo.GetById(id);
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

               

                //employeeFromRepo.ImageName = employee.ImageName ?? employeeFromRepo.ImageName;
                //employeeFromRepo.ImagePath = employee.ImagePath ?? employeeFromRepo.ImagePath;
                //employeeFromRepo.Assignments = employee.Assignments ?? employeeFromRepo.Assignments;
                //employeeFromRepo.AcquiredKnowledges = employee.AcquiredKnowledge ?? employeeFromRepo.AcquiredKnowledges;
                try
                {
                    var employeeUpdated = _empRepo.Update(employeeFromRepo.EmployeeId, employeeFromRepo);
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
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromBody]ImageForCreation imageForCreation)
        {
            if (imageForCreation == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // return 422 - Unprocessable Entity when validation fails
                return new UnprocessableEntityObjectResult(ModelState);
            }

            // Automapper maps only the Title in our configuration
            var newEmployee = new Employee();

            // Create an image from the passed-in bytes (Base64), and 
            // set the filename on the image

            // get this environment's web root path (the path
            // from which static content, like an image, is served)
            var webRootPath = _env.WebRootPath;

            // create the filename
            string fileName = Guid.NewGuid().ToString() + ".jpg";

            // the full file path
            var filePath = Path.Combine($"{webRootPath}/images/{fileName}");

            // write bytes and auto-close stream
            System.IO.File.WriteAllBytes(filePath, imageForCreation.Bytes);

            // fill out the filename
            newEmployee.ImageName = fileName;

            // set the ownerId on the imageEntity
            newEmployee.ImagePath = filePath;

            // add and save.  
            var addedEmp = await _empRepo.Create(newEmployee);

            

            return Ok (addedEmp);
        }
    }
}