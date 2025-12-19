using Libraries.Api.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Profiles.Api.Core.Extensions;
using Profiles.Api.DataPersistence.Extensions;
using Profiles.Api.Extensions;

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

app.UseAuthentication();

app.UseAuthorization();

app.UseApiEndpoints();

app.UseApiServices();

app.Run();
