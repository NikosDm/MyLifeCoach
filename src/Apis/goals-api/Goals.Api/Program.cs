using Goals.Api.DataPersistence.Extensions;
using Goals.Api.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Goals.Api.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using System;
using Libraries.Api.Extensions;
using Libraries.Api.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(static c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Goals API",
        Version = "v1",
        Description = "Minimal API for managing goals and its goal steps."
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Paste ONLY the JWT. Swagger will add 'Bearer ' automatically."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services
    .AddHttpUserContext()
    .AddDataPersistence(builder.Configuration)
    .AddCore()
    .AddIdentityAuthentication(builder.Configuration);

builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("GoalsDB"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMigration();

app.UseAuthentication();

app.UseAuthorization();

app.UseApiEndpoints();

app.UseApiServices();

app.Run();
