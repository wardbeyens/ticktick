using TickTick.Models.Contracts;

namespace TickTick.Models
{
    public class Song : BaseEntity, IPlaylistItem
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public TimeSpan? Duration { get; set; }
        public uint SequenceNumber { get; set; }
        public bool IsDeleted { get; set; }

        public Song(string title, string artist)
        {
            this.Title = title;
            this.Artist = artist;
        }


        public void Delete()
        {
            this.IsDeleted = true;
        }

        public void Update(string title, string artist, TimeSpan? duration, uint sequenceNumber)
        {
            this.Title = title;
            this.Artist = artist;
            this.Duration = duration;
            this.SequenceNumber = sequenceNumber;
        }
    }
}
