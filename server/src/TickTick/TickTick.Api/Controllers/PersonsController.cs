using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TickTick.Models;

namespace TickTick.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() {
            List<Person> people = new List<Person>();
            people.Add(new Person ("John","Doe", "john@mail.com"));
            people.Add(new Person("Kevin", "DeRudder", "kevin.derudder@gmail.com"));
            return Ok(people);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult Get(Guid id)
        {
            //TODO: Haal een persoon op
            Location personLocation = new Location("Kerkstraat", "1", "Gent", "9000", "Belgium");
            Person person = new Person("Kevin", "DeRudder", "kevin.derudder@gmail.com");
            person.Addresses = new List<Location>{
                personLocation
            };
            return Ok(person.ConvertToDto());
        }
    }
}
