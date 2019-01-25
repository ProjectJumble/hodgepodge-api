using Hodgepodge.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSwag.AspNetCore;
using System;

namespace Hodgepodge.Api
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
            services.AddCors();
            services.AddCosmosDb(o =>
            {
                // TODO: Bind configuration object.
                o.ServiceEndpoint = Configuration["CosmosDb:ServiceEndpoint"];
                o.AuthKey = Configuration["CosmosDb:AuthKey"];
                o.ProvisionThroughputForCosmosDbDatabase =
                    bool.Parse(Configuration["CosmosDb:RequestOptions:ProvisionThroughputForCosmosDbDatabase"]);
            });
            services.AddHttpClient<FakeboxService>(c => c
                .BaseAddress = new Uri(Configuration["Fakebox:ServiceEndpoint"]));
            services.AddMemoryCache();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddRepositories();
            services.AddServices();
            services.AddSingleton(Configuration);
            services.AddSwaggerDocument();
            services.AddUtilities();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Information);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUi3();
            }

            app.UseCors(b => b
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());
            app.UseMvc();
        }
    }
}
