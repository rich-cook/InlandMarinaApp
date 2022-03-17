using Microsoft.AspNetCore.Authentication.Cookies; //cookies authentication
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InlandMarinaMVC
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
            AddCookie(opt => opt.LoginPath = "/Account/Login"); //login page
            //Account controller login method
            services.AddSession(); //needed to use session
            services.AddControllersWithViews();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                //derfault HSTS value is 30 days.  see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //add comments
            app.UseHttpsRedirection();
            app.UseStatusCodePages(); //more user friendly pages for 403, 404 errors
            app.UseStaticFiles();

            app.UseRouting();
            //authentication, which needs to be before the authorization
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");//might want to change this depending on what we want to do
            });
        }
    }
}
