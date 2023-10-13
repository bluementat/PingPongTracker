using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Data.Interfaces;
using PingPongTracker.Extensions;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin.GamePlay
{
    public class TourneyAcceptModel : PageModel
    {
        private readonly ITeamRepository _teamRepository;

        [BindProperty]
        public List<Team> ProposedTeams { get; set; } = new List<Team>();        
        
        public TourneyAcceptModel(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }        
        
        public void OnGet(Guid id)
        {
            ProposedTeams = HttpContext.Session.Get<List<Team>>("CandidateTeams");
        } 

        public async Task<IActionResult> OnPostAcceptTeams()
        {
            ProposedTeams = HttpContext.Session.Get<List<Team>>("CandidateTeams");
            await _teamRepository.AddRangeAsync(ProposedTeams);            

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
