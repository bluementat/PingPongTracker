using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin
{
    public class AddSeasonModel : PageModel
    {        
        private readonly ISeasonRepository _seasonRepository;
        private readonly ITeamRepository _teamRepository;

        [BindProperty]
        public Season SeasonToAdd { get; set; } = new();

        public AddSeasonModel(ISeasonRepository Repo, ITeamRepository teamRepository)
        {            
            _seasonRepository = Repo;
            _teamRepository = teamRepository;
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
            if (SeasonToAdd.Active && _teamRepository.GetTeams().Any())
            {
                SeasonToAdd.Active = false;
                await _seasonRepository.AddSeason(SeasonToAdd);

                return RedirectToPage("ActiveSeasonVerify", new { SeasonId = SeasonToAdd.SeasonId });
            }

            await _seasonRepository.AddSeason(SeasonToAdd);
            return RedirectToPage("Seasons");
        }
    }
}
