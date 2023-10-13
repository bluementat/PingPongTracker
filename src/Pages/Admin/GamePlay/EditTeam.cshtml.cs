using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PingPongTracker.Data;
using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin.GamePlay
{
    [Authorize(Roles = "Admin")]
    public class EditTeamModel : PageModel
    {        
        private readonly ITeamRepository _teamRepository;
        private readonly IPlayerRepository _playerRepository;

        [BindProperty]
        public Team TeamToEdit { get; set; } = new Team();

        public SelectList ActivePlayerSelectList { get; set; } = new SelectList(new List<TeamPlayerViewModel>());

        public EditTeamModel(ITeamRepository teamRepository, IPlayerRepository playerrepo)
        {
            _teamRepository = teamRepository;
            _playerRepository = playerrepo;
        }

        public async Task OnGet(int TeamID)
        {
            TeamToEdit = await _teamRepository.GetTeamAsync(TeamID) ?? new Team();
            ActivePlayerSelectList = await GetPlayerOptions();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ActivePlayerSelectList = await GetPlayerOptions();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (TeamToEdit.Player1Id == TeamToEdit.Player2Id)
            {
                ModelState.AddModelError(string.Empty, "Player 1 and Player 2 cannot be the same player");
                return Page();
            }

            TeamToEdit.Player1UserName = _playerRepository.GetUserNameById(TeamToEdit.Player1Id);
            TeamToEdit.Player2UserName = _playerRepository.GetUserNameById(TeamToEdit.Player2Id);

            await _teamRepository.UpdateTeamAsync(TeamToEdit);            
            return RedirectToPage("/Admin/GamePlay/Tournament");
        }

        private async Task<SelectList> GetPlayerOptions()
        {
            List<TeamPlayerViewModel> PlayerOptions = new List<TeamPlayerViewModel>
            {
                new TeamPlayerViewModel { PlayerId = Guid.Empty, UserName = "Select Player" }
            };
            
            var players = await _playerRepository.GetPlayers();            
            PlayerOptions.AddRange(players.Where(p => p.Active).Select(p => new TeamPlayerViewModel { PlayerId = p.PlayerId, UserName = p.UserName }).ToList());

            return new SelectList(PlayerOptions, "PlayerId", "UserName");
        }
    }
}
