using Goals.Api.DataPersistence.Extensions;
using Goals.Api.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Goals.Api.Extensions;
using Libraries.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiDocumentation()
    .AddHttpUserContext()
    .AddIdentityAuthentication(builder.Configuration)
    .AddApiServices(builder.Configuration)
    .AddCore()
    .AddDataPersistence(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMigration();

app.UseAuthentication();

app.UseAuthorization();

app.UseApiEndpoints();

app.UseApiServices();

app.Run();
