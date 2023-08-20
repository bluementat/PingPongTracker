using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin
{
    public class AddSeasonModel : PageModel
    {
        private readonly ISeasonRepository _repo;

        [BindProperty]
        public Season SeasonToAdd { get; set; } = new();
        
        public AddSeasonModel(ISeasonRepository Repo)
        {
            _repo = Repo;
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

            await _repo.AddSeason(SeasonToAdd);            
            return RedirectToPage("Seasons");
        }
    }
}
