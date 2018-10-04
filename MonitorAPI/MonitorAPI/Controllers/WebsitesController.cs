using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitorAPI.DAL;
using MonitorAPI.DAL.Models;

namespace MonitorAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class WebsitesController : ControllerBase
    {
        /// <summary>
        /// Returns all websites.
        /// </summary>
        [HttpGet]
        public IEnumerable<Website> Get()
        {
            var db = new MonitorContext();
            return db.Websites;
  
        }

        /// <summary>
        /// Returns specific a website.
        /// </summary>
        /// <param name="id">The id of a website.</param>
        /// <response code="200">Requested website found and returned.</response>
        /// <response code="400">Requested website not found.</response>
        [HttpGet("website/{id}")]
        public async Task<IActionResult> GetWebsite([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var db = new MonitorContext();
            var site = await db.Websites.FindAsync(id);

            if (site == null)
            {
                return NotFound();
            }

            return Ok(site);
        }

        /// <summary>
        /// Creates a new website object to check.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /website/add/
        ///     {
        ///        "link" : "https://www.example.com",
        ///        "word" : "Foo"
        ///     }
        ///
        /// </remarks>
        /// <returns>A newly created Website</returns>
        /// <response code="201">Returns the newly created website object</response>
        [HttpPost("website/add/")]
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

        /// <summary>
        /// Deletes the specified website.  
        /// </summary>
        /// <param name="id">The id of a website.</param>   
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /website/{id}
        ///
        /// </remarks>
        /// <returns>Response code.</returns>
        /// <response code="200">Succesfully deleted the service.</response>
        /// <response code="400">Requested website to delete not found.</response>
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

        /// <summary>
        /// Updates the specified website with a new link and word. Also re-checks the status.
        /// </summary>
        /// <remarks>
        /// <param name="id">The id of a website.</param>   
        /// Sample request:
        ///
        ///     PUT /service/edit/{id}
        ///     {
        ///        "link" : "https://www.example.com",
        ///        "word" : "Foo"
        ///     }
        ///
        /// </remarks>
        /// <returns>The updated website object.</returns>
        /// <response code="200">Returns the newly created website</response>
        [HttpPut("website/edit/{id}")]
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
