using System.Security.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using PingPongTracker.Data;
using PingPongTracker.Models;
using PingPongTracker.Pages.Admin;
using static PingPongTracker.Data.Greetings;

namespace PingPongTracker.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ApplicationDbContext _context;

    public Greeting greeting { get; set; } = new Greeting(0, string.Empty, string.Empty, string.Empty);
    public string SeasonTitle { get; set; } = string.Empty;
    public string SeasonStartDate { get; set; } = string.Empty;
    public List<Team> CurrentTeams { get; set; } = new List<Team>();
    public IEnumerable<PlayerStandingViewModel> SeasonStandings { get; set; } = new List<PlayerStandingViewModel>();
    public IEnumerable<PlayerStandingViewModel> AllTimeStandings { get; set; } = new List<PlayerStandingViewModel>();
    public Season? CurrentSeason { get; set; }

    public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext Context)
    {
        _context = Context;
        _logger = logger;
    }

    public void OnGet()
    {

        // Randomly select a greeting
        var greetings = Greetings.GetGreetings().ToList();
        greeting = greetings[new Random().Next(0, greetings.Count)];

        // Get all active players and create a PreSort list
        var players = _context.Players.Where(p => p.Active).ToList();
        IEnumerable<PlayerStandingViewModel> PreSort = new List<PlayerStandingViewModel>();

        // Get and display the current season and the standings, if there is one.
        CurrentSeason = _context.Seasons.Where(s => s.Active).FirstOrDefault();
        if (CurrentSeason != null)
        {
            CurrentTeams = _context.Teams.ToList();

            SeasonTitle = CurrentSeason.SeasonName;
            SeasonStartDate = " - " + CurrentSeason.SeasonStart.ToString("MMMM dd, yyyy");

            foreach (var player in players)
            {
                var wins = _context.Games.Where(g => g.Player1WinnerId == player.PlayerId && g.SeasonId == CurrentSeason.SeasonId).Count();
                wins += _context.Games.Where(g => g.Player2WinnerId == player.PlayerId && g.SeasonId == CurrentSeason.SeasonId).Count();
                var totalGames = _context.Games.Where(g => g.Team1Player1Id == player.PlayerId && g.SeasonId == CurrentSeason.SeasonId
                    || g.Team1Player2Id == player.PlayerId && g.SeasonId == CurrentSeason.SeasonId
                    || g.Team2Player1Id == player.PlayerId && g.SeasonId == CurrentSeason.SeasonId
                    || g.Team2Player2Id == player.PlayerId && g.SeasonId == CurrentSeason.SeasonId).Count();
                var winPercentage = totalGames == 0 ? 0 : Math.Round((double)wins / totalGames * 100, 3);
                var losses = totalGames - wins;
                PreSort = PreSort.Append(new PlayerStandingViewModel
                {
                    Rank = 0,
                    UserName = player.UserName,
                    Wins = wins,
                    Losses = losses,
                    TotalGames = totalGames,
                    WinPercentage = winPercentage
                });

                SeasonStandings = PreSort.OrderByDescending(p => p.WinPercentage).ThenBy(p => p.Wins).ThenBy(p => p.Losses).ToList();

                for (int i = 0; i < SeasonStandings.Count(); i++)
                {
                    SeasonStandings.ElementAt(i).Rank = i + 1;
                }
            }
        }
        else
        {
            SeasonTitle = "No Season Active";
            SeasonStartDate = string.Empty;
        }

        // Get and display the all-time standings
        PreSort = new List<PlayerStandingViewModel>();
        players = _context.Players.ToList();
        foreach (var player in players)
        {
            var wins = _context.Games.Where(g => g.Player1WinnerId == player.PlayerId).Count();
            wins += _context.Games.Where(g => g.Player2WinnerId == player.PlayerId).Count();
            var totalGames = _context.Games.Where(g => g.Team1Player1Id == player.PlayerId || g.Team1Player2Id == player.PlayerId
                || g.Team2Player1Id == player.PlayerId || g.Team2Player2Id == player.PlayerId).Count();
            var winPercentage = totalGames == 0 ? 0 : Math.Round((double)wins / totalGames * 100, 3);
            var losses = totalGames - wins;

            PreSort = PreSort.Append(new PlayerStandingViewModel
            {
                Rank = 0,
                UserName = player.UserName,
                Wins = wins,
                Losses = losses,
                TotalGames = totalGames,
                WinPercentage = winPercentage
            });
        }

        AllTimeStandings = PreSort.OrderByDescending(p => p.WinPercentage).ThenBy(p => p.Wins).ThenBy(p => p.Losses).ToList();

        for (int i = 0; i < AllTimeStandings.Count(); i++)
        {
            AllTimeStandings.ElementAt(i).Rank = i + 1;
        }

    }
}
