using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Data;
using portal.Areas.Identity.Data;
using Infrastructure;
using Core.DomainServices;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("IdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityContextConnection' not found.");
var boardgamesString = builder.Configuration.GetConnectionString("BoardgamesContextConnection") ?? throw new InvalidOperationException("Connection string 'BoardgamesContextConnection' not found");

builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<BoardgamesContext>(options =>
    options.UseSqlServer(boardgamesString));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<IdentityContext>();


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IGameNightRepository, GameNightRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
