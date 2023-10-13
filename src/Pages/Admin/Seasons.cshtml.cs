using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;
using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin
{
    public class SeasonModel : PageModel
    {
        private readonly ISeasonRepository _seasonRepository;

        public List<Season> Seasons { get; set; } = new();
        
        public SeasonModel(ISeasonRepository seasonRepository)
        {
            _seasonRepository = seasonRepository;
        }

        public void OnGet()
        {
            Seasons = _seasonRepository.GetSeasons().OrderBy(s => s.SeasonStart).ToList();
        }
    }
}
