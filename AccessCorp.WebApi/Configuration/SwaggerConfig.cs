using Microsoft.OpenApi.Models;

namespace OnFunction.WebApi.Configuration;

public static class SwaggerConfig
{
    public static WebApplicationBuilder AddSwaggerConfiguration(this WebApplicationBuilder builder)
    {
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
        
        return builder;
    }

    public static WebApplication UseSwaggerConfiguration(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        
        return app;
    }
}