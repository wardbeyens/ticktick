using Microsoft.EntityFrameworkCore;
using TickTick.Data.Extensions;
using TickTick.Models;

namespace TickTick.Data
{
    public class TickTickDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistItem> PlaylistItems { get; set; }

        public TickTickDbContext(DbContextOptions options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            this.AddAuditableInfo();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.AddAuditableInfo();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
