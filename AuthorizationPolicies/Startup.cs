using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AuthorizationPolicies
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IWeatherProvider, WeatherProvider>();

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(configure =>
                    {
                        configure.LoginPath = new PathString("/Account/Login/");
                        configure.AccessDeniedPath = new PathString("/Account/Forbidden/");
                    });

            services.AddAuthorization(configure =>
            {
                configure.AddPolicy(
                    AuthorizationPolicies.Inside,
                    policy =>
                    policy.Requirements.Add(new LocationRequirement { Location = Location.Inside }));
                configure.AddPolicy(
                    AuthorizationPolicies.Outside,
                    policy =>
                    policy.Requirements.Add(new LocationRequirement { Location = Location.Outside }));
            });

            services.AddTransient<IAuthorizationHandler, LocationAuthorizationHandler>();
            services.AddTransient<IAuthorizationHandler, LocationAuthorizationForGingersHandler>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                     name: "default",
                     template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
