using Microsoft.AspNetCore.Authorization;
using AccessCorp.Application.Interfaces;
using AccessCorp.Application.Services;
using AccessCorp.WebApi.Configuration;
using AccessCorp.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiConfiguration()
    .AddIdentityConfiguration()
    .AddSwaggerConfiguration();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserClaimsService, UserClaimsService>();

builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

var app = builder.Build();

app.UseApiConfiguration();