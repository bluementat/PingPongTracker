using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin
{
    public class EditGameModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public Game GameToEdit { get; set; } = new();

        public EditGameModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(Guid GameId)
        {
            GameToEdit = _context.Games.Find(GameId) ?? new Game();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            GameToEdit.Player1WinnerId = GameToEdit.Team1Score > GameToEdit.Team2Score ? GameToEdit.Team1Player1Id : GameToEdit.Team2Player1Id;
            GameToEdit.Player2WinnerId = GameToEdit.Team1Score > GameToEdit.Team2Score ? GameToEdit.Team1Player2Id : GameToEdit.Team2Player2Id;

            // The following code has been added to ensure that no winner is selected in the case of a tie
            if (GameToEdit.Team1Score == GameToEdit.Team2Score)
            {
                GameToEdit.Player1WinnerId = Guid.Empty;
                GameToEdit.Player2WinnerId = Guid.Empty;
            }

            _context.Games.Update(GameToEdit);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin/Games");
        }
    }
}
