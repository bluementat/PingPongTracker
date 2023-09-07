using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Extensions;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin.GamePlay
{
    public class TourneyAcceptModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public List<Team> ProposedTeams { get; set; } = new List<Team>();        
        
        public TourneyAcceptModel(ApplicationDbContext context)
        {            
            _context = context;            
        }
        
        public void OnGet(Guid id)
        {
            ProposedTeams = HttpContext.Session.Get<List<Team>>("CandidateTeams");
        } 

        public async Task<IActionResult> OnPostAcceptTeams()
        {
            ProposedTeams = HttpContext.Session.Get<List<Team>>("CandidateTeams");
            _context.Teams.AddRange(ProposedTeams);
            await _context.SaveChangesAsync();

            return RedirectToPage("Tournament");
        }    

        public IActionResult OnPostNewTeams()
        {
            // Delete the teams
            HttpContext.Session.Set<List<Team>>("CandidateTeams", new List<Team>());
            
            return RedirectToPage("Tournament");
        }   
    }
}
