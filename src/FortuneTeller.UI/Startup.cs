using FortuneTeller.UI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.CircuitBreaker.Hystrix;
using Steeltoe.CloudFoundry.Connector.Redis;
using Steeltoe.Common.Http.Discovery;
using Steeltoe.Discovery.Client;
using Steeltoe.Management.Endpoint.Health;
using Steeltoe.Management.Endpoint.Info;
using Steeltoe.Management.Endpoint.Loggers;
using Steeltoe.Management.Endpoint.Trace;
using Steeltoe.Management.Exporter.Tracing;
using Steeltoe.Management.Tracing;
using Steeltoe.Security.Authentication.CloudFoundry;

namespace FortuneTeller.UI
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddTransient<DiscoveryHttpMessageHandler>();
            services
                .AddAuthentication((options) =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CloudFoundryDefaults.AuthenticationScheme;
                })
                .AddCookie((options) =>
                {
                    options.AccessDeniedPath = new PathString("/Error");
                })
                .AddCloudFoundryOAuth(Configuration);
            services.AddAuthorization(options =>
            {
                options.AddPolicy("fortunes.read", policy => policy.RequireClaim("scope", "fortunes.read"));
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDiscoveryClient(Configuration);
            services.Configure<FortuneServiceOptions>(Configuration.GetSection("fortuneService"));
            services.AddScoped<IFortuneService, FortuneServiceClient>();
            services.AddHttpClient<IFortuneService, FortuneServiceClient>()
                .AddHttpMessageHandler<DiscoveryHttpMessageHandler>();

            services.AddDistributedMemoryCache();
            services.AddHystrixCommand<FortuneServiceCommand>("FortuneService", Configuration);
            services.AddHystrixMetricsStream(Configuration);

            services.AddDistributedRedisCache(Configuration);
            services.AddSession();
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
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseCookiePolicy();
            app.UseDiscoveryClient();
            app.UseHystrixRequestContext();
            app.UseHystrixMetricsStream();
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
