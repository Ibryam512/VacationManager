using DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repositories;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Services;
using Web.Services.Interfaces;
using MudBlazor.Services;
using Repositories.Mapper;
using AutoMapper;

namespace VacationManager.Web
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
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddMudServices();

            //Repositories
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ILoginRegisterRepository, LoginRegisterRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            services.AddScoped<IVacationRepository, VacationRepository>();
            
            //Services
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IVacationDocumentService, VacationDocumentService>();

            services.AddDbContext<VacationManagerDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("IbryamConnection")));
            MapperConfiguration mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile())); 
            services.AddSingleton(mapperConfig.CreateMapper());
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
