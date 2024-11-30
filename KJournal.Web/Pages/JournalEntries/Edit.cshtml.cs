using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KJournal.Web.Data;
using KJournal.Web.Models;

namespace KJournal.Web.Pages.JournalEntries
{
    public class EditModel : PageModel
    {
        private readonly KJournal.Web.Data.KJournalDbContext _context;

        public EditModel(KJournal.Web.Data.KJournalDbContext context)
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

            var journalentry =  await _context.JournalEntry.FirstOrDefaultAsync(m => m.Id == id);
            if (journalentry == null)
            {
                return NotFound();
            }
            JournalEntry = journalentry;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(JournalEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JournalEntryExists(JournalEntry.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool JournalEntryExists(Guid id)
        {
            return _context.JournalEntry.Any(e => e.Id == id);
        }
    }
}
