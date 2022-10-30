using Core.DomainServices;
using Core.DomainServices.IService;
using Core.DomainServices.IValidator;
using Core.DomainServices.Service;
using Core.DomainServices.Validator;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Webservice;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGameNightRepository, GameNightRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IGameNightGameRepository, GameNightGameRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IGameNightPlayerRepository, GameNightPlayerRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IPersonReviewRepository, PersonReviewRepository>();

// services hip en modern
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IGameNightService, GameNightService>();
builder.Services.AddScoped<IGameNightValidator, GameNightValidator>();
builder.Services.AddScoped<IPersonValidator, UserValidator>();

var connectionString = builder.Configuration.GetConnectionString("BoardgamesContextConnection");
builder.Services.AddDbContext<BoardgamesContext>(options => options.UseSqlServer(connectionString)
    .EnableSensitiveDataLogging()).AddLogging(Console.WriteLine);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .RegisterService<IGameNightRepository>()
    .AddProjections()
    .AddFiltering().AddSorting();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL("/graphql");

app.Run();

