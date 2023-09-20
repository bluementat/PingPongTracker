using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin
{
    public class ActiveSeasonVerifyModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ISeasonRepository _repo;

        [BindProperty]
        public Season SeasonToUpdate { get; set; } = new();

        public ActiveSeasonVerifyModel(ApplicationDbContext context, ISeasonRepository Repo)
        {
            _context = context;
            _repo = Repo;
        }


        public async Task OnGet(Guid SeasonId)
        {
            SeasonToUpdate = await _repo.GetSeasonById(SeasonId);
        }

        public IActionResult OnPostNewSeasonInactive()
        {
            return RedirectToPage("Seasons");
        }

        public async Task<IActionResult> OnPostActivateNewSeason()
        {
            _context.TourneyGames.RemoveRange(_context.TourneyGames);
            _context.Teams.RemoveRange(_context.Teams);
            await _context.SaveChangesAsync();

            SeasonToUpdate.Active = true;
            await _repo.UpdateSeason(SeasonToUpdate);

            return RedirectToPage("Seasons");
        }
    }
}
