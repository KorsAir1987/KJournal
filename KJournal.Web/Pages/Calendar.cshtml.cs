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
    }
}
