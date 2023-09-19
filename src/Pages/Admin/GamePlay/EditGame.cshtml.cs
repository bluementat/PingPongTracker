using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PingPongTracker.Data;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin.GamePlay
{
    public class EditGameModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public GameViewModel GameToEdit { get; set; } = new GameViewModel();

        public SelectList TeamSelectList { get; set; } = new SelectList(new List<TeamOptionViewModel>());

        public EditGameModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(Guid GameId)
        {
            TeamSelectList = GetTeamOptions();
            var Game = _context.TourneyGames.Find(GameId) ?? new TourneyGame();

        }



        private SelectList GetTeamOptions()
        {
            List<TeamOptionViewModel> TeamOptions = new List<TeamOptionViewModel>();

            foreach (Team team in _context.Teams)
            {
                TeamOptions.Add(new TeamOptionViewModel
                {
                    Value = team.TeamID,
                    Text = $"{team.Player1UserName} and {team.Player2UserName}"
                });
            }

            return new SelectList(TeamOptions, "Value", "Text");
        }
    }
}
