using TickTick.Api.Dtos;
using TickTick.Models;

namespace TickTick.Api.Services
{
    public interface ISongsService
    {
        void DeleteSong(Guid id);
        void DeleteSong(Song song);
        SongDto AddSong(SongDto dto);
        SongDto UpdateSong(long songId, SongDto dto);
    }
}