using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin.GamePlay
{
    public class TourneyGenerateModel : PageModel
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ApplicationDbContext _context;
        private IEnumerable<Player> _activePlayers;    

        public record EligiblePlayers(Guid PlayerId, string UserName, bool Eligible);

        [BindProperty]
        public List<EligiblePlayers> EligiblePlayersList { get; set; } = new();

        public TourneyGenerateModel(ApplicationDbContext context, IPlayerRepository playerRepository)
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
            // Add Eligible Players to the Torunament in Random Order
            var eligiblePlayers = EligiblePlayersList.Where(p => p.Eligible == true).ToList();
            var random = new Random();
            var shuffledPlayers = eligiblePlayers.OrderBy(p => random.Next()).ToList();

            if(shuffledPlayers.Count % 2 != 0)
            {
                _context.Teams.Add(new Team { Player1Id = shuffledPlayers[0].PlayerId, Player1UserName = shuffledPlayers[0].UserName });

                for( int i = 1; i < shuffledPlayers.Count; i++)
                {
                   _context.Teams.Add(new Team 
                   { 
                        Player1Id = shuffledPlayers[i].PlayerId, 
                        Player1UserName = shuffledPlayers[i].UserName,
                        Player2Id = shuffledPlayers[i+1].PlayerId,
                        Player2UserName = shuffledPlayers[i+1].UserName 
                    });
                }
            }
            else
            {
                for( int i = 0; i < shuffledPlayers.Count; i++)
                {
                   _context.Teams.Add(new Team 
                   { 
                        Player1Id = shuffledPlayers[i].PlayerId, 
                        Player1UserName = shuffledPlayers[i].UserName,
                        Player2Id = shuffledPlayers[i+1].PlayerId,
                        Player2UserName = shuffledPlayers[i+1].UserName 
                    });
                }
            }
            
            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin/GamePlay/TourneyAccept");
        }
    }
}
