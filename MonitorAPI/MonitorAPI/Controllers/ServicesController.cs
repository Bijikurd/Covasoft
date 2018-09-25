using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MonitorAPI.DAL;
using MonitorAPI.DAL.Models;

namespace MonitorAPI.Controllers
{

    [Route("/")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly MonitorContext _context;

        // GET api/values
        [HttpGet]
        public void Get()
        {
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWebsite([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = await _context.Websites.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetService([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = await _context.Services.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        // POST api/values
        [HttpPost("services/add/")]
        public async Task<IActionResult> Post([FromBody] Service data)
        {

            MonitorAPI.Data.Service Service = new Data.Service();
            var service = new Service();
            service.Link = data.Link;

            if (Service.GetStatus(service.Link))
            {
                service.Status = true;
            }
            else
            {
                service.Status = false;
            }

            using (var db = new MonitorContext())
            {
                db.Services.Add(service);
                await db.SaveChangesAsync();
            }

            return CreatedAtAction("GetService", new { id = service.Id }, service);

        }

        // POST api/values
        [HttpPost("website/add")]
        public async Task<IActionResult> Post([FromBody] string link, string word)
        {

            MonitorAPI.Data.Website Website = new Data.Website();
            var website = new Website();
            website.Link = link;
            website.Word = word;

            if (Website.GetStatus(website.Link, website.Word))
            {
                website.Status = true;
            }
            else
            {
                website.Status = false;
            }

            using (var db = new MonitorContext())
            {
                db.Websites.Add(website);
                await db.SaveChangesAsync();
            }

            return CreatedAtAction("GetWebsite", new { id = website.Id }, website);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
