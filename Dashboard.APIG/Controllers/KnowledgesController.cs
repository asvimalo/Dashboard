﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dashboard.DataG.EF.Contracts;
using Microsoft.Extensions.Logging;
using Dashboard.EntitiesG.EntitiesRev;

namespace Dashboard.API.Controllers
{
    
    [Route("api/dashboard/knowledges")]
    public class KnowledgesController : Controller
    {
        public IRepoKnowledge _repo;
        private ILogger<KnowledgesController> _logger;

        public KnowledgesController(IRepoKnowledge repo,
            ILogger<KnowledgesController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET api/dashboard/knowledges
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = _repo.Include(x => x.AcquiredKnowledges);
                return Ok(result);
                //return Ok(Mapper.Map<IEnumerable<CommitmentViewModel>>(result));
            }
            catch (Exception ex)
            {
                // LOGGING TODO
                _logger.LogError($"Exception thrown white getting knowledges: {ex}");
                return BadRequest($"Error ocurred");
            }
        }

        // GET api/dashboard/Commitments/5
        [HttpGet("{id}", Name = "GetKnowledge")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = _repo.GetById(id);
                return Ok(result);
                //return Ok(Mapper.Map<CommitmentViewModel>(result));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown while getting knowledge: {ex}");
                return BadRequest($"Error ocurred");
            }

        }

        // POST api/dashboard/knowledges
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]Knowledge knowledge)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var addedKnowledgee = _repo.Create(knowledge);
                    return Created($"api/dashboard/commitments/{addedKnowledgee.Id}", addedKnowledgee);
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Exception thrown while getting knowledge: {ex}");
                    return BadRequest($"Error ocurred");
                }
                //var newCommitment = Mapper.Map<Commitment>(commitment);
                
               
                    
              
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/dashboard/knowledges/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Knowledge knowledge)
        {
            if (ModelState.IsValid)
            {
                //var projectId = 0;
                //var userId = 0;
                try
                {
                    var knowledgeFromRepo = await _repo.GetById(id);
                    //Mapper.Map(commitmentVM, commiFromRepo);

                    knowledgeFromRepo.KnowledgeName = knowledge.KnowledgeName ?? knowledgeFromRepo.KnowledgeName;
                    knowledgeFromRepo.Description = knowledge.Description ?? knowledgeFromRepo.Description;
                    knowledgeFromRepo.AcquiredKnowledges = knowledge.AcquiredKnowledges ?? knowledgeFromRepo.AcquiredKnowledges;


                    var knowledgeUpdated = _repo.Create(knowledgeFromRepo);


                    

                    return Ok(/*Mapper.Map<CommitmentViewModel>(*/knowledgeUpdated/*)*/);
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
                var knowledgeToDel = await _repo.GetById(id);
                await _repo.Delete(knowledgeToDel.Id);

                return Ok($"Commitment deleted!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Thrown exception when updating {ex}");
                return BadRequest($"Knowledge  wasn't deleted!");
            }
            
                
        }

    }
}