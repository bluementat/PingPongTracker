using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class AddPlayerModel : PageModel
    {        
        private readonly ApplicationDbContext _db;
        
        [BindProperty]
        public Player NewPlayer { get; set; } = new();
        
        public AddPlayerModel(ApplicationDbContext db)
        {
            _db = db;
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
            
            _db.Players.Add(NewPlayer);            
            await _db.SaveChangesAsync();
            
            return RedirectToPage("Players");
        }
    }
}