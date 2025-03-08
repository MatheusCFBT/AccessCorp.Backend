using AccessCorp.WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiConfiguration()
    .AddIdentityConfiguration()
    .AddSwaggerConfiguration()
    .AddDependencyInjectionConfiguration();

var app = builder.Build();

app.UseApiConfiguration();