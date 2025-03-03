using OnFunction.WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiConfiguration()
    .AddIdentityConfiguration()
    .AddSwaggerConfiguration();

var app = builder.Build();

app.UseApiConfiguration();