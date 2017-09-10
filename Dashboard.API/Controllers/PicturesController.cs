using AutoMapper;
using Dashboard.API.EF.IRepository;
using Dashboard.Data.Entities;
using Dashboard.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.Controllers
{
  
    [Route("api/dashboard/[controller]")]
    public class PicturesController : Controller
    {
        public IRepository<Picture> _repo;
        private ILogger<PicturesController> _logger;

        public PicturesController(IRepository<Picture> repo,
            ILogger<PicturesController> logger)
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

                return Ok(Mapper.Map<IEnumerable<PictureViewModel>>(result));
            }
            catch (Exception ex)
            {
                // LOGGING TODO
                _logger.LogError($"Exception thrown while getting Pictures: {ex}");
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
                return Ok(Mapper.Map<PictureViewModel>(result));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown while getting Picture: {ex}");
                return BadRequest($"Error ocurred");
            }

        }

        // POST api/values
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]PictureViewModel picture)
        {
            if (ModelState.IsValid)
            {
                var newPicture = Mapper.Map<Picture>(picture);
                _repo.Add(newPicture);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/dashboard/projects/{picture.Title}", Mapper.Map<PictureViewModel>(newPicture));
                }
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PictureViewModel pictureVM)
        {
            if (ModelState.IsValid)
            {
                var pictureFromRepo = await _repo.Get(id);
                Mapper.Map(pictureVM, pictureFromRepo);
                var pictureUpdated = _repo.Update(pictureFromRepo);
                if (!await _repo.SaveChangesAsync())
                {
                    _logger.LogError($"Thrown exception when updating");
                    BadRequest("Something when wrong while updating");
                }
                return Ok(Mapper.Map<PictureViewModel>(pictureUpdated));
            }
            return BadRequest("Error occured");

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pictureToDel = await _repo.Get(id);
            _repo.Delete(pictureToDel);

            if (await _repo.SaveChangesAsync())
                return Ok($"Picture deleted!");
            else
                return BadRequest($"Picture {pictureToDel.Title} wasn't deleted!");
        }
    }

}
