using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Versioning;
using PingPongTracker.Data;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin.GamePlay
{
    [Authorize(Roles = "Admin")]
    public class AddGameModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public GameViewModel GameToAdd { get; set; } = new GameViewModel();

        public SelectList TeamSelectList { get; set; } = new SelectList(new List<TeamOptionViewModel>());

        public AddGameModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            TeamSelectList = GetTeamOptions();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            TeamSelectList = GetTeamOptions();

            var Team1 = _context.Teams.Find(GameToAdd.Team1Id) ?? new Team();
            var Team2 = _context.Teams.Find(GameToAdd.Team2Id) ?? new Team();
            var CurrentSeason = _context.Seasons.Where(s => s.Active == true).FirstOrDefault() ?? new Season();

            var NewGame = new TourneyGame
            {
                Team1Name = GetTeamOptions().First(t => t.Value == GameToAdd.Team1Id.ToString()).Text,
                Team1Player1Id = Team1.Player1Id,
                Team1Player2Id = Team1.Player2Id,
                Team2Name = GetTeamOptions().First(t => t.Value == GameToAdd.Team2Id.ToString()).Text,
                Team2Player1Id = Team2.Player1Id,
                Team2Player2Id = Team2.Player2Id,
                Team1Score = GameToAdd.Team1Score,
                Team2Score = GameToAdd.Team2Score,
                Player1WinnerId = GameToAdd.Team1Score > GameToAdd.Team2Score ? Team1.Player1Id : Team2.Player1Id,
                Player2WinnerId = GameToAdd.Team1Score > GameToAdd.Team2Score ? Team1.Player2Id : Team2.Player2Id,
                MatchupDate = DateTime.Now,
                SeasonId = CurrentSeason.SeasonId
            };

            _context.TourneyGames.Add(NewGame);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin/GamePlay/Tournament");
        }




        private SelectList GetTeamOptions()
        {
            List<TeamOptionViewModel> TeamOptions = new List<TeamOptionViewModel>();

            foreach (Team team in _context.Teams)
            {
                TeamOptions.Add(new TeamOptionViewModel
                {
                    Value = team.TeamID,
                    Text = GamePlayUtilities.CreateTeamName(team.Player1UserName, team.Player2UserName)
                });
            }

            return new SelectList(TeamOptions, "Value", "Text");
        }
    }
}
