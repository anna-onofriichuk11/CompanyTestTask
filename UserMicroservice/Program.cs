using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserMicroservice.Data;
using UserMicroservice.Services;
using UserMicroservice.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services
    .AddDbContextPool<UserContext>(contextBuilder => contextBuilder.UseNpgsql(
        builder.Configuration.GetConnectionString("DatabaseConnection"), options => options.EnableRetryOnFailure()))
    .AddScoped<IUserService, UserService>()
    .AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddAutoMapper(typeof(Mapper));
// Learn more about configuri   ng Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<UserContext>();
    dbContext.Database.EnsureCreated(); 
}

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