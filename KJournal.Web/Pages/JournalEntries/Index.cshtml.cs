using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KJournal.Web.Data;
using KJournal.Web.Models;

namespace KJournal.Web.Pages.JournalEntries
{
    public class IndexModel : PageModel
    {
        private readonly KJournal.Web.Data.KJournalDbContext _context;

        public IndexModel(KJournal.Web.Data.KJournalDbContext context)
        {
            _context = context;
        }

        public IList<JournalEntry> JournalEntry { get;set; } = default!;

        public async Task OnGetAsync()
        {
            JournalEntry = await _context.JournalEntry.ToListAsync();
        }
    }
}
