using Ioc.Extension;
using IOC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace ProjectUniversity
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddLogging();

            services.AdicionarSwagger();

            services.AdicionarServicos(connectionString);

            services.ConfigurarAutenticacaoJwt();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });


            services.AddControllers();

            services.AddHealthChecks()
                .AddSqlServer(connectionString)
                .AddCheck<SelfHealthCheck>("Self");

            // Adicionar Health Checks UI com armazenamento em memória
            services.AddHealthChecksUI()
                .AddInMemoryStorage();

            //services.AddHealthChecksUI(options =>
            //{
            //    options.AddHealthCheckEndpoint("Endpoint1", "/healthz");

            //});
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Add Swagger middleware
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectUniversity API v1");
                });
            }

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // Use Health Checks middleware
            app.UseHealthChecks("/status", new HealthCheckOptions
            {
                ResponseWriter = (httpContext, result) =>
                {
                    httpContext.Response.ContentType = "application/json";

                    var json = new JObject(
                        new JProperty("status", result.Status.ToString()),
                        new JProperty("results", new JObject(result.Entries.Select(pair =>
                            new JProperty(pair.Key, new JObject(
                                new JProperty("status", pair.Value.Status.ToString()),
                                new JProperty("description", pair.Value.Description),
                                new JProperty("data", new JObject(pair.Value.Data.Select(
                                    p => new JProperty(p.Key, p.Value))))))))));
                    return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
                }
            });

            app.UseHealthChecksUI(options =>
            {
                options.UIPath = "/status-dashboard";
            });

            app.UseHealthChecks("/status-api", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = HealthChecks.UI.Client.UIResponseWriter.WriteHealthCheckUIResponse,
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
