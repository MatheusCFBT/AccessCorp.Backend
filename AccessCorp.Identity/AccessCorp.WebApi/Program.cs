using AccessCorp.Application.Entities;
using Microsoft.AspNetCore.Authorization;
using AccessCorp.Application.Interfaces;
using AccessCorp.Application.Services;
using AccessCorp.Domain.Interfaces;
using AccessCorp.Domain.Services;
using AccessCorp.WebApi.Configuration;
using AccessCorp.WebApi.Extensions;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiConfiguration()
    .AddIdentityConfiguration()
    .AddSwaggerConfiguration();

var emailSenderSection = builder.Configuration.GetSection("SendEmail");
builder.Services.Configure<SendEmail>(emailSenderSection);

builder.Services.AddTransient<ISendEmailService, SendEmailService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserClaimsService, UserClaimsService>();
builder.Services.AddScoped<ICepValidationService, CepValidationService>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

var app = builder.Build();

app.UseApiConfiguration();