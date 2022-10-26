using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Core.DomainServices;
using portal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Core.DomainServices.IService;
using Core.DomainServices.Service;
using Core.DomainServices.Validator;
using Core.DomainServices.IValidator;

var builder = WebApplication.CreateBuilder(args);
var securityString = builder.Configuration.GetConnectionString("IdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityContextConnection' not found.");
var boardgamesString = builder.Configuration.GetConnectionString("BoardgamesContextConnection") ?? throw new InvalidOperationException("Connection string 'BoardgamesContextConnection' not found");

builder.Services.AddDbContext<Infrastructure.SecurityContext>(options =>
    options.UseSqlServer(securityString, options => options.EnableRetryOnFailure()));

builder.Services.AddDbContext<BoardgamesContext>(options =>
    options.UseSqlServer(boardgamesString, options => options.EnableRetryOnFailure()));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<Infrastructure.SecurityContext>();


// Add services to the container.
builder.Services.AddControllersWithViews();

// repos oldschool
builder.Services.AddScoped<IGameNightRepository, GameNightRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IGameNightGameRepository, GameNightGameRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IGameNightPlayerRepository, GameNightPlayerRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IPersonReviewRepository, PersonReviewRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// services hip en modern
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IGameNightService, GameNightService>();
builder.Services.AddScoped<IGameNightValidator, GameNightValidator>();
builder.Services.AddScoped<IPersonValidator, UserValidator>();

builder.Services.AddSession();

builder.Services.AddMemoryCache().AddSession(options =>
{
    options.Cookie.IsEssential = true;
    options.Cookie.Name = "LoggedInObject";
    options.Cookie.HttpOnly = true;
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
});

builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 5;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopRight;
});

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
app.UseAuthentication();

app.UseSession();
app.UseAuthorization();

app.UseNotyf();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=GameNight}/{action=Index}");

app.Run();
