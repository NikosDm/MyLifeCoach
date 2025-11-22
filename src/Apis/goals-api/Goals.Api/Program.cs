using Goals.Api.DataPersistence.Extensions;
using Goals.Api.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Goals.Api.Extensions;
using Microsoft.Extensions.Configuration;
using Libraries.Api.Extensions;
using Libraries.Api.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiDocumentation()
    .AddHttpUserContext()
    .AddIdentityAuthentication(builder.Configuration)
    .AddDataPersistence(builder.Configuration)
    .AddCore();

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
