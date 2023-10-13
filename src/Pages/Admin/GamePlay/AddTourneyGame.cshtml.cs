using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PingPongTracker.Data;
using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;

namespace PingPongTracker.Pages.Admin.GamePlay
{
    [Authorize(Roles = "Admin")]
    public class AddTourneyGameModel : PageModel
    {        
        private readonly ITeamRepository _teamRepository;
        private readonly ITourneyGameRepository _tgRepository;
        private readonly ISeasonRepository _seasonRepository;

        [BindProperty]
        public GameViewModel GameToAdd { get; set; } = new GameViewModel();

        public SelectList TeamSelectList { get; set; } = new SelectList(new List<TeamOptionViewModel>());

        public AddTourneyGameModel(ITeamRepository teamRepository, ITourneyGameRepository TGRepository, ISeasonRepository seasonRepository)
        {
            _teamRepository = teamRepository;
            _tgRepository = TGRepository;
            _seasonRepository = seasonRepository;
        }

        public void OnGet()
        {
            TeamSelectList = GetTeamOptions();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            TeamSelectList = GetTeamOptions();

            var Team1 = await _teamRepository.GetTeamAsync(GameToAdd.Team1Id)  ?? new Team();
            var Team2 = await _teamRepository.GetTeamAsync(GameToAdd.Team2Id) ?? new Team();
            var CurrentSeason = _seasonRepository.GetSeasons().Where(s => s.Active == true).FirstOrDefault() ?? new Season();

            var NewGame = new TourneyGame
            {
                Team1Name = GetTeamOptions().First(t => t.Value == GameToAdd.Team1Id.ToString()).Text,
                Team1Player1Id = Team1.Player1Id,
                Team1Player2Id = Team1.Player2Id,
                Team2Name = GetTeamOptions().First(t => t.Value == GameToAdd.Team2Id.ToString()).Text,
                Team2Player1Id = Team2.Player1Id,
                Team2Player2Id = Team2.Player2Id,
                Team1Score = GameToAdd.Team1Score,
                Team2Score = GameToAdd.Team2Score,
                Player1WinnerId = GameToAdd.Team1Score > GameToAdd.Team2Score ? Team1.Player1Id : Team2.Player1Id,
                Player2WinnerId = GameToAdd.Team1Score > GameToAdd.Team2Score ? Team1.Player2Id : Team2.Player2Id,
                MatchupDate = DateTime.Now,
                SeasonId = CurrentSeason.SeasonId
            };

            await _tgRepository.AddTourneyGame(NewGame);            
            return RedirectToPage("/Admin/GamePlay/Tournament");
        }




        private SelectList GetTeamOptions()
        {
            List<TeamOptionViewModel> TeamOptions = _teamRepository.GetTeams()                                
                .Select(t => new TeamOptionViewModel
                {
                    Value = t.TeamID,
                    Text = GamePlayUtilities.CreateTeamName(t.Player1UserName, t.Player2UserName)
                }).ToList();            

            return new SelectList(TeamOptions, "Value", "Text");
        }
    }
}
