using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin
{
    public class AddSeasonModel : PageModel
    {
        private ApplicationDbContext _context;
        private readonly ISeasonRepository _repo;

        [BindProperty]
        public Season SeasonToAdd { get; set; } = new();

        public AddSeasonModel(ApplicationDbContext context, ISeasonRepository Repo)
        {
            _context = context;
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

            // Are there any Teams? If there are, this indicates that a Season has already been created and that there is
            // an active torunament. Check if the SeasonToAdd is set to Active.
            if (SeasonToAdd.Active && _context.Teams.Any())
            {
                SeasonToAdd.Active = false;
                await _repo.AddSeason(SeasonToAdd);

                return RedirectToPage("ActiveSeasonVerify", new { SeasonId = SeasonToAdd.SeasonId });
            }

            await _repo.AddSeason(SeasonToAdd);
            return RedirectToPage("Seasons");
        }
    }
}
