using Contracts;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;
using Repository;
using SQL_SanitizeWords_WebApi.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IWord, WordRepository>();
var connectionString = builder.Configuration.GetConnectionString("WordsCS");
builder.Services.AddDbContext<WordContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
