using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class EditSeasonModel : PageModel
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ISeasonRepository _seasonRepository;

        [BindProperty]
        public Season SeasonToEdit { get; set; } = new();

        public EditSeasonModel(ISeasonRepository repo, ITeamRepository teamRepository)
        {            
            _seasonRepository = repo;
            _teamRepository = teamRepository;
        }

        public async Task OnGet(Guid SeasonID)
        {
            SeasonToEdit = await _seasonRepository.GetSeasonById(SeasonID);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Are there any Teams? If there are, this indicates that a Season has already been created and that there is
            // an active torunament. Check if the SeasonToAdd is set to Active.
            if (SeasonToEdit.Active && _teamRepository.GetTeams().Any())
            {
                SeasonToEdit.Active = false;
                await _seasonRepository.UpdateSeason(SeasonToEdit);

                return RedirectToPage("ActiveSeasonVerify", new { SeasonId = SeasonToEdit.SeasonId });
            }

            await _seasonRepository.UpdateSeason(SeasonToEdit);
            return RedirectToPage("Seasons");
        }
    }
}
