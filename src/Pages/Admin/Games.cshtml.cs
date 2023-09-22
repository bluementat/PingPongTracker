using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin
{
    public class GamesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public List<Game> Games { get; set; } = new();

        public GamesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Games = _context.Games.ToList();
        }
    }
}
