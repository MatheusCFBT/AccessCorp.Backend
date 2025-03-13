using AccessCorpUsers.Application.Configuration;
using AccessCorpUsers.Application.Interfaces;
using AccessCorpUsers.Application.Services;
using AccessCorpUsers.Domain.Interfaces;
using AccessCorpUsers.Infra.Context;
using AccessCorpUsers.Infra.Repositories;
using AccessCorpUsers.WebApi.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AccessCorpUsersDbContext>(op =>
{
    op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.AddApiConfiguration()
    .AddSwaggerConfiguration();
builder.Services.AddAutoMapper(typeof(Program));



builder.Services.Configure<IdentityApiSettings>(builder.Configuration.GetSection("IdentityApi"));

builder.Services.AddScoped<IAdministratorRepository, AdministratorRepository>();
builder.Services.AddScoped<IAdministratorService, AdministratorService>();
builder.Services.AddHttpClient<IIdentityApiClient, IdentityApiClient>();

var app = builder.Build();

app.UseApiConfiguration();
