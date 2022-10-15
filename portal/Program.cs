using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Core.DomainServices;
using portal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security;

var builder = WebApplication.CreateBuilder(args);
var securityString = builder.Configuration.GetConnectionString("IdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityContextConnection' not found.");
var boardgamesString = builder.Configuration.GetConnectionString("BoardgamesContextConnection") ?? throw new InvalidOperationException("Connection string 'BoardgamesContextConnection' not found");

builder.Services.AddDbContext<Infrastructure.SecurityContext>(options =>
    options.UseSqlServer(securityString));

builder.Services.AddDbContext<BoardgamesContext>(options =>
    options.UseSqlServer(boardgamesString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<Infrastructure.SecurityContext>();


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
    pattern: "{controller=GameNight}/{action=Index}/{id?}");


app.Run();
