using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Wiedza.Api.Configs;
using Wiedza.Api.Configs.ConfigureOptions;
using Wiedza.Api.Core;
using Wiedza.Api.Data;
using Wiedza.Api.Repositories;
using Wiedza.Api.Repositories.Implementations;
using Wiedza.Api.Services;
using Wiedza.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // Указываем источник (ваш фронтенд)
                .AllowAnyHeader() // Разрешаем все заголовки
                .AllowAnyMethod(); // Разрешаем любые HTTP методы
        });
});


builder.Configuration.AddJsonFile("appsettings.secret.json", true);
builder.Services.AddProblemDetails();

builder.Services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtOptions>();

builder.Services.AddSingleton<DatabaseConfiguration>();
builder.Services.AddSingleton<JwtConfiguration>();
builder.Services.AddSingleton<RedisConfiguration>();

builder.Services.AddScoped<IUserRepository, DbUserRepository>();
builder.Services.AddScoped<IPersonRepository, DbPersonRepository>();

builder.Services.AddScoped<ITokenRepository, RedisTokenRepository>();
builder.Services.AddScoped<IUserSaltRepository, DbUserSaltRepository>();

builder.Services.AddScoped<IProjectRepository, DbProjectRepository>();
builder.Services.AddScoped<IServiceRepository, DbServiceRepository>();

builder.Services.AddScoped<IProjectService, DbProjectService>();
builder.Services.AddScoped<IServiceService, DbServiceService>();


builder.Services.AddScoped<IAuthService, DbAuthService>();
builder.Services.AddScoped<IProfileService, DbProfileService>();

builder.Services.AddScoped<IOfferService, DbOfferService>();
builder.Services.AddScoped<IOfferRepository, DbOfferRepository>();

builder.Services.AddScoped<IPublicationService, DbPublicationService>();
builder.Services.AddScoped<IPublicationRepository, DbPublicationRepository>();

builder.Services.AddScoped<IComplaintService, DbComplaintService>();
builder.Services.AddScoped<IComplaintRepository, DbComplaintRepository>();

builder.Services.AddScoped<IWithdrawService, DbWithdrawService>();
builder.Services.AddScoped<IWithdrawRepository, DbWithdrawRepository>();

builder.Services.AddScoped<IVerificationService, DbVerificationService>();
builder.Services.AddScoped<IVerificationRepository, DbVerificationRepository>();

builder.Services.AddScoped<IReviewService, DbReviewService>();
builder.Services.AddScoped<IReviewRepository, DbReviewRepository>();

builder.Services.AddSingleton<ExceptionHandlerService>();

builder.Services.AddScoped<DbUnitOfWork>();

builder.Services.AddDbContext<DataContext>((provider, optionsBuilder) =>
{
    var configuration = provider.GetRequiredService<DatabaseConfiguration>();
    optionsBuilder
        .UseSqlServer(configuration.ConnectionString)
        .UseSnakeCaseNamingConvention();
});

builder.Services.AddScoped<ConnectionMultiplexer>(provider =>
{
    var redisConfiguration = provider.GetRequiredService<RedisConfiguration>();
    return ConnectionMultiplexer.Connect(redisConfiguration.ConfigurationOptions);
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/app/data/keys"))
    .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
    {
        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Policies.PersonPolicy, policyBuilder => policyBuilder.RequireRole(Roles.PersonRole));
    options.AddPolicy(Policies.AdminPolicy, policyBuilder => policyBuilder.RequireRole(Roles.AdministratorRole));
    options.AddPolicy(Policies.AdminAndPersonPolicy,
        policyBuilder => policyBuilder.RequireRole(Roles.AdministratorRole, Roles.PersonRole));
});

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

app.UseCors("AllowLocalhost");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler(errApp =>
{
    var handlerService = errApp.ApplicationServices.GetRequiredService<ExceptionHandlerService>();
    errApp.Run(async context =>
    {
        var feature = context.Features.GetRequiredFeature<IExceptionHandlerFeature>();
        var result = handlerService.HandleException(feature.Error, context);
        await result.ExecuteResultAsync(new ActionContext(context, context.GetRouteData(),
            new ControllerActionDescriptor()));
    });
});

app.Run();