using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin
{
    public class PlayersModel : PageModel
    {
        public List<Player> Players { get; set; } = new();

        // constructor
        public PlayersModel(ApplicationDbContext db)
        {
            Players = db.Players.ToList();
        }
        
        public void OnGet()
        {
        }
    }
}
