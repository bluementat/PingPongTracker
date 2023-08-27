namespace PingPongTracker.Data;

public static class PingPongDataStructures
{
    public record Team(Guid Player1Id, string Player1UserName, Guid Player2Id, string Player2UserName, int Score);
}
