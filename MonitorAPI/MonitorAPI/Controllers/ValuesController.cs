using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MonitorAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            string output;
            //return new string[] { "value1", "value2" };
            MonitorAPI.Data.Api API = new Data.Api();
            MonitorAPI.Data.Website site = new Data.Website();

            if (API.GetStatus("https://gmsapi.azurewebsites.net/Record/break"))
            {
                output = "The API is live!";
            }
            else
            {
                output = "The API is down!";
            }

            if (site.GetStatus("https://gms.azurewebsites.net/", "Nepwoord"))
            {
                output += "The website is live!";
            }
            else
            {
                output += "The website is down!";
            }

            return output;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
