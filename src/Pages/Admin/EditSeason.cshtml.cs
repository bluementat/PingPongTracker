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
        private readonly ApplicationDbContext _context;
        private readonly ISeasonRepository _repo;

        [BindProperty]
        public Season SeasonToEdit { get; set; } = new();

        public EditSeasonModel(ApplicationDbContext context, ISeasonRepository repo)
        {
            _context = context;
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

            // Are there any Teams? If there are, this indicates that a Season has already been created and that there is
            // an active torunament. Check if the SeasonToAdd is set to Active.
            if (SeasonToEdit.Active && _context.Teams.Any())
            {
                SeasonToEdit.Active = false;
                await _repo.UpdateSeason(SeasonToEdit);

                return RedirectToPage("ActiveSeasonVerify", new { SeasonId = SeasonToEdit.SeasonId });
            }

            await _repo.UpdateSeason(SeasonToEdit);
            return RedirectToPage("Seasons");
        }
    }
}
