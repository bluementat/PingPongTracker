using System.ComponentModel.DataAnnotations;

namespace PingPongTracker.Models;

public class Season
{
    [Key]
    public Guid SeasonId { get; set; }
    [Required]
    public string SeasonName { get; set; } = string.Empty;
    [Required]
    public DateTime SeasonStart { get; set; }        
    public bool Active { get; set; } = false;
    
    public List<Tournament> Tournaments { get; set; } = new();   
}
