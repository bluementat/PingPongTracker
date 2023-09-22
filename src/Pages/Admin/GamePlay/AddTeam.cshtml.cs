using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PingPongTracker.Data;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin.GamePlay
{
    public class AddTeamModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IPlayerRepository _playerRepository;

        [BindProperty]
        public Team TeamToAdd { get; set; } = new Team();

        public SelectList ListOfPlayers { get; set; } = new SelectList(new List<SelectListItem>());

        public AddTeamModel(ApplicationDbContext context, IPlayerRepository playerRepository)
        {
            _context = context;
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

            _context.Teams.Add(NewTeam);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/GamePlay/Tournament");
        }


        private SelectList GetPlayerOptions()
        {
            var Players = _playerRepository.GetActivePlayers();
            var PlayerOptions = new List<SelectListItem>();

            foreach (var player in Players)
            {
                PlayerOptions.Add(new SelectListItem
                {
                    Value = player.PlayerId.ToString(),
                    Text = player.UserName
                });
            }

            return new SelectList(PlayerOptions, "Value", "Text");
        }
    }
}
