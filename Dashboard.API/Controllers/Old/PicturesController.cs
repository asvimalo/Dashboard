using AutoMapper;
using Dashboard.Data.EF.Contracts;

using Dashboard.Entities;
using Dashboard.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Data.Controllers
{
  
    [Route("api/dashboard/[controller]")]
    public class PicturesController : Controller
    {
        private IRepo _repo;
        private ILogger<PicturesController> _logger;
        private IHostingEnvironment _env;

        public PicturesController(IRepo repo,
            ILogger<PicturesController> logger,
            IHostingEnvironment env)
        {
            _repo = repo;
            _logger = logger;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }
        // GET api/dashboard/Pictures
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                // Get from repo
                var result = await _repo.GetAll<Picture>();
                // map to model view
                //var pictures = Mapper.Map<IEnumerable<PictureViewModel>>(result);
                // return pictures
                return Ok(result);
            }
            catch (Exception ex)
            {
                // LOGGING 
                _logger.LogError($"Exception thrown while getting Pictures: {ex}");
                return BadRequest($"Error ocurred");
            }
        }

        // GET api/dashboard/Pictures/5
        [HttpGet("{id}", Name = "GetImage")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                // Get picture
                var result = _repo.Get<Picture>(id);
                if (result == null)
                {
                    return NotFound();
                }
                //var picture = Mapper.Map<PictureViewModel>(result);
                return Ok(result);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown while getting Picture: {ex}");
                return BadRequest($"Error ocurred");
            }

        }

        // POST api/dashboard/Pictures
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]PictureForCreation pictureForCreation)
        {
            if (ModelState.IsValid)
            {
                // Automapper maps only the Title
                //var newPicture = Mapper.Map<Picture>(pictureForCreation);
                var newPicture = new Picture
                {
                    Title = pictureForCreation.Title
                };
                // get this environment's web root path (the path
                // from which static content, wwwroot)
                var webRootPath = _env.WebRootPath;
                // create file name
                string fileName = newPicture.Title + ".jpg";

                // the full file path
                var filePath = Path.Combine($"{webRootPath}/Images/{fileName}");

                // write bytes and auto-close stream
                await System.IO.File.WriteAllBytesAsync(filePath, pictureForCreation.Bytes);

                newPicture.FileName = fileName;

                // add and save ...
                var pictureFromRDb = await _repo.AddAsync(newPicture);
                if (await _repo.SaveChangesAsync())
                {
                    //var pictureToReturn = Mapper.Map<PictureViewModel>(newPicture);
                    return CreatedAtRoute("GetImage", new { id = pictureFromRDb.PictureId}, pictureFromRDb);
                }
            }
            return BadRequest("Failed to save changes to the database");
        }

        // PUT api/dashboard/Pictures/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PictureToUpdate pictureVM)
        {
            if (ModelState.IsValid)
            {
                var pictureFromRepo = _repo.Get<Picture>(id);
                if (pictureFromRepo == null)
                {
                    return NotFound();
                }
                pictureFromRepo.Title = pictureVM.Title;
                //Mapper.Map(pictureVM, pictureFromRepo);
                var pictureUpdated = _repo.Update(pictureFromRepo);
                if (!await _repo.SaveChangesAsync())
                {
                    _logger.LogError($"Thrown exception when updating");
                    BadRequest("Something when wrong while updating");
                }
                return Ok(/*Mapper.Map<PictureViewModel>(*/pictureUpdated/*)*/);
            }
            return BadRequest("Error occured");

        }

        // DELETE api/dashboard/Pictures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pictureToDel = _repo.Get<Picture>(id);
            if (pictureToDel == null)
            {
                return NotFound();
            }
            _repo.Delete(pictureToDel);

            if (await _repo.SaveChangesAsync())
                return Ok($"Picture deleted!");
            else
                return BadRequest($"Picture {pictureToDel.Title} wasn't deleted!");
        }
    }

}
