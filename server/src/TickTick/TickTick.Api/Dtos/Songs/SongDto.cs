using TickTick.Models;

namespace TickTick.Api.Dtos
{
    public class SongDto
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public TimeSpan? Duration { get; set; }
        public uint SequenceNumber { get; set; }
    }

    public static class SongExtensions
    {
        public static SongDto ConvertToDto(this Song song)
        {
            return new SongDto()
            {
                Title = song.Title,
                Artist = song.Artist,
                Duration = song.Duration,
                SequenceNumber = song.SequenceNumber
            };
        }
    }
}
