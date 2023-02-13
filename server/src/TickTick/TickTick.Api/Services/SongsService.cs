using TickTick.Api.Dtos;
using TickTick.Models;

namespace TickTick.Api.Services
{
    public class SongsService : ISongsService
    {
        public SongDto AddSong(SongDto dto)
        {
            Song song = new Song(dto.Title, dto.Artist);
            // iets?
            return song.ConvertToDto();
        }

        public SongDto UpdateSong(long id, SongDto dto)
        {
            Song song = new Song(dto.Title, dto.Artist);
            song.Update(dto.Title, dto.Artist, dto.Duration, dto.SequenceNumber);
            return song.ConvertToDto();
        }

        public void DeleteSong(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteSong(Song song)
        {
            song.Delete();
        }
    }
}
