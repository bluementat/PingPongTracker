using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PingPongTracker.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class MaitenanceModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
