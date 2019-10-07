using FortuneTeller.UI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Common.Http.Discovery;
using Steeltoe.Discovery.Client;

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
            services.AddDiscoveryClient(Configuration);
            services.Configure<FortuneServiceOptions>(Configuration.GetSection("fortuneService"));
            services.AddScoped<IFortuneService, FortuneServiceClient>();
            services.AddHttpClient<IFortuneService, FortuneServiceClient>()
                .AddHttpMessageHandler<DiscoveryHttpMessageHandler>();
            

            services.AddDistributedMemoryCache();
            services.AddSession();
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
            app.UseMvc();
        }
    }
}
