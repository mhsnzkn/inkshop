using Business.Abstract;
using Business.Concrete;
using Data;
using Data.Automapper;
using Data.Entities;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web
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
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
            services.AddControllersWithViews();

            services.AddAutoMapper(typeof(AppProfile).Assembly);

            #region Services
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IOrderDal, OrderDal>();
            services.AddScoped<IOrderTypeManager, OrderTypeManager>();
            services.AddScoped<IOrderTypeDal, OrderTypeDal>();
            services.AddScoped<IOfficeManager, OfficeManager>();
            services.AddScoped<IOfficeDal, OfficeDal>();
            services.AddScoped<ICountryManager, CountryManager>();
            services.AddScoped<ICountryDal, CountryDal>();
            services.AddScoped<ICurrencyManager, CurrencyManager>();
            services.AddScoped<ICurrencyDal, CurrencyDal>();
            services.AddScoped<IPersonnelManager, PersonnelManager>();
            services.AddScoped<IPersonnelDal, PersonnelDal>();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            //    app.UseMigrationsEndPoint();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
        //}
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
        }
    }
}
