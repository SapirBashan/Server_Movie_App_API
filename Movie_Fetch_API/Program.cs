using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Movie_Fetch_API.Controllers;
using Movie_Fetch_API.Services;
using System;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DataSetSettings>(
    builder.Configuration.GetSection(nameof(DataSetSettings)));

builder.Services.AddSingleton<IDataSetSettings>(
    sp => sp.GetRequiredService<IOptions<DataSetSettings>>().Value);

// Log the retrieved configuration
var config = builder.Configuration.GetSection(nameof(DataSetSettings));
var movieCollectName = config.GetValue<string>("MovieCollectName");
var connectionString = config.GetValue<string>("ConnectionStrings");
var dataBaseName = config.GetValue<string>("DataBaseName");

Console.WriteLine($"Retrieved configuration:");
Console.WriteLine($"MovieCollectName: {movieCollectName}");
Console.WriteLine($"ConnectionString: {connectionString}");
Console.WriteLine($"DataBaseName: {dataBaseName}");

if (string.IsNullOrEmpty(connectionString))
{
    throw new ArgumentNullException(nameof(connectionString), "Connection string is missing or null.");
}

builder.Services.AddSingleton<IMongoClient>(
    s => new MongoClient(connectionString));

builder.Services.AddScoped<IMovieService, MovieService>();

builder.Services.AddControllers();
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
