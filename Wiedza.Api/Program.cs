using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Configs;
using Wiedza.Api.Data;
using Wiedza.Api.Repositories;
using Wiedza.Api.Repositories.Implemetations;
using Wiedza.Api.Services;
using Wiedza.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.secret.json", optional: true);

builder.Services.AddSingleton<DatabaseConfiguration>();
builder.Services.AddSingleton<JwtConfiguration>();

builder.Services.AddScoped<IAuthRepository, DbAuthRepository>();
builder.Services.AddScoped<IAuthService, DbAuthService>();

builder.Services.AddDbContext<DataContext>((provider, optionsBuilder) =>
{
    var configuration = provider.GetRequiredService<DatabaseConfiguration>();
    optionsBuilder
        .UseSqlServer(configuration.ConnectionString)
        .UseSnakeCaseNamingConvention();
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer();

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