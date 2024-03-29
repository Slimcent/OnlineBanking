using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OnlineBanking.Domain.Entities;
using WebUI.domain.Middlewares;
using OnlineBanking.Domain.UnitOfWork;
using OnlineBanking.Domain.Interfaces.Repositories;
using OnlineBanking.Domain.Repositories;
using WebUI.domain.Interfaces.Services;

using WebUI.domain.Services;
using Microsoft.AspNetCore.Identity;
using WebUI.domain.Data;
using OnlineBanking.Domain.Interfaces;

namespace WebUI.domain
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get;}

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDBConnection(Configuration);
            services.AddControllersWithViews();

            services.AddScoped<DbContext, AppDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();           
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAddressRepository, AddressRepository>();

            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddSingleton<IDateTime, SystemDateTime>();
                services.AddHttpContextAccessor();
            services.AddMvc();
            
            services.Configure<IdentityOptions>(opts => {
                opts.User.RequireUniqueEmail = true;
             
                //opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABC";

                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            });
            
            //services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders(
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
                    name: "default", 
                    pattern: "{controller=home}/{action=index}/{id?}");
            });
            SeedRoles.EnsurePopulated(app);
            SeedAdmin.EnsurePopulated(app);
        }
    }
}
