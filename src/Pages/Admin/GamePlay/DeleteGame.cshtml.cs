using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin.GamePlay
{
    [Authorize(Roles = "Admin")]
    public class DeleteGameModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public GameViewModel GameToDelete { get; set; } = new GameViewModel();

        public DeleteGameModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(Guid GameId)
        {
            var Game = _context.TourneyGames.Find(GameId) ?? new TourneyGame();

            GameToDelete.GameId = Game.GameId;
            GameToDelete.Team1Name = Game.Team1Name;
            GameToDelete.Team2Name = Game.Team2Name;
            GameToDelete.Team1Score = Game.Team1Score;
            GameToDelete.Team2Score = Game.Team2Score;
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var Game = _context.TourneyGames.Find(GameToDelete.GameId) ?? new TourneyGame();

            _context.TourneyGames.Remove(Game);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin/GamePlay/Tournament");
        }
    }
}
