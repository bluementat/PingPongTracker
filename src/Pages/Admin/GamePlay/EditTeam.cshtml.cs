using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PingPongTracker.Data;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin.GamePlay
{
    [Authorize(Roles = "Admin")]
    public class EditTeamModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IPlayerRepository _playerrepo;

        [BindProperty]
        public Team TeamToEdit { get; set; } = new Team();

        public SelectList ActivePlayerSelectList { get; set; } = new SelectList(new List<TeamPlayerViewModel>());

        public EditTeamModel(ApplicationDbContext context, IPlayerRepository playerrepo)
        {
            _context = context;
            _playerrepo = playerrepo;
        }

        public void OnGet(int TeamID)
        {
            TeamToEdit = _context.Teams.Find(TeamID) ?? new Team();
            ActivePlayerSelectList = GetPlayerOptions();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ActivePlayerSelectList = GetPlayerOptions();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (TeamToEdit.Player1Id == TeamToEdit.Player2Id)
            {
                ModelState.AddModelError(string.Empty, "Player 1 and Player 2 cannot be the same player");
                return Page();
            }

            TeamToEdit.Player1UserName = _playerrepo.GetUserNameById(TeamToEdit.Player1Id);
            TeamToEdit.Player2UserName = _playerrepo.GetUserNameById(TeamToEdit.Player2Id);

            _context.Teams.Update(TeamToEdit);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin/GamePlay/Tournament");
        }

        private SelectList GetPlayerOptions()
        {
            List<TeamPlayerViewModel> PlayerOptions = new List<TeamPlayerViewModel>
            {
                new TeamPlayerViewModel { PlayerId = Guid.Empty, UserName = "Select Player" }
            };
            PlayerOptions.AddRange(_context.Players.Where(p => p.Active).Select(p => new TeamPlayerViewModel { PlayerId = p.PlayerId, UserName = p.UserName }).ToList());

            return new SelectList(PlayerOptions, "PlayerId", "UserName");
        }
    }
}
