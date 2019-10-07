using FortuneTeller.Service.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Security.Authentication.CloudFoundry;
using Steeltoe.CloudFoundry.Connector.SqlServer.EFCore;
using Steeltoe.Discovery.Client;
using Steeltoe.Management.Endpoint.Info;
using Steeltoe.Management.Endpoint.Loggers;
using Steeltoe.Management.Endpoint.Health;
using Steeltoe.Management.Endpoint.Trace;
using Steeltoe.Management.Tracing;
using Steeltoe.Management.Exporter.Tracing;

namespace FortuneTeller.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCloudFoundryJwtBearer(Configuration);
            services
                .AddAuthorization(options =>
                {
                    options.AddPolicy("fortunes.read", policy => policy.RequireClaim("scope", "fortunes.read"));
                });

            services.AddEntityFrameworkInMemoryDatabase().AddDbContext<FortuneContext>(options => options.UseInMemoryDatabase("Fortunes"));
            //services.AddDbContext<FortuneContext>(options => options.UseSqlServer(Configuration));
           // services.AddEntityFrameworkInMemoryDatabase().AddDbContext<FortuneContext>(options => options.UseInMemoryDatabase("Fortunes"));
            services.AddDbContext<FortuneContext>(options => options.UseSqlServer(Configuration));
            services.AddScoped<IFortuneRepository, FortuneRepository>();
            services.AddDiscoveryClient(Configuration);
            services.AddInfoActuator(Configuration);
            services.AddLoggersActuator(Configuration);
            services.AddHealthActuator(Configuration);
            services.AddTraceActuator(Configuration);

            services.AddDistributedTracing(Configuration);
            services.AddZipkinExporter(Configuration);

            
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDiscoveryClient();
            app.UseAuthentication();
            app.UseInfoActuator();
            app.UseLoggersActuator();
            app.UseHealthActuator();
            app.UseTraceActuator();
            app.UseTracingExporter();
            app.UseMvc();
           
        }
    }
}
