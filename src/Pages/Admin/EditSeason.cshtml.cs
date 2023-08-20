using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class EditSeasonModel : PageModel
    {
        private readonly ISeasonRepository _repo;        
        
        [BindProperty]
        public Season SeasonToEdit { get; set; } = new();

        public EditSeasonModel(ISeasonRepository repo)
        {
            _repo = repo;            
        }
        
        public async Task OnGet(Guid SeasonID)
        {
            SeasonToEdit = await _repo.GetSeasonById(SeasonID);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repo.UpdateSeason(SeasonToEdit);
            return RedirectToPage("Seasons");
        }
    }
}
