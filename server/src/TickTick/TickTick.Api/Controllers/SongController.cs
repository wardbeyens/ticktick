using Microsoft.AspNetCore.Mvc;
using TickTick.Api.Dtos;
using TickTick.Api.ResponseWrappers;
using TickTick.Api.Services;
using TickTick.Models;
using TickTick.Repositories.Base;

namespace TickTick.Api.Controllers
{
    [Route("v{v:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ISongsService svc;
        private readonly IRepository<Song> repo;

        public SongsController(ISongsService service, IRepository<Song> repo)
        {
            this.svc = service;
            this.repo = repo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<SongDto>), 200)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var songs = await repo.GetAllAsync(s => s.IsDeleted == false);

                //List<Song> people = new List<Song>();
                //people.Add(new Song("John", "Doe", "john@mail.com"));
                //people.Add(new Song("Kevin", "DeRudder", "kevin.derudder@gmail.com"));

                Response<IEnumerable<Song>> resp = new Response<IEnumerable<Song>>();
                resp.Data = songs;
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Response<IEnumerable<Song>> r = new Response<IEnumerable<Song>>();
                r.Data = null;
                r.Message = ex.Message;
                r.Status = System.Net.HttpStatusCode.InternalServerError;
                return StatusCode(500, r);
            }

        }


        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(SongDto), 200)]
        public async Task<IActionResult> Get(long id)
        {
            //TODO: Haal een persoon op
            //Song song = new Song("Kevin", "DeRudder", "kevin.derudder@gmail.com");

            var song = await repo.GetAsync(s => s.Id == id);

            Response<Song> resp = new Response<Song>();
            resp.Data = song;

            return Ok(song.ConvertToDto());
        }

        /*
        [HttpGet("{songId:guid}/locations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<Location>), 200)]
        public IActionResult GetLocations(Guid songId)
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

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(long id)
        {

            var song = await repo.GetAsync(s => s.Id == id);
            if (song == null)
            {
                return NotFound();
            }
            svc.DeleteSong(song);
            repo.Delete(song);

            int i = await repo.SaveAsync();
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(SongDto), 201)]
        public async Task<IActionResult> Post([FromBody] SongDto song)
        {
            SongDto newSong = svc.AddSong(song);
            Song s = new Song(song.Title, song.Artist);
            repo.Add(s);
            int i = await repo.SaveAsync();
            return CreatedAtAction(nameof(Get), new { id = 99}, newSong);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(SongDto), 200)]
        public async Task<IActionResult> Put(long id, [FromBody] SongDto song)
        {
            SongDto newSong = svc.UpdateSong(id, song);

            Song s = new Song(song.Title, song.Artist);
            repo.Add(s);
            int i = await repo.SaveAsync();

            return Ok(newSong);
        }
    }
}
