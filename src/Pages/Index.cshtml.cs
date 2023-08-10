using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PingPongTracker.Data;

namespace PingPongTracker.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public string Message { get; set; } = string.Empty;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        // Randomly select a greeting
        var greetings = Greetings.GetGreetings().ToList();
        var greeting = greetings[new Random().Next(0, greetings.Count)];
        Message = greeting.Text;
    }
}
