using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.API.EF.IRepository;
using Dashboard.Data.Entities;

namespace Dashboard.API.Controllers
{
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        public IRepository<Commitment> _repo;

        public DashboardController(IRepository<Commitment> repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }
        // GET api/values
        [HttpGet("commitments")]
        public IActionResult Get()
        {
            return Ok( _repo.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
