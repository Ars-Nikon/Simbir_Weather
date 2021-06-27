using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Simbirsoft_Weather.Models;
using Simbirsoft_Weather.Services;

namespace Simbirsoft_Weather
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
            services.AddDbContext<IdentityContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("Connection")));

            services.AddDbContext<CityContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("Connection")));

            services.AddDbContext<EventContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("Connection")));

            services.AddDbContext<ClothesContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("Connection")));
            services.AddScoped<IClothesRepository, ClothesRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IClothingConsultant, ClothingConsultant>();
            services.Configure<SmtpClientConfiguration>(Configuration.GetSection("SmtpClientConfiguration"));
            services.AddScoped<INotificationSender, MailNotificationSender>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificationWritter, NotificationWritter>();

            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 8;   // ìèíèìàëüíàÿ äëèíà
                opts.Password.RequireNonAlphanumeric = false;   // òðåáóþòñÿ ëè íå àëôàâèòíî-öèôðîâûå ñèìâîëû
                opts.Password.RequireLowercase = false; // òðåáóþòñÿ ëè ñèìâîëû â íèæíåì ðåãèñòðå
                opts.Password.RequireUppercase = false; // òðåáóþòñÿ ëè ñèìâîëû â âåðõíåì ðåãèñòðå
                opts.Password.RequireDigit = false; // òðåáóþòñÿ ëè öèôðû
            }).AddEntityFrameworkStores<IdentityContext>();

            services.AddControllersWithViews();

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

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
                endpoints.MapControllers();
            });
        }
    }
}