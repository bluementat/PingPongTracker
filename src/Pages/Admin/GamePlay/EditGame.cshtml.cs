using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PingPongTracker.Data;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin.GamePlay
{
    [Authorize(Roles = "Admin")]
    public class EditGameModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public GameViewModel GameToEdit { get; set; } = new GameViewModel();

        public EditGameModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(Guid GameId)
        {
            var Game = _context.TourneyGames.Find(GameId) ?? new TourneyGame();

            GameToEdit.GameId = Game.GameId;
            GameToEdit.Team1Name = Game.Team1Name;
            GameToEdit.Team2Name = Game.Team2Name;
            GameToEdit.Team1Score = Game.Team1Score;
            GameToEdit.Team2Score = Game.Team2Score;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var Game = _context.TourneyGames.Find(GameToEdit.GameId) ?? new TourneyGame();

            Game.Team1Score = GameToEdit.Team1Score;
            Game.Team2Score = GameToEdit.Team2Score;
            Game.Player1WinnerId = GameToEdit.Team1Score > GameToEdit.Team2Score ? Game.Team1Player1Id : Game.Team2Player1Id;
            Game.Player2WinnerId = GameToEdit.Team1Score > GameToEdit.Team2Score ? Game.Team1Player2Id : Game.Team2Player2Id;

            // The following code has been added to ensure that no winner is selected in the case of a tie
            if (GameToEdit.Team1Score == GameToEdit.Team2Score)
            {
                Game.Player1WinnerId = Guid.Empty;
                Game.Player2WinnerId = Guid.Empty;
            }

            _context.TourneyGames.Update(Game);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin/GamePlay/Tournament");
        }

    }
}
