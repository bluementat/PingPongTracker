using PingPongTracker.Models;

namespace PingPongTracker.Tests.TestData;

public static class PlayersList01
{
    public static List<Player> GetPlayers()
    {
        return new List<Player>()
        {
            new Player
            {
                PlayerId = new Guid("00000000-0000-0000-0000-000000000001"),
                FirstName = "John",
                LastName = "Doe",
                Eligible = true,
                Active = true,
            },
            new Player
            {
                PlayerId = new Guid("00000000-0000-0000-0000-000000000002"),
                FirstName = "Jane",
                LastName = "Doe",
                Eligible = true,
                Active = true,
            },
            new Player
            {
                PlayerId = new Guid("00000000-0000-0000-0000-000000000003"),
                FirstName = "John",
                LastName = "Smith",
                Eligible = true,
                Active = true,
            },
            new Player
            {
                PlayerId = new Guid("00000000-0000-0000-0000-000000000004"),
                FirstName = "Jane",
                LastName = "Smith",
                Eligible = true,
                Active = true,
            }
        };
    }
}
