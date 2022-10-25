using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Webservice;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<Webservice.Query>();
var connectionString = builder.Configuration.GetConnectionString("BoardgamesContextConnection");
builder.Services.AddPooledDbContextFactory<BoardgamesContext>(options => options.UseSqlServer(connectionString)
    .EnableSensitiveDataLogging()).AddLogging(Console.WriteLine);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddProjections()
    .AddFiltering().AddSorting();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL("/graphql");

app.Run();

