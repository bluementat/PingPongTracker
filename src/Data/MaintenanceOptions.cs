namespace PingPongTracker.Data;

public static class MaintenanceOptions
{
    public record MaintenanceOption(int Id, string Name, string Page);

    public static IEnumerable<MaintenanceOption> GetMaintenanceOptions()
    {
        yield return new MaintenanceOption(1, "Player Management", "Players");      
        yield return new MaintenanceOption(2, "Season Management", "Seasons");  
    }
}
