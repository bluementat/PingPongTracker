using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin
{
    public class AddSeasonModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public Season SeasonToAdd { get; set; } = new();
        
        public AddSeasonModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Seasons.Add(SeasonToAdd);
            await _context.SaveChangesAsync();
            return RedirectToPage("Seasons");
        }
    }
}
