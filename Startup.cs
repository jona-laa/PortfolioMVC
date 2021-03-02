using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using PortfolioMVC.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PortfolioMVC.Models;

namespace PortfolioMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<DateFormat>();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddDefaultUI() 
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            // services.AddAuthorization(options => {  
            //     options.AddPolicy("readpolicy",  
            //         builder => builder.RequireRole("Admin", "Manager", "User"));  
            //     options.AddPolicy("writepolicy",  
            //         builder => builder.RequireRole("Admin", "Manager"));  
            // }); 
        }

        // private RoleManager<IdentityRole> _roleManager;
        // private UserManager<IdentityUser> _userManager;

        // private async Task createAdminWithRole(IServiceProvider serviceProvider)
        // {
        //     _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //     _userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        //     var roleExist = await _roleManager.RoleExistsAsync("Admin");
        //     var userExist = await _userManager.FindByEmailAsync("admin@portfolio.com");

        //     if (!roleExist || userExist == null)
        //     {
        //         if(!roleExist)
        //         {
        //             var role = new IdentityRole();
        //             role.Name = "Admin";
        //             await _roleManager.CreateAsync(role);
        //         }

        //         if(userExist == null)
        //         {
        //             var user = new IdentityUser();
        //             user.UserName = "admin@portfolio.com";

        //             string userPass = "k1ll4kaz0O_";

        //             IdentityResult checkUser = await _userManager.CreateAsync(user, userPass);
        //             if(checkUser.Succeeded)
        //             {
        //                 var result = await _userManager.AddToRoleAsync(user, "Admin");
        //             }
        //         }
        //     }
        // }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
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
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            // hhrhrhr

            // createAdminWithRole(serviceProvider).Wait();
        }
    }
}
