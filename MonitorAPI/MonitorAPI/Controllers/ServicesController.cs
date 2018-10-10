using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MonitorAPI.DAL;
using MonitorAPI.DAL.Models;

namespace MonitorAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {

        /// <summary>
        /// Returns all services. And updates every status and timestamps.
        /// </summary>
        [HttpGet]
        public IEnumerable<Service> Get()
        {
            var db = new MonitorContext();
            
            foreach(Service system in db.Services)
            {
                Put(system.Id, system);
            }

            return db.Services;
           
        }

        /// <summary>
        /// Returns specific a service.
        /// </summary>
        /// <param name="id">The id of a service.</param>
        /// <response code="400">Requested service not found.</response>
        [HttpGet("service/{id}")]
        public async Task<IActionResult> GetService([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var db = new MonitorContext();
            var service = await db.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        /// <summary>
        /// Creates a new service object to check.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /service/add/
        ///     {
        ///        "link": "https://www.exampleapi.com",
        ///     }
        ///
        /// </remarks>
        /// <returns>A newly created Service</returns>
        /// <response code="201">Returns the newly created service object</response>
        [HttpPost("service/add/")]
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

        /// <summary>
        /// Deletes the specified service.  
        /// </summary>
        /// <param name="id">The id of a service.</param>   
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /service/{id}
        ///
        /// </remarks>
        /// <returns>Response code.</returns>
        /// <response code="200">Succesfully deleted the service.</response>
        /// <response code="400">Requested service to delete not found.</response>
        [HttpDelete("service/{id}")]
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

        /// <summary>
        /// Updates the specified service with a new link and re-checks the status.
        /// </summary>
        /// <remarks>
        /// <param name="id">The id of a service.</param>   
        /// Sample request:
        ///
        ///     PUT /service/edit/{id}
        ///     {
        ///        "link": "https://www.exampleapi.com",
        ///     }
        ///
        /// </remarks>
        /// <returns>The updated service object.</returns>
        /// <response code="200">Returns the newly created service</response>
        [HttpPut("service/edit/{id}")]
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
