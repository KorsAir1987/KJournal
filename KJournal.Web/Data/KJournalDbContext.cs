using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KJournal.Web.Models;

namespace KJournal.Web.Data
{
    public class KJournalDbContext : DbContext
    {
        public KJournalDbContext (DbContextOptions<KJournalDbContext> options)
            : base(options)
        {
        }

        public DbSet<KJournal.Web.Models.JournalEntry> JournalEntry { get; set; } = default!;
    }
}
