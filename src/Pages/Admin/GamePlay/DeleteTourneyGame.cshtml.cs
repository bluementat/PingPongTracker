using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin.GamePlay
{
    [Authorize(Roles = "Admin")]
    public class DeleteTourneyGameModel : PageModel
    {
        private readonly ITourneyGameRepository _tgRepository;

        [BindProperty]
        public GameViewModel GameToDelete { get; set; } = new GameViewModel();

        public DeleteTourneyGameModel(ITourneyGameRepository TGRepository)
        {
            _tgRepository = TGRepository;
        }

        public async void OnGet(Guid GameId)
        {
            var Game = await _tgRepository.GetTourneyGameById(GameId) ?? new TourneyGame();

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

            var Game = await _tgRepository.GetTourneyGameById(GameToDelete.GameId) ?? new TourneyGame();            
            await _tgRepository.DeleteTourneyGame(Game.GameId);
            
            return RedirectToPage("/Admin/GamePlay/Tournament");
        }
    }
}
