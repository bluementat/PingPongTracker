using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class AddPlayerModel : PageModel
    {        
        private readonly IPlayerRepository _playerRepository;
        
        [BindProperty]
        public Player NewPlayer { get; set; } = new();
        
        public AddPlayerModel(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
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
            await _playerRepository.AddPlayer(NewPlayer);                        
            return RedirectToPage("Players");
        }
    }
}