using TickTick.Api.Dtos;
using TickTick.Models;

namespace TickTick.Api.Services
{
    public class SongService : ISongsService
    {
        SongDto ISongsService.AddSong(SongDto dto)
        {
            Song song = new Song(dto.Title, dto.Artist);
            // iets?
            return song.ConvertToDto();
        }
        
        SongDto ISongsService.UpdateSong(long id, SongDto dto)
        {
            Song song = new Song(dto.Title, dto.Artist);
            song.Update(dto.Title, dto.Artist, dto.Duration, dto.SequenceNumber);

        }

        void ISongsService.DeleteSong(Guid id)
        {
            throw new NotImplementedException();
        }

        void ISongsService.DeleteSong(Song song)
        {
            song.Delete();
        }

    }
}
