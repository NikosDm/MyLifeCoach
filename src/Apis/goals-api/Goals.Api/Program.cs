using Goals.Api.DataPersistence.Extensions;
using Goals.Api.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Goals.Api.Extensions;
using Libraries.Api.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Libraries.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiDocumentation()
    .AddHttpUserContext()
    .AddIdentityAuthentication(builder.Configuration)
    .AddApiServices(builder.Configuration)
    .AddCore()
    .AddDataPersistence(builder.Configuration)
    .AddCors();

var app = builder.Build();

app.UseCors(opt => opt
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithOrigins(builder.Configuration.GetValue<string>("MyLifeCoachWebClient:BaseUrl")));

app.UseSwagger();
app.UseSwaggerUI();

app.UseMigration();

app.UseExceptionHandler(o => { });

app.UseAuthentication();

app.UseMiddleware<UserContextMiddleware>();

app.UseAuthorization();

app.UseApiEndpoints();

app.Run();
