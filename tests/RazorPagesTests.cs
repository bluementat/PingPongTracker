using Microsoft.EntityFrameworkCore;
using PingPongTracker.Data;
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
    public void OnGetPlayers_ReturnsListOfPlayers()
    {        
        var mockPlayersRepo = new Mock<IPlayerRepository>(MockBehavior.Strict);
        var expectedPlayersList = PlayersList01.GetPlayers();
        mockPlayersRepo.Setup(repo => repo.GetPlayers()).Returns(expectedPlayersList);
        
        var pageModel = new PlayersModel(mockPlayersRepo.Object);

        pageModel.OnGet();

        var actualPlayersList = pageModel.Players;

        Assert.That(actualPlayersList, Is.EqualTo(expectedPlayersList));        
        Assert.Pass();
    }
}
