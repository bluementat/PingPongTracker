using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
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
            ProposedTeams = _context.Teams.ToList();         
        }
        
        public void OnGet(Guid id)
        {
            
        } 

        public IActionResult OnPostAcceptTeams()
        {
            return RedirectToPage("Tournament");
        }    

        public async Task<IActionResult> OnPostNewTeams()
        {
            // Delete the teams
            var TheTeams = _context.Teams.ToList();
            _context.Teams.RemoveRange(TheTeams);
            await _context.SaveChangesAsync();
            
            return RedirectToPage("Tournament");
        }   
    }
}
