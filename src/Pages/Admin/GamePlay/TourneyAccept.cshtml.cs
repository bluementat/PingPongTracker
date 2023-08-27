using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin.GamePlay
{
    public class TourneyAcceptModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public Tournament Tournament { get; set; } = new();

        [BindProperty]
        public IEnumerable<TeamViewModel> Teams { get; set; } = new List<TeamViewModel>();

        public TourneyAcceptModel(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public void OnGet(Guid id)
        {
            Tournament = _context.Tournaments.Find(id) ?? new Tournament();

            for(var i = 0; i < Tournament.Players.Count; i += 2)
            {
                if (i + 1 >= Tournament.Players.Count)
                {
                    Teams.Append(new TeamViewModel
                    {
                        TeamName = $" Odd-Person Out: {Tournament.Players[i].UserName}",
                        Player1Id = Tournament.Players[i].PlayerId,
                        Player2Id = Guid.Empty,
                        Score = 0
                    });
                    break;
                }
                Teams.Append(new TeamViewModel
                {
                    TeamName = $"{Tournament.Players[i].UserName} & {Tournament.Players[i + 1].UserName}",
                    Player1Id = Tournament.Players[i].PlayerId,
                    Player2Id = Tournament.Players[i + 1].PlayerId,
                    Score = 0
                });
            }            
        }
        
    }
}
