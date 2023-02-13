using MediatR;
using Microsoft.AspNetCore.Mvc;
using TickTick.Api.Dtos;
using TickTick.Api.RequestHandlers;
using TickTick.Api.ResponseWrappers;
using TickTick.Api.Services;
using TickTick.Models;
using TickTick.Repositories.Base;

namespace TickTick.Api.Controllers
{
    [Route("v{v:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PersonsController : ApiControllerBase
    {
        private readonly IPersonsService svc;
        private readonly IRepository<Person> repo;

        public PersonsController(IPersonsService service, IRepository<Person> repo, IMediator mediator)
            :base(mediator)
        {
            this.svc = service;
            this.repo = repo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<PersonDto>), 200)]
        public async Task<IActionResult> Get()
        {

            return await ExecuteRequest(new GetAllPersonsRequest());

            /*
            try
            {
                var persons = await repo.GetAllAsync(p => p.IsDeleted == false);

                //List<Person> people = new List<Person>();
                //people.Add(new Person("John", "Doe", "john@mail.com"));
                //people.Add(new Person("Kevin", "DeRudder", "kevin.derudder@gmail.com"));

                Response<IEnumerable<Person>> resp = new Response<IEnumerable<Person>>();
                resp.Data = persons;
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Response<IEnumerable<Person>> r = new Response<IEnumerable<Person>>();
                r.Data = null;
                r.Message = ex.Message;
                r.Status = System.Net.HttpStatusCode.InternalServerError;
                return StatusCode(500, r);
            }
            */

        }


        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(PersonDto), 200)]
        public async Task<IActionResult> Get(Guid id)
        {
            return await ExecuteRequest(new GetPersonRequest(id));
            /*
            
            //TODO: Haal een persoon op
            //Person person = new Person("Kevin", "DeRudder", "kevin.derudder@gmail.com");
            
            var person = await repo.GetAsync(p => p.PublicId == id);

            Response<Person> resp = new Response<Person>();
            resp.Data = person;

            return Ok(person.ConvertToDto());
            */
        }

        /*
        [HttpGet("{personId:guid}/locations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<Location>), 200)]
        public IActionResult GetLocations(Guid personId)
        {
            //TODO: Haal een persoon op
            List<Location> locations = new List<Location>() {
                new Location("Coupure Rechts", "40", "Gent", "9000", "Belgium"),
                new Location("Coupure Links", "40", "Gent", "9000", "Belgium"),
                new Location("Koning Albert I laan", "1", "Gent", "9000", "Belgium")
            };
            return Ok(new Response<IEnumerable<Location>>(locations));
        }
        */

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await ExecuteRequest(new DeletePersonRequest(id));

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(PersonDto), 201)]
        public async Task<IActionResult> Post([FromBody] AddPersonDto person)
        {
            return await ExecuteRequest(new AddPersonRequest(person));
            /*
            PersonDto newP = svc.AddPerson(person);
            Person p = new Person(person.FirstName, person.LastName, person.Email);
            repo.Add(p);
            int i = await repo.SaveAsync();
            return CreatedAtAction(nameof(Get), new { id = newP.PublicId }, newP);
            */
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(PersonDto), 200)]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdatePersonDto person)
        {
            return await ExecuteRequest(new UpdatePersonRequest(id, person));
        }
    }
}
