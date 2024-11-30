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
    public class DeleteModel : PageModel
    {
        private readonly KJournal.Web.Data.KJournalDbContext _context;

        public DeleteModel(KJournal.Web.Data.KJournalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public JournalEntry JournalEntry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journalentry = await _context.JournalEntry.FirstOrDefaultAsync(m => m.Id == id);

            if (journalentry is not null)
            {
                JournalEntry = journalentry;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journalentry = await _context.JournalEntry.FindAsync(id);
            if (journalentry != null)
            {
                JournalEntry = journalentry;
                _context.JournalEntry.Remove(JournalEntry);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
