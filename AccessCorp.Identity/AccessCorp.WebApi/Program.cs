using Microsoft.AspNetCore.Authorization;
using OnFunction.Application.Interfaces;
using OnFunction.Application.Services;
using OnFunction.WebApi.Configuration;
using OnFunction.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiConfiguration()
    .AddIdentityConfiguration()
    .AddSwaggerConfiguration();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserClaimsService, UserClaimsService>();

builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

var app = builder.Build();

app.UseApiConfiguration();