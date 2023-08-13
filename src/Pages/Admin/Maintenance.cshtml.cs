using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static PingPongTracker.Data.MaintenanceOptions;

namespace PingPongTracker.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class MaintenanceModel : PageModel
    {
        public List<MaintenanceOption> Options { get; set; } = GetMaintenanceOptions().ToList();

        public void OnGet()
        {
        }
    }
}
