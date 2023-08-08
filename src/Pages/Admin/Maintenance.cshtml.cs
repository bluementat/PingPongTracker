using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    [Authorize(Roles = "Admin")]
    public class MaitenanceModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
