using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin
{
    public class ActiveSeasonVerifyModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ISeasonRepository _seasonRepository;
        private readonly ITourneyGameRepository _TGRepository;

        [BindProperty]
        public Season SeasonToUpdate { get; set; } = new();

        public ActiveSeasonVerifyModel(ApplicationDbContext context, ISeasonRepository Repo, ITourneyGameRepository TGRepo)
        {
            _context = context;
            _seasonRepository = Repo;
            _TGRepository = TGRepo;
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
            await _TGRepository.RemoveRange(_TGRepository.GetTourneyGames().ToList());
            _context.Teams.RemoveRange(_context.Teams);
            await _context.SaveChangesAsync();

            SeasonToUpdate.Active = true;
            await _seasonRepository.UpdateSeason(SeasonToUpdate);

            return RedirectToPage("Seasons");
        }
    }
}
