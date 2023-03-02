using ProjectMicroservice.Services;
using ProjectMicroservice.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ProjectMicroservice.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services
    .AddHttpClient<IUserApi, UserApi>(options =>
        options.BaseAddress = builder.Configuration.GetValue<Uri>("UserApiHost"));

var configSection = builder.Configuration.GetSection("MongoDb");
var client = new MongoClient(configSection.GetValue<string>("ConnectionUri"));
var database = client.GetDatabase(configSection.GetValue<string>("DatabaseName"));

var projectsCollection = database.GetCollection<Project>("projects");
builder.Services.AddSingleton(projectsCollection);

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IPopularIndicatorService, PopularIndicatorService>();
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