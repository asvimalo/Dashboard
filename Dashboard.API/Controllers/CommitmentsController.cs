using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.API.EF.IRepository;
using Dashboard.Data.Entities;
using Dashboard.Data.ViewModelsAPI;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Dashboard.API.Controllers
{

    [Route("api/dashboard/[controller]")]
    public class CommitmentsController : Controller
    {
        public IRepository<Commitment> _repo;
        private ILogger<CommitmentsController> _logger;

        public CommitmentsController(IRepository<Commitment> repo, ILogger<CommitmentsController> logger)
        {
            _repo = repo;
            _logger = logger;
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
                var result = await _repo.GetAll();

                return Ok(Mapper.Map<IEnumerable<CommitmentViewModel>>(result));
            }
            catch (Exception ex)
            {
                // LOGGING TODO
                _logger.LogError($"Exception thrown white getting commitments: {ex}");
                return BadRequest($"Error ocurred");
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _repo.Get(id);
                return Ok(Mapper.Map<CommitmentViewModel>(result));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown while getting commitment: {ex}");
                return BadRequest($"Error ocurred");
            }

        }

        // POST api/values
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CommitmentViewModel commitment)
        {
            if (ModelState.IsValid)
            {
                var newCommitment = Mapper.Map<Commitment>(commitment);
                _repo.Add(newCommitment);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/dashboard/commitments/{commitment.Name}", Mapper.Map<CommitmentViewModel>(newCommitment));
                }
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]CommitmentViewModel commitmentVM)
        {
            if (ModelState.IsValid)
            {
                var commiFromRepo = await _repo.Get(id);
                Mapper.Map(commitmentVM, commiFromRepo);
                var commitUpdated =  _repo.Update(commiFromRepo);
                if (!await _repo.SaveChangesAsync())
                {
                    _logger.LogError($"Thrown exception when updating");
                    BadRequest("Something when wrong while updating");
                }
                return Ok(Mapper.Map<CommitmentViewModel>(commitUpdated)); 
            }
            return BadRequest("Error occured");
            
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var commiToDel = await _repo.Get(id);
            _repo.Delete(commiToDel);
            if (await _repo.SaveChangesAsync())
                return Ok($"Commitment deleted!");
            else
                return BadRequest($"Commitment {commiToDel.Name} wasn't deleted!");
        }
    }
}
