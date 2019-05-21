using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RESTfullAPIRoComputer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoComputerController : ControllerBase
    {
        
        // GET: api/RoComputer
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/RoComputer/email
        [HttpGet("{email}", Name = "Get")]
        public List<PersonData> GetStats(string email)
        {
            using (RoComputerContext context = new RoComputerContext())
            {
               return context.PersonData.Where(p => p.FkEmail == email).ToList<PersonData>();
            }

        }
        [HttpPost()]
        public bool Login([FromBody] Person person)
        {
            return Person.Login(person);
        }

        // POST: api/RoComputer

    }
}
