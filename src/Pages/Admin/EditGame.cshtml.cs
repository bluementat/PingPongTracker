using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin
{
    public class EditGameModel : PageModel
    {        
        private readonly IGameRepository _gameRepository;

        [BindProperty]
        public Game GameToEdit { get; set; } = new();

        public EditGameModel(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task OnGet(Guid GameId)
        {
            GameToEdit = await _gameRepository.GetGameAsync(GameId) ?? new Game();
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

            await _gameRepository.UpdateGameAsync(GameToEdit);
            return RedirectToPage("/Admin/Games");
        }
    }
}
