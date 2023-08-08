using System.ComponentModel.DataAnnotations;

namespace PingPongTracker.Models;

public class Player
{
    [Key]
    public Guid PlayerId { get; set; }
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;    
    public bool Eligible { get; set; } = true; 
    public bool Active { get; set; } = true;
}
