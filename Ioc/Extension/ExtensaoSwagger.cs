using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

public static class ExtensaoSwagger
{
    public static void AdicionarSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectUniversity", Version = "v1" });
        });
    }
}