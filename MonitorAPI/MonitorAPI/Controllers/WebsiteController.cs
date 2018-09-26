using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitorAPI.DAL;
using MonitorAPI.DAL.Models;

namespace MonitorAPI.Controllers
{

    [Route("/")]
    [ApiController]
    public class WebsiteController : ControllerBase
    {
        private readonly MonitorContext _context;

        [HttpGet("websites/all")]
        public IEnumerable<Website> Get()
        {
            var db = new MonitorContext();
            return db.Websites;
  
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
        [HttpPost("websites/add/")]
        public async Task<IActionResult> Post([FromBody] Website data)
        {

            MonitorAPI.Data.Website Website = new Data.Website();
            var website = new Website();
            website.Link = data.Link;
            website.Word = data.Word;
            website.TimeStamp = DateTime.Now.ToString("dd-MM-yyyyTHH:mm:sszzz");

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

        // DELETE api/values/5
        [HttpDelete("websites/{id}")]
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

        [HttpPut("websites/edit/{id}")]
        public async Task<IActionResult> Put(int id, Website data)
        {
            using (var db = new MonitorContext())
            {
                var website = db.Websites.SingleOrDefault(s => s.Id == id);

                if (website == null)
                {
                    return NotFound();
                }

                website.Word = data.Word;
                website.TimeStamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");

                MonitorAPI.Data.Website tester = new Data.Website();
                if (tester.GetStatus(website.Link, website.Word))
                {
                    website.Status = true;
                }
                else
                {
                    website.Status = false;
                }

                db.Websites.Update(website);
                await db.SaveChangesAsync();
                return Ok(website);
            }
        }
    }
}
