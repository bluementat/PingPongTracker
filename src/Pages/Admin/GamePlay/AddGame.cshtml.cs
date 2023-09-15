using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PingPongTracker.Data;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin.GamePlay
{
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


        private SelectList GetTeamOptions()
        {
            List<TeamOptionViewModel> TeamOptions = new List<TeamOptionViewModel>();

            foreach (Team team in _context.Teams)
            {
                TeamOptions.Add(new TeamOptionViewModel
                {
                    TeamID = team.TeamID,
                    TeamName = $"{team.Player1UserName} and {team.Player2UserName}"
                });
            }

            return new SelectList(TeamOptions, "TeamID", "TeamName");
        }
    }
}
