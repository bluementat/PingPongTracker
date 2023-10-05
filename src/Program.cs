using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PingPongTracker.Areas.Identity.Data;
using PingPongTracker.Data;
using PingPongTracker.Data.Interfaces;
using PingPongTracker.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PingPongdbContextConnection") ?? throw new InvalidOperationException("Connection string 'PingPongdbContextConnection' not found.");

builder.Services.AddDbContext<PingPongdbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<PingPongdbContext>();

builder.Services.AddTransient<IPlayerRepository>(provider => new PlayerRepository(provider.GetRequiredService<ApplicationDbContext>()));
builder.Services.AddTransient<ISeasonRepository>(provider => new SeasonRepository(provider.GetRequiredService<ApplicationDbContext>()));
builder.Services.AddTransient<IGameRepository>(provider => new GameRepository(provider.GetRequiredService<ApplicationDbContext>()));
builder.Services.AddTransient<ITeamRepository>(provider => new TeamRepository(provider.GetRequiredService<ApplicationDbContext>()));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<DataSeeder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

await CheckDatabase();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();


async Task CheckDatabase()
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    try
    {
        var IdentityContext = services.GetRequiredService<PingPongdbContext>();
        await IdentityContext.Database.MigrateAsync();
        var AppContext = services.GetRequiredService<ApplicationDbContext>();
        await AppContext.Database.MigrateAsync();
        var seeder = services.GetRequiredService<DataSeeder>();
        await seeder.SeedData();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}
