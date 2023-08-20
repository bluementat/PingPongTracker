using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    public IEnumerable<PlayerStandingViewModel> Players { get; set; } = new List<PlayerStandingViewModel>();

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

        // Get and displkay the current season
        var CurrentSeason = _context.Seasons.Where(s => s.Active).FirstOrDefault();
        if (CurrentSeason != null)
        {
            SeasonTitle = CurrentSeason.SeasonName;
            SeasonStartDate = " - " + CurrentSeason.SeasonStart.ToString("MMMM dd, yyyy");
        }
        else
        {
            SeasonTitle = "No Season Active";
            SeasonStartDate = string.Empty;
        }

        IEnumerable<PlayerStandingViewModel> PreSort = new List<PlayerStandingViewModel>();

        // Get all active players
        var players = _context.Players.Where(p => p.Active).ToList();

        foreach(var player in players)
        {
            var wins = _context.Games.Where(g => g.Player1WinnerId == player.PlayerId).Count();
            wins += _context.Games.Where(g => g.Player2WinnerId == player.PlayerId).Count();            
            var totalGames = _context.Games.Where(g => g.Team1Player1Id == player.PlayerId || g.Team1Player2Id == player.PlayerId 
                || g.Team2Player1Id == player.PlayerId || g.Team2Player2Id == player.PlayerId).Count();
            var winPercentage = totalGames == 0 ? 0 : (int)Math.Round((double)wins / totalGames * 100);
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

        Players = PreSort.OrderBy(p => p.WinPercentage).ThenBy(p => p.Wins).ThenBy(p => p.Losses).ToList();

    }
}
