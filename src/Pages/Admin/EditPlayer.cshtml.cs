using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Differencing;
using PingPongTracker.Data;
using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class EditPlayerModel : PageModel
    {
        private readonly IPlayerRepository _playerRepository;
        
        [BindProperty]
        public Player PlayerToEdit { get; set; } = new();
        
        public EditPlayerModel(IPlayerRepository repo)
        {
            _playerRepository = repo;            
        }
        
        public async Task OnGet(Guid PlayerID)
        {
            PlayerToEdit = await _playerRepository.GetPlayerById(PlayerID);
        }  

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!_playerRepository.GoodUserNameChange(PlayerToEdit))
            {
                ModelState.AddModelError(string.Empty, "Username already exists");
                return Page();
            }                                    
                                    
            await _playerRepository.UpdatePlayer(PlayerToEdit);                        
            return RedirectToPage("Players");
        }              
    }
}
