using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Hangfire;
using IPT.Data;
using IPT.Data.Entity;
using IPT.Infrastructure.Interface;
using IPT.Infrastructure.Middlewares;
using IPT.Infrastructure.Models;
using IPT.Infrastructure.Services;
using IPT.Repository.AutofacModule;
using IPT.Web.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IPT.Web
{
    public class Startup
    {
    

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }
        public IHostingEnvironment HostingEnvironment { get; }
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddDbContext<IPTContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("IPT")));

            // for hangfire
            services.AddHangfire(config =>
            config.UseSqlServerStorage(Configuration.GetConnectionString("IPT")));

            services.AddDefaultIdentity<User>()
                .AddRoles<Role>()
                .AddUserManager<UserManager<User>>()
                .AddRoleManager<RoleManager<Role>>()
                .AddSignInManager<SignInManager<User>>()
                .AddEntityFrameworkStores<IPTContext>();


            services.AddAutoMapper(typeof(Startup).Assembly);

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            //services.AddSingleton<ISMTPService, SMTPService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



                  // inrastructure service
            services.AddTransient<IViewRenderService, ViewRenderService>();
            services.AddSingleton<ISMTPService, SMTPService>();
            services.AddScoped<IBackgroundService, BackgroundService>();


            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddCookie();

            var builder = new ContainerBuilder();
            builder.Populate(services);

            // builder.RegisterType<EmailSettings>().AsSelf().SingleInstance();
            builder.RegisterModule<AutofacRepoModule>();

            ApplicationContainer = builder.Build();
            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(ApplicationContainer);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime, IServiceProvider services)
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
            app.UseCookiePolicy();


            var appSettingsSection = Configuration.GetSection("AppSettings:BackgroundJobDisplay").Value;
            if (appSettingsSection == "TRUE")
            {
                app.UseHangfireDashboard("/BackGroundService", new DashboardOptions
                {
                    Authorization = new[] { new HangFireAuthorizationFilter() }
                });
            }

            app.UseHangfireServer();
            app.UseMiddleware(typeof(HangFireMiddleWare));
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseCookiePolicy();
            CreateUserRoles(services).Wait();
            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }
        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var user = new User() { Email = "admin@gmail.com", UserName = "admin" };
            var result = await UserManager.CreateAsync(user, "Pa$$word123");

            //var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            //if (!roleCheck)
            //{
            //    //create the roles and seed them to the database  
            //   await RoleManager.CreateAsync(new Role("Admin"));
            //}
            // await UserManager.AddToRoleAsync(user, "Admin");

        }
    }
}
