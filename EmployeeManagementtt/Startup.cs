using EmployeeManagementtt.data;
using EmployeeManagementtt.interfaces;
using EmployeeManagementtt.Repositries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementtt
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
            services.AddControllersWithViews();
            services.AddScoped<IemployeeRepo, SQLEmployeeRepo>();
            services.AddDbContextPool<AppDBcontext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("EmployeeDbConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDBcontext>();

            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "428828471420-oe2j3j4ngtmt99oeg6jd2d6ds2rud6jq.apps.googleusercontent.com";
                options.ClientSecret = "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";
            })
             .AddFacebook(options =>
             {
                 options.AppId = "394050379527940";
                 options.AppSecret = "71c445dd90b05644675f769f2a6b7e27";
             });

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

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
