using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Breakdown.API.Controllers
{
    [Route("api/[controller]")]
    public class WelcomeController : Controller
    {
        // GET api/values
        /// <summary>
        /// Junk method to check API availability
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Hi, Welcome to GarageMan API!" };
        }
    }
}
