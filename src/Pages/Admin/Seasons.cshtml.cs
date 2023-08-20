using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin
{
    public class SeasonModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public List<Season> Seasons { get; set; } = new();
        
        public SeasonModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Seasons = _context.Seasons.OrderBy(s => s.SeasonStart).ToList();
        }
    }
}
