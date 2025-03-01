using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OnFunction.WebApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AccessCorpDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AccessCorpDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s => 
    {
        s.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "AccessCorp API",
            Description = "Esta API realiza a autenticação e ações do banco de dados",
            Contact = new OpenApiContact() {Name = "Matheus Caldana", Email = "matheusterzini@gmail.com"}
        });
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthentication();

app.MapControllers();

app.Run();
