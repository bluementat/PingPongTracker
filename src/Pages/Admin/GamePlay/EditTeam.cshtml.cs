using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin.GamePlay
{
    public class EditTeamModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public Team Team { get; set; } = new Team();

        public EditTeamModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(int TeamID)
        {
            Team = _context.Teams.Find(TeamID) ?? new Team();
        }
    }
}
