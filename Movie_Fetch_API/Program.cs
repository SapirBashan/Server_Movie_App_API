using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Movie_Fetch_API.Controllers;
using Movie_Fetch_API.Services;
using System;
using Microsoft.Extensions.Options;
using OpenAI_ChatGPT;

var builder = WebApplication.CreateBuilder(args);

//************************* For Chat GPT *************************************************************
builder.Services.AddHttpClient(); // Adds the IHttpClientFactory and related services to service coll

builder.Services.AddScoped<IChatCompletionService, ChatCompletionService>();
//************************* For Chat GPT *************************************************************

//************************* For MongoDB *************************************************************
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

//this is a way to get the configuration values from appsettings.json file
//for the MongoDB connection
Console.WriteLine($"Retrieved configuration:");
Console.WriteLine($"MovieCollectName: {movieCollectName}");
Console.WriteLine($"ConnectionString: {connectionString}");
Console.WriteLine($"DataBaseName: {dataBaseName}");

//if the connection string is null or empty, throw an exception
if (string.IsNullOrEmpty(connectionString))
{
    throw new ArgumentNullException(nameof(connectionString), "Connection string is missing or null.");
}

builder.Services.AddSingleton<IMongoClient>(
    s => new MongoClient(connectionString));
//************************* For MongoDB *************************************************************

//************************* For OMDB *************************************************************
builder.Services.AddScoped<IMovieService, MovieService>();

//this is service is used to get the configuration values from appsettings.json file
//for the OMDB API
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

//app services are used to get the services that are registered in the container
//and then we can use them in the application
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
