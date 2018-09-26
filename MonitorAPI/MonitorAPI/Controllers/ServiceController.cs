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
        [HttpGet("services/all")]
        public IEnumerable<Service> Get()
        {
            var db = new MonitorContext();
            return db.Services;
           
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetService([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        // POST api/values
        [HttpPost("services/add/")]
        public async Task<IActionResult> Post([FromBody] Service data)
        {

            MonitorAPI.Data.Service Service = new Data.Service();
            var service = new Service();
            service.Link = data.Link;
            service.TimeStamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");

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

        // DELETE api/values/5
        [HttpDelete("services/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using (var db = new MonitorContext())
            {
                var service = db.Services.SingleOrDefault(s => s.Id == id);

                if (service == null || id <= 0)
                {
                    return NotFound();
                }

                db.Services.Remove(service);
                await db.SaveChangesAsync();
                return Ok(service);
            }
        }


        [HttpPut("services/edit/{id}")]
        public async Task<IActionResult> Put(int id, Service data)
        {
            using (var db = new MonitorContext())
            {
                var service = db.Services.SingleOrDefault(s => s.Id == id);

                if (service == null)
                {
                    return NotFound();
                }

                service.TimeStamp = DateTime.Now.ToString("dd-MM-yyyyTHH:mm:sszzz");
                service.Link = data.Link;

                MonitorAPI.Data.Service tester = new Data.Service();
                if (tester.GetStatus(service.Link))
                {
                    service.Status = true;
                }
                else
                {
                    service.Status = false;
                }

                db.Services.Update(service);
                await db.SaveChangesAsync();
                return Ok(service);
            }
        }
    }
}
