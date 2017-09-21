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

namespace Dashboard.API.Controllers
{
    [Produces("application/json")]
    [Route("api/dashboard/Users")]
    public class UsersController : Controller
    {
        public IRepositoryDashboard _repo;
        private ILogger<UsersController> _logger;

        public UsersController(IRepositoryDashboard repo, ILogger<UsersController> logger)
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
                var result = await _repo.GetUsers();
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
                var result = await _repo.GetUser(id);
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
        public async Task<IActionResult> Post([FromBody]User user)
        {
            if (ModelState.IsValid)
            {
                //var newCommitment = Mapper.Map<Commitment>(commitment);
                var addeduser = await _repo.AddAsync(user);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/dashboard/commitments/{addeduser.UserId}", addeduser);
                }
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/dashboard/Commitments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]User user)
        {
            if (ModelState.IsValid)
            {
                //var projectId = 0;
                //var userId = 0;
                var userFromRepo = await _repo.GetUser(id);
                //Mapper.Map(commitmentVM, commiFromRepo);

                userFromRepo.FirstName = user.FirstName ?? userFromRepo.FirstName;
                userFromRepo.LastName = user.LastName ?? userFromRepo.LastName;               
                userFromRepo.PersonNr = user.PersonNr ?? userFromRepo.PersonNr;
                userFromRepo.PictureId = user.PictureId ?? userFromRepo.PictureId;
                userFromRepo.Picture = user.Picture ?? userFromRepo.Picture;

                var userUpdated = _repo.Update(userFromRepo);

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
            var userToDel = await _repo.GetUser(id);
            _repo.Delete(userToDel);
            if (await _repo.SaveChangesAsync())
                return Ok($"Commitment deleted!");
            else
                return BadRequest($"Commitment {userToDel.FirstName } wasn't deleted!");
        }

    }
}