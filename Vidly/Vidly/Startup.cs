using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Vidly.Data;
using AutoMapper;
using System.Text.Json;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Vidly
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
            
            //add db connection string registration
            services.AddDbContextPool<MovieRentDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("VidlyDBConnection")));


            // For automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //for identity registration and override PasswordOptions class properties
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {

            })
                .AddEntityFrameworkStores<MovieRentDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();


            services.AddControllersWithViews()
                .AddJsonOptions(options => {
                    // Camel Notation added for Json format value
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

                });
            services.AddRazorPages();

            services.AddMvc(options => {
                // For Global authorize
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            // For external logins
            services.AddAuthentication().AddFacebook(options =>
                {
                    options.AppId = "661391721451012";
                    options.AppSecret = "feaa7f908f0381af45e1f33599cae487";
                });


            // For Glimpse registration
            

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "areas",
                    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });

           
        }
    }
}
