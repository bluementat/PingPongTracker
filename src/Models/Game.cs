using System.ComponentModel.DataAnnotations;

namespace PingPongTracker.Models;

public class Game
{
    [Key]
    public Guid GameId { get; set; }
    [Required]
    public Guid Team1Player1Id { get; set; }
    [Required]
    public Guid Team1Player2Id { get; set; }
    [Required]
    public Guid Team2Player1Id { get; set; }
    [Required]
    public Guid Team2Player2Id { get; set; }
    [Required]
    public int Team1Score { get; set; }
    [Required]
    public int Team2Score { get; set; }
    [Required]
    public DateTime MatchupDate { get; set; } = DateTime.Now;
    [Required]
    public Guid Player1WinnerId { get; set; }
    [Required]
    public Guid Player2WinnerId { get; set; }

    public Tournament Tournament { get; set; } = new();
}
