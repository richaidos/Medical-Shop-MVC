using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medical_Shop_MVC.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Shop_MVC.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class APIUserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public APIUserController(ApplicationDbContext context)
        {
            _context = context;
        }

        /*
        // GET: api/user
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        */

        // GET: api/user/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/user
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT: api/user/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
