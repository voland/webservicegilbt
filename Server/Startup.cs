using System;
/* using System.Collections.Generic; */
/* using System.Linq; */
/* using System.Threading.Tasks; */
using Microsoft.AspNetCore.Builder;
/* using Microsoft.AspNetCore.Components; */
using Microsoft.AspNetCore.Hosting;
/* using Microsoft.AspNetCore.HttpsPolicy; */
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebServiceGilBT.Data;
/* using WebServiceGilBT.Shared; */
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.SessionStorage;
/* using Microsoft.JSInterop; */
using WebServiceGilBT.Services;
/* using System.Configuration; */

namespace WebServiceGilBT {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }



        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddBlazoredSessionStorage();
            services.AddTransient<SqlDataAccess>();
            services.AddTransient<ScreenListMySQLService>();
            services.AddTransient<UserMySQLService>();
			services.AddTransient<GminaMySqlService>();
			/* WebServiceGilBT.Controller.ScreensController.CopyDataToDb(); */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            /* app.UseHttpsRedirection(); */
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
