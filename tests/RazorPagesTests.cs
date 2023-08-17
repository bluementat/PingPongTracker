using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Moq;
using PingPongTracker.Data;
using PingPongTracker.Models;
using PingPongTracker.Pages.Admin;
using PingPongTracker.Tests.TestData;

namespace PingPongTracker.Tests;

public class RazorPagesTests
{
    [SetUp]
    public void Setup()
    {        

    }

    [Test]
    public void OnGetMainenance_ReturnsListOfMaintenanceOptions()
    {        
        var expectedMaintenanceList = MaintenanceOptions.GetMaintenanceOptions();        
        
        var pageModel = new MaintenanceModel();

        pageModel.OnGet();

        var actualMaintenanceList = pageModel.Options;

        Assert.That(actualMaintenanceList, Is.EqualTo(expectedMaintenanceList));        
        
    }

    [Test]
    public void OnGetPlayers_ReturnsListOfPlayers()
    {        
        var mockPlayersRepo = new Mock<IPlayerRepository>(MockBehavior.Strict);
        var expectedPlayersList = PlayersList01.GetPlayers();
        mockPlayersRepo.Setup(repo => repo.GetPlayers()).Returns(expectedPlayersList);
        
        var pageModel = new PlayersModel(mockPlayersRepo.Object);

        pageModel.OnGet();

        var actualPlayersList = pageModel.Players;

        Assert.That(actualPlayersList, Is.EqualTo(expectedPlayersList));        
        
    }

    [Test]
    public async Task OnPostAddPlayer_ReturnsAPageResult_WhenModelStateIsInvalid()
    {
        var mockPlayersRepo = new Mock<IPlayerRepository>();
        var expectedPlayersList = PlayersList01.GetPlayers();
        mockPlayersRepo.Setup(repo => repo.GetPlayers()).Returns(expectedPlayersList);
        
        var pageModel = new AddPlayerModel(mockPlayersRepo.Object);

        pageModel.ModelState.AddModelError("Name", "Required");

        var actualResult = await pageModel.OnPostAsync();
        //Assert.IsType<PageResult>(actualResult);

        Assert.That(actualResult, Is.InstanceOf<PageResult>());
    }

    [Test]
    public async Task OnPostAddPlayer_ReturnsAPageResult_WhenModelStateIsValid()
    {
        var mockPlayersRepo = new Mock<IPlayerRepository>();
        var expectedPlayersList = PlayersList01.GetPlayers();
        mockPlayersRepo.Setup(repo => repo.GetPlayers()).Returns(expectedPlayersList);
        
        var pageModel = new AddPlayerModel(mockPlayersRepo.Object);

        pageModel.NewPlayer = new Player { FirstName = "John", LastName = "Doe", Eligible = true, Active = true };

        var actualResult = await pageModel.OnPostAsync();
        //Assert.IsType<PageResult>(actualResult);

        Assert.That(actualResult, Is.InstanceOf<RedirectToPageResult>());
    }

    [Test]
    public async Task OnGetEditPlayer_ReturnsAPlayer_WhenPlayerExists()
    {
        var mockPlayersRepo = new Mock<IPlayerRepository>();
        var expectedPlayer = PlayersList01.GetPlayers().First();
        mockPlayersRepo.Setup(repo => repo.GetPlayerById(expectedPlayer.PlayerId)).ReturnsAsync(expectedPlayer);
        
        var pageModel = new EditPlayerModel(mockPlayersRepo.Object);

        await pageModel.OnGet(expectedPlayer.PlayerId);

        var actualPlayer = pageModel.PlayerToEdit;

        Assert.That(actualPlayer, Is.EqualTo(expectedPlayer));
    }

    [Test]
    public async Task OnGetEditPlayer_ReturnsNewPlayer_WhenPlayerDoesNotExist()
    {
        var mockPlayersRepo = new Mock<IPlayerRepository>();
        var expectedPlayer = new Player { PlayerId = Guid.Empty, FirstName = "", LastName = "", Eligible = true, Active = true };
        mockPlayersRepo.Setup(repo => repo.GetPlayerById(It.IsAny<Guid>())).ReturnsAsync(expectedPlayer);
        
        var pageModel = new EditPlayerModel(mockPlayersRepo.Object);

        await pageModel.OnGet(Guid.NewGuid());

        var actualResult = pageModel.PlayerToEdit;

        Assert.That(actualResult.PlayerId, Is.EqualTo(Guid.Empty));
    }
}
