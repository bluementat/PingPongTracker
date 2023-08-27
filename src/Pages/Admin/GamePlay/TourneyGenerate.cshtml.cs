using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin.GamePlay
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

        public async Task<IActionResult> OnPostAsync()
        {
            var Tournament = new Tournament();

            // Add Eligible Players to the Torunament in Random Order
            var eligiblePlayers = EligiblePlayersList.Where(p => p.Eligible == true).ToList();
            var random = new Random();
            var shuffledPlayers = eligiblePlayers.OrderBy(p => random.Next()).ToList();
            foreach(var player in shuffledPlayers)
            {
                Tournament.Players.Add(new Player { PlayerId = player.PlayerId });
            }

            // Add Tournament to Database
            _context.Tournaments.Add(Tournament);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/GamePlay/TourneyAccept", new { id = Tournament.TournamentId });
        }
    }
}
