using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KJournal.Web.Pages
{
    [Authorize]
    public class CalendarModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPostSaveNote()
        {
            // Read the incoming JSON body
            using (var reader = new StreamReader(HttpContext.Request.Body))
            {
                var body = reader.ReadToEndAsync().Result;

                // Deserialize into the custom object
                var noteRequest = JsonSerializer.Deserialize<NoteRequest>(body);

                if (noteRequest != null)
                {
                    // Access the data
                    DateTime date = noteRequest.Date;
                    string note = noteRequest.Note;

                    if (string.IsNullOrEmpty(note))
                    {
                        // Handle deletion (e.g., remove note from database or storage)
                        Console.WriteLine($"Deleting note for date: {date.ToShortDateString()}");
                    }
                    else
                    {
                        // Handle saving (e.g., save note to database or storage)
                        Console.WriteLine($"Saving note for date: {date.ToShortDateString()}, note: {note}");
                    }

                    // Return a success response
                    return new JsonResult(new { success = true, message = "Note processed successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Invalid request data" });
                }
            }
        }
    }

    public class NoteRequest
    {
        public DateTime Date { get; set; }
        public string Note { get; set; }
    }
}
