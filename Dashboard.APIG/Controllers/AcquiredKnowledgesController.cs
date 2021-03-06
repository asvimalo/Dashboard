﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.DataG.Contracts;
using Microsoft.Extensions.Logging;
using Dashboard.EntitiesG.EntitiesRev;
using Dashboard.APIG.Infrastructure;
using Dashboard.APIG.Models;

namespace Dashboard.APIG.Controllers
{
    
    [Route("api/dashboard/acquiredKnowledges")]
    public class AcquiredKnowledgesController : Controller
    {
        public IRepoAcquiredKnowledge _repo;
        private ILogger<AcquiredKnowledgesController> _logger;

        public AcquiredKnowledgesController(IRepoAcquiredKnowledge repo,
            ILogger<AcquiredKnowledgesController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET api/dashboard/acquiredKnowledges
        [HttpGet("")]
        [NoCache]
        [ProducesResponseType(typeof(List<AcquiredKnowledge>), 200)]
        [ProducesResponseType(typeof(ApiResponse<AcquiredKnowledge>), 400)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = _repo.Include(x => x.Employee, y => y.Knowledge);
                return Ok(result);
                
            }
            catch (Exception ex)
            {
                // LOGGING TODO
                _logger.LogError($"Exception thrown white getting Acquired Knowledges: {ex}");
                return BadRequest($"Error ocurred");
            }
        }

        // GET api/dashboard/acquiredKnowledges/5
        [HttpGet("{id}", Name = "GetAcquiredKnowledge")]
        [NoCache]
        [ProducesResponseType(typeof(AcquiredKnowledge), 200)]
        [ProducesResponseType(typeof(ApiResponse<AcquiredKnowledge>), 400)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = _repo.GetById(id);
                return Ok(result);
               
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown while getting Acquired Knowledge: {ex}");
                return BadRequest($"Error ocurred");
            }

        }

        // POST api/dashboard/acquiredKnowledges
        [HttpPost("")]
        [ProducesResponseType(typeof(ApiResponse<AcquiredKnowledge>), 201)]
        [ProducesResponseType(typeof(ApiResponse<AcquiredKnowledge>), 400)]
        public async Task<IActionResult> Post([FromBody]AcquiredKnowledge acquiredKnowledge)
        {
            if (ModelState.IsValid)
            {
                //var newCommitment = Mapper.Map<Commitment>(commitment);
                var addedacquiredKnowledge =  _repo.Create(acquiredKnowledge);
                
               return Created($"api/dashboard/commitments/{addedacquiredKnowledge.Id}", addedacquiredKnowledge);
                
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/dashboard/addedacquiredKnowledges/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<AcquiredKnowledge>), 200)]
        [ProducesResponseType(typeof(ApiResponse<AcquiredKnowledge>), 400)]
        public async Task<IActionResult> Put(int id, [FromBody]AcquiredKnowledge acquiredKnowledge)
        {
            if (ModelState.IsValid)
            {
                //var projectId = 0;
                //var userId = 0;
                try
                {
                    var acquiredKnowledgeFromRepo = await _repo.GetById(id);
                    //Mapper.Map(commitmentVM, commiFromRepo);

                    acquiredKnowledgeFromRepo.Employee = acquiredKnowledge.Employee ?? acquiredKnowledgeFromRepo.Employee;
                    acquiredKnowledgeFromRepo.EmployeeId = acquiredKnowledge.EmployeeId != 0 ? acquiredKnowledge.EmployeeId : acquiredKnowledgeFromRepo.EmployeeId;
                    acquiredKnowledgeFromRepo.Knowledge = acquiredKnowledge.Knowledge ?? acquiredKnowledgeFromRepo.Knowledge;
                    acquiredKnowledgeFromRepo.KnowledgeId = acquiredKnowledge.KnowledgeId != 0 ? acquiredKnowledge.KnowledgeId : acquiredKnowledgeFromRepo.KnowledgeId;

                    var acquiredKnowledgeUpdated = _repo.Update(id, acquiredKnowledgeFromRepo);

                    return Ok(/*Mapper.Map<CommitmentViewModel>(*/acquiredKnowledgeUpdated/*)*/);
                }
                catch (Exception)
                {

                    _logger.LogError($"Thrown exception when updating");
                    return BadRequest("Error occured");
                }
            }
            return BadRequest("Failed to save changes to the database");


        }

        // DELETE api/dashboard/Commitments/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<AcquiredKnowledge>), 200)]
        [ProducesResponseType(typeof(ApiResponse<AcquiredKnowledge>), 400)]
        public async Task<IActionResult> Delete(int id)
        {
            var acquiredKnowledgeToDel = _repo.GetById(id);
            if(acquiredKnowledgeToDel == null)            
                return BadRequest($"AcquiredKnowledge {acquiredKnowledgeToDel.Id } wasn't deleted!");            
            try
            {               
                await _repo.Delete(acquiredKnowledgeToDel.Id);
                return Ok($"AcquiredKnowledge deleted!");
            }
            catch (Exception)
            {
                return BadRequest("Error occured");

            }
        
                
            
                
        }

    }
}