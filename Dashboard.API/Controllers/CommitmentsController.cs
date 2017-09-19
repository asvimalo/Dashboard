using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Data.EF.IRepository;
using Dashboard.Data.Entities;
using Dashboard.Data.ViewModelsAPI;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Dashboard.Data.Controllers
{

    [Route("api/dashboard/[controller]")]
    public class CommitmentsController : Controller
    {
        public IRepositoryDashboard _repo;
        private ILogger<CommitmentsController> _logger;

        public CommitmentsController(IRepositoryDashboard repo, ILogger<CommitmentsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        // GET api/dashboard/Commitments
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetCommitments();
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
        [HttpGet("{id}", Name = "GetCommitment")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _repo.GetCommitment(id);
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
        public async Task<IActionResult> Post([FromBody]Commitment commitment)
        {
            if (ModelState.IsValid)
            {
                //var newCommitment = Mapper.Map<Commitment>(commitment);
                var addedCommitment =  await _repo.AddAsync(commitment);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/dashboard/commitments/{addedCommitment.CommitmentId}", addedCommitment);
                }
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/dashboard/Commitments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Commitment commitment)
        {
            if (ModelState.IsValid)
            {
                var projectId = 0;
                var userId = 0;
                var commiFromRepo = await _repo.GetCommitment(id);
                //Mapper.Map(commitmentVM, commiFromRepo);

                commiFromRepo.Name = commitment.Name ?? commiFromRepo.Name;
                commiFromRepo.ProjectId = projectId;
                Int32.TryParse((commitment.ProjectId.ToString() ?? commiFromRepo.ProjectId.ToString()),out projectId);
                commiFromRepo.UserId = userId;
                Int32.TryParse((commitment.UserId.ToString() ?? commiFromRepo.UserId.ToString()),out userId);

                var commitUpdated =  _repo.Update(commiFromRepo);
                if (!await _repo.SaveChangesAsync())
                {
                    _logger.LogError($"Thrown exception when updating");
                    BadRequest("Something when wrong while updating");
                }
                return Ok(/*Mapper.Map<CommitmentViewModel>(*/commitUpdated/*)*/); 
            }
            return BadRequest("Error occured");
            
        }

        // DELETE api/dashboard/Commitments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var commiToDel = await _repo.GetCommitment(id);
            _repo.Delete(commiToDel);
            if (await _repo.SaveChangesAsync())
                return Ok($"Commitment deleted!");
            else
                return BadRequest($"Commitment {commiToDel.Name} wasn't deleted!");
        }
    }
}
