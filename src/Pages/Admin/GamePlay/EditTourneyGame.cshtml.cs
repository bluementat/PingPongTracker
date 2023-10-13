using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PingPongTracker.Data;
using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin.GamePlay
{
    [Authorize(Roles = "Admin")]
    public class EditTourneyGameModel : PageModel
    {
        private readonly ITourneyGameRepository _tgRepository;

        [BindProperty]
        public GameViewModel GameToEdit { get; set; } = new GameViewModel();

        public EditTourneyGameModel(ITourneyGameRepository TGRepository)
        {
            _tgRepository = TGRepository;
        }

        public async Task OnGet(Guid GameId)
        {
            var Game = await _tgRepository.GetTourneyGameById(GameId) ?? new TourneyGame();

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

            var Game = await _tgRepository.GetTourneyGameById(GameToEdit.GameId) ?? new TourneyGame();

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

            await _tgRepository.UpdateTourneyGame(Game);
            
            return RedirectToPage("/Admin/GamePlay/Tournament");
        }

    }
}
