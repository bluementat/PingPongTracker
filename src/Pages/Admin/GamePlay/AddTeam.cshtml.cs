using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PingPongTracker.Data;
using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin.GamePlay
{
    public class AddTeamModel : PageModel
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IPlayerRepository _playerRepository;

        [BindProperty]
        public Team TeamToAdd { get; set; } = new Team();

        public SelectList ListOfPlayers { get; set; } = new SelectList(new List<SelectListItem>());

        public AddTeamModel(ITeamRepository teamRepository,IPlayerRepository playerRepository)
        {
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
        }
        
        
        public void OnGet()
        {
            ListOfPlayers = GetPlayerOptions();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ListOfPlayers = GetPlayerOptions();

            var NewTeam = new Team
            {
                Player1Id = TeamToAdd.Player1Id,
                Player1UserName = ListOfPlayers.First(p => p.Value == TeamToAdd.Player1Id.ToString()).Text,
                Player2Id = TeamToAdd.Player2Id,
                Player2UserName = ListOfPlayers.First(p => p.Value == TeamToAdd.Player2Id.ToString()).Text
            };

            await _teamRepository.AddTeamAsync(NewTeam);            

            return RedirectToPage("/Admin/GamePlay/Tournament");
        }


        private SelectList GetPlayerOptions()
        {
            var Players = _playerRepository.GetActivePlayers();
            var PlayerOptions = from player in Players
                                select new SelectListItem
                                {
                                    Value = player.PlayerId.ToString(),
                                    Text = player.UserName
                                };

            return new SelectList(PlayerOptions, "Value", "Text");
        }
    }
}
