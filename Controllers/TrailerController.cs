using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace trailers_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrailerController : ControllerBase
    {
        // GET: api/Trailer
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET: api/Trailer/:id
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        // POST: api/Trailer
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return Ok();
        }

        // PUT: api/Trailer/:id
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok();
        }

        // DELETE: api/Trailer/:id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
