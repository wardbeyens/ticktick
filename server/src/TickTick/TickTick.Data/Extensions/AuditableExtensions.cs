using Microsoft.EntityFrameworkCore;
using TickTick.Models;

namespace TickTick.Data.Extensions
{
    public static class AuditableExtensions
    {
        public static void AddAuditableInfo(this DbContext ctx)
        {
            // PUT, POST en DELETE
            // BASE AUDITABLE ENTITY

            //e van entry
            var entries = ctx.ChangeTracker
                             .Entries<BaseAuditableEntity>()
                             .Where(e => /*e.Entity is BaseAuditableEntity && */ (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.State is EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }

        }
    }
}
