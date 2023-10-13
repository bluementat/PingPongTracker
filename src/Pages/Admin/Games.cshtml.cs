using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin
{
    public class GamesModel : PageModel
    {
        private readonly IGameRepository _gameRepository;

        [BindProperty]
        public List<Game> Games { get; set; } = new();

        public GamesModel(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public void OnGet()
        {
            Games = _gameRepository.GetGames().ToList();
        }
    }
}
