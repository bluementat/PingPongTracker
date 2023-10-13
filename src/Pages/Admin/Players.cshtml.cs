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
        private readonly IPlayerRepository _playerRepository;
        
        public IEnumerable<Player> Players { get; set; } = new List<Player>();

        // constructor
        public PlayersModel(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;            
        }        
        
        public async Task OnGet()
        {
            Players = await _playerRepository.GetPlayers();
        }
    }
}
