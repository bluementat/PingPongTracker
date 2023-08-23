using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin.Tournament
{
    public class TournamentModel : PageModel
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ApplicationDbContext _context;
        private IEnumerable<Player> _activePlayers;    

        public record EligiblePlayers(Guid PlayerId, string UserName, bool Eligible);

        [BindProperty]
        public List<EligiblePlayers> EligiblePlayersList { get; set; } = new();

        public TournamentModel(ApplicationDbContext context, IPlayerRepository playerRepository)
        {
            _context = context;
            _playerRepository = playerRepository;
            _activePlayers = _playerRepository.GetActivePlayers();
        }
        
        public void OnGet()
        {
             foreach(var player in _activePlayers)
             {
                 EligiblePlayersList.Add(new EligiblePlayers(player.PlayerId, player.UserName, player.Eligible));
             }
        }
    }
}
