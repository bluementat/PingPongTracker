using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class AddPlayerModel : PageModel
    {        
        private readonly IPlayerRepository _repo;
        
        [BindProperty]
        public Player NewPlayer { get; set; } = new();
        
        public AddPlayerModel(IPlayerRepository repo)
        {
            _repo = repo;
        }

        public IActionResult OnGet()
        {
            return Page();
        }                 
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }            
            await _repo.AddPlayer(NewPlayer);                        
            return RedirectToPage("Players");
        }
    }
}