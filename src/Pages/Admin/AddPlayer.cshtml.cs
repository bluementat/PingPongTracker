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

        public void OnGet()
        {
        }

        // Post the new user to the database
        public async Task<IActionResult> OnPostAsync(Player newPlayer)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Players.Add(newPlayer);
            await _db.SaveChangesAsync();
            return RedirectToPage("Players");
        }
    }
}