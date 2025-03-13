using System.Reflection;

namespace AccessCorpUsers.WebApi.Configuration;

public static class ApiConfig
{
    public static WebApplicationBuilder AddApiConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables()
            .AddUserSecrets(Assembly.GetExecutingAssembly(), true);
        
        builder.Services.AddControllers();
        
        return builder;
    }

    public static WebApplication UseApiConfiguration(this WebApplication app)
    {
        app.UseSwaggerConfiguration();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting(); 

        app.UseAuthentication(); 
        app.UseAuthorization(); 

        app.MapControllers();

        app.Run();
        return app;
    }
}