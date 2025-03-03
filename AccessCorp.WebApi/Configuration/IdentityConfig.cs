﻿using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnFunction.Application.Entities;
using OnFunction.WebApi.Data;
using OnFunction.WebApi.Extensions;

namespace OnFunction.WebApi.Configuration;

public static class IdentityConfig
{
    public static WebApplicationBuilder AddIdentityConfiguration(this WebApplicationBuilder builder)
    {
        
        builder.Services.AddDbContext<AccessCorpDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddDefaultIdentity<IdentityUser>()
            .AddErrorDescriber<IdentityPortugueseMessages>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AccessCorpDbContext>()
            .AddDefaultTokenProviders();
        
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy =>
                policy.Requirements.Add(new PermissionRequirement("FullAccess")));

            options.AddPolicy("DoormanPolicy", policy =>
                policy.Requirements.Add(new PermissionRequirement("LimitedAccess")));
        });

        builder.AddJwtConfiguration();
        
        return builder;
    }

    private static WebApplicationBuilder AddJwtConfiguration(this WebApplicationBuilder builder)
    {
        var appSettingsSection = builder.Configuration.GetSection("AppSettings");
        builder.Services.Configure<AppSettings>(appSettingsSection);

        var appSettings = appSettingsSection.Get<AppSettings>();
        var key = Encoding.ASCII.GetBytes(appSettings.Secret);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(bearerOptions =>
        {
            bearerOptions.RequireHttpsMetadata = true;
            bearerOptions.SaveToken = true;
            bearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = appSettings.ValidoEm,
                ValidIssuer = appSettings.Emissor
            };
        });

        return builder;
    }
    
    public static WebApplication UseIdentityConfiguration(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthentication();
        
        return app;
    }
}