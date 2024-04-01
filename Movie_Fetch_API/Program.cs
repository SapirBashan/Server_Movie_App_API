//program
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Movie_Fetch_API.Controllers;
using Movie_Fetch_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DataSetSettings>(
    builder.Configuration.GetSection(nameof(DataSetSettings)));

builder.Services.AddSingleton<IDataSetSettings>(
    sp => sp.GetRequiredService<IOptions<DataSetSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(
    s => new MongoClient(builder.Configuration.GetValue<string>("DataSetSettings:ConnectionString")));

builder.Services.AddScoped<IMovieService, MovieService>();

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
                                                                                                  