using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BlazorApp_ClientPortal.Areas.Identity;
using BlazorApp_ClientPortal.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace BlazorApp_ClientPortal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient(); // register IHttpClientFactory for make an api call from server blazor app.

            services.AddRazorPages();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add default authentication scheme.
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie();

            //services.AddAuthentication("Identity.Application")
            //    .AddCookie();

            services.AddAuthentication(options =>
                                    {
                                        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                                        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                                    }
                                      )
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddOpenIdConnect(options =>
                    {
                        options.Authority = "https://localhost:44333";
                        //options.ClientId = "bethanyspieshophr";
                        options.ClientId = "BlazorApp_ClientPortal";

                        options.ClientSecret = "108B7B4F-BEFC-4DD2-82E1-7F025F0F75D0";
                        options.ResponseType = "code id_token";

                        options.Scope.Add("openid");
                        options.Scope.Add("email");
                        options.Scope.Add("profile");

                        //options.CallbackPath = ...

                        options.SaveTokens = true;
                        options.GetClaimsFromUserInfoEndpoint = true;
                    })
                    ;

            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //Add authentication and authorization pipeline.
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
