using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class PlayersModel : PageModel
    {
        private readonly IPlayerRepository _repo;
        
        public List<Player> Players { get; set; } = new();

        // constructor
        public PlayersModel(IPlayerRepository repo)
        {
            _repo = repo;
            Players = _repo.GetPlayers();
        }
        
        
        public void OnGet()
        {
        }
    }
}
