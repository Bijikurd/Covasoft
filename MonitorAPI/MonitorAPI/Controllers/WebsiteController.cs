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
    public class WebsiteController : ControllerBase
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

            var site = await _context.Websites.FindAsync(id);

            if (site == null)
            {
                return NotFound();
            }

            return Ok(site);
        }

        // POST api/values
        [HttpPost("website/add/")]
        public async Task<IActionResult> Post([FromBody] Website data)
        {

            MonitorAPI.Data.Website Website = new Data.Website();
            var website = new Website();
            website.Link = data.Link;
            website.Word = data.Word;

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
        [HttpDelete("website/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using (var db = new MonitorContext())
            {
                var website = db.Websites.SingleOrDefault(s => s.Id == id);

                if (website == null || id <= 0)
                {
                    return NotFound();
                }

                db.Websites.Remove(website);
                await db.SaveChangesAsync();
                return Ok(website);
            }
        }
    }
}
