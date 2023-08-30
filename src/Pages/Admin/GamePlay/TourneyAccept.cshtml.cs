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
        public IEnumerable<Team> ProposedTeams { get; set; } = new List<Team>();        
        
        public TourneyAcceptModel(ApplicationDbContext context)
        {
            _context = context;   
            ProposedTeams = _context.Teams.ToList();         
        }
        
        public void OnGet(Guid id)
        {
            
        }        
    }
}
