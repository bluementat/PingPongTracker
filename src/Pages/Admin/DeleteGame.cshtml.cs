using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class DeleteGameModel : PageModel
    {
        private readonly IGameRepository _gameRepository;

        [BindProperty]
        public GameViewModel GameToDelete { get; set; } = new GameViewModel();

        public DeleteGameModel(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }
        

        public async Task OnGet(Guid GameId)
        {
            var Game = await _gameRepository.GetGameAsync(GameId) ?? new Game();

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

            var Game = await _gameRepository.GetGameAsync(GameToDelete.GameId) ?? new Game();
            if(Game != new Game())
            {
                await _gameRepository.DeleteGameAsync(Game.GameId);                
            }
           
            return RedirectToPage("/Admin/Games");
        }
    }
}
