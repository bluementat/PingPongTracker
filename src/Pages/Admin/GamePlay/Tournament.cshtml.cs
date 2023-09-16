using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Extensions;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin.GamePlay
{
    public class TournamentModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IPlayerRepository _playerRepository;
        private IEnumerable<Player> _activePlayers;

        public record EligiblePlayers(Guid PlayerId, string UserName, bool Eligible);
        [BindProperty]
        public List<Team> CurrentTeams { get; set; } = new List<Team>();
        [BindProperty]
        public List<EligiblePlayers> EligiblePlayersList { get; set; } = new();
        [BindProperty]
        public Season? CurrentSeason { get; set; }
        [BindProperty]
        public List<GameViewModel> Games { get; set; } = new List<GameViewModel>();

        public TournamentModel(ApplicationDbContext context, IPlayerRepository playerRepository)
        {
            _context = context;
            _playerRepository = playerRepository;
            _activePlayers = _playerRepository.GetActivePlayers();
        }


        public void OnGet()
        {
            // If there is no active season, show nothing            
            CurrentSeason = _context.Seasons.Where(s => s.Active).FirstOrDefault();
            if (CurrentSeason != null)
            {
                // If there are Teams defined, then we are in a season. Do not show the eligible players list.
                if (_context.Teams.Count() > 0)
                {
                    CurrentTeams = _context.Teams.ToList();

                    var tourneyGames = _context.TourneyGames.ToList();
                    foreach (var game in tourneyGames)
                    {                        
                        var team1 = $"{_playerRepository.GetUserNameById(game.Team1Player1Id)} & {_playerRepository.GetUserNameById(game.Team1Player2Id)}";
                        var team2 = $"{_playerRepository.GetUserNameById(game.Team2Player1Id)} & {_playerRepository.GetUserNameById(game.Team2Player2Id)}";

                        Games.Add(new GameViewModel
                        {
                            GameId = game.GameId,
                            Team1Name = team1,
                            Team2Name = team2,
                            Team1Score = game.Team1Score,
                            Team2Score = game.Team2Score
                        });
                    }
                }
                else
                {
                    foreach (var player in _activePlayers)
                    {
                        EligiblePlayersList.Add(new EligiblePlayers(player.PlayerId, player.UserName, player.Eligible));
                    }
                }
            }
        }

        public IActionResult OnPostMakeTeams()
        {
            // Randomly add Eligible Players teams
            var eligiblePlayers = EligiblePlayersList.Where(p => p.Eligible == true).ToList();
            var random = new Random();
            var shuffledPlayers = eligiblePlayers.OrderBy(p => random.Next()).ToList();
            var CandidateTeams = new List<Team>();

            if (shuffledPlayers.Count % 2 != 0)
            {
                for (int i = 0; i < shuffledPlayers.Count - 1; i += 2)
                {
                    CandidateTeams.Add(new Team
                    {
                        Player1Id = shuffledPlayers[i].PlayerId,
                        Player1UserName = shuffledPlayers[i].UserName,
                        Player2Id = shuffledPlayers[i + 1].PlayerId,
                        Player2UserName = shuffledPlayers[i + 1].UserName
                    });
                }
                CandidateTeams.Add(new Team
                {
                    Player1Id = Guid.Empty,
                    Player1UserName = "ODD PERSON OUT",
                    Player2Id = shuffledPlayers[shuffledPlayers.Count - 1].PlayerId,
                    Player2UserName = shuffledPlayers[shuffledPlayers.Count - 1].UserName
                });
            }
            else
            {
                for (int i = 0; i < shuffledPlayers.Count; i += 2)
                {
                    CandidateTeams.Add(new Team
                    {
                        Player1Id = shuffledPlayers[i].PlayerId,
                        Player1UserName = shuffledPlayers[i].UserName,
                        Player2Id = shuffledPlayers[i + 1].PlayerId,
                        Player2UserName = shuffledPlayers[i + 1].UserName
                    });
                }
            }

            HttpContext.Session.Set<List<Team>>("CandidateTeams", CandidateTeams);
            return RedirectToPage("/Admin/GamePlay/TourneyAccept");
        }

        public async Task<IActionResult> OnPostTourneyComplete()
        {
            _context.Teams.RemoveRange(_context.Teams);

            await _context.SaveChangesAsync();

            foreach (var player in _activePlayers)
            {
                EligiblePlayersList.Add(new EligiblePlayers(player.PlayerId, player.UserName, player.Eligible));
            }

            return RedirectToPage("/Index");
        }
    }
}
