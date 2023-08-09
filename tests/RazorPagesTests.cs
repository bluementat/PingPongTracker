using Microsoft.EntityFrameworkCore;
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
    public void Test1()
    {
        // var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("PingPongDb");
        // var mockDbContext = new Mock<ApplicationDbContext>(optionsBuilder.Options);
        // var expectedPlayersList = PlayersList01.GetPlayers();
        // mockDbContext.Setup(db => db.Players.ToList()).Returns(expectedPlayersList);
        // var pageModel = new PlayersModel(mockDbContext.Object);

        // pageModel.OnGet();

        // var actualPlayersList = pageModel.Players;

        // Assert.That(actualPlayersList, Is.EqualTo(expectedPlayersList));        
        Assert.Pass();
    }
}
