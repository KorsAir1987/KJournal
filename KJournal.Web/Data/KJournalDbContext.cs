using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KJournal.Web.Data
{
    public class KJournalDbContext : IdentityDbContext
    {
        public KJournalDbContext (DbContextOptions<KJournalDbContext> options)
            : base(options)
        {
        }

        public DbSet<KJournal.Web.Models.JournalEntry> JournalEntry { get; set; } = default!;
    }
}
