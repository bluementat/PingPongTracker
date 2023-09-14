using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PingPongTracker.Data;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin.GamePlay
{
    public class EditTeamModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public Team TeamToEdit { get; set; } = new Team();

        public SelectList ActivePlayerSelectList { get; set; } = new SelectList(new List<TeamPlayerViewModel>());

        public EditTeamModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(int TeamID)
        {
            TeamToEdit = _context.Teams.Find(TeamID) ?? new Team();

            ActivePlayerSelectList = new SelectList(_context.Players.Where(p => p.Active).Select(p => new TeamPlayerViewModel { PlayerId = p.PlayerId, UserName = p.UserName }).ToList(), "PlayerId", "UserName");
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("/Admin/GamePlay/Tournament");
        }
    }
}
