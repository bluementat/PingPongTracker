using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin
{
    public class ActiveSeasonVerifyModel : PageModel
    {        
        private readonly ISeasonRepository _seasonRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ITourneyGameRepository _tgRepository;

        [BindProperty]
        public Season SeasonToUpdate { get; set; } = new();

        public ActiveSeasonVerifyModel(ISeasonRepository seasonRepository, ITeamRepository teamRepository, ITourneyGameRepository TGRepo)
        {            
            _seasonRepository = seasonRepository;
            _teamRepository = teamRepository;
            _tgRepository = TGRepo;
        }


        public async Task OnGet(Guid SeasonId)
        {
            SeasonToUpdate = await _seasonRepository.GetSeasonById(SeasonId);
        }

        public IActionResult OnPostNewSeasonInactive()
        {
            return RedirectToPage("Seasons");
        }

        public async Task<IActionResult> OnPostActivateNewSeason()
        {
            await _tgRepository.RemoveRange(_tgRepository.GetTourneyGames().ToList());
            _teamRepository.RemoveRange(_teamRepository.GetTeams().ToList());            

            SeasonToUpdate.Active = true;
            await _seasonRepository.UpdateSeason(SeasonToUpdate);

            return RedirectToPage("Seasons");
        }
    }
}
