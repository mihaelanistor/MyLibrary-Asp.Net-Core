using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyLibrary.Repositories;

namespace MyLibrary
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            //var connection = Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<AppContext>(options => options.UseSqlServer(connection));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<BookRepository>();

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));




            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddEntityFrameworkStores<AppContext>();



            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                //googleOptions.ClientId = Configuration["Authentication:Google:820424689484-17q5mb93hnb83tr4jdq738rs2pr1p65m.apps.googleusercontent.com"];
                //googleOptions.ClientSecret = Configuration["Authentication:Google:tlPhIp5SCMNfsoT3oXkUkaR-"];

                googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "get_all_books",
                    template: "Books/{action}",
                    
                    defaults: new { controller = "Book", action = "GetAll" });
                routes.MapRoute(
                    name: "get_by_author_id",
                    template: "Books/{action}/{id}",
                    defaults: new { controller = "Book", action = "GetByAuthorId" });
                routes.MapRoute(
                    name: "get_id",
                    template: "Books/{action}/{id}",
                    defaults: new { controller = "Book", action = "GetById" });
                routes.MapRoute(
                    name: "search_books",
                    template: "Books/{action}/{searchedString?}",
                    defaults: new { controller = "Book", action = "Search" });
                routes.MapRoute(
                    name: "post_book",
                    template: "Books/{action}",
                    defaults: new { controller = "Book", action = "Create", method="POST" });
                routes.MapRoute(
                    name: "edit_book",
                    template: "Books/{action}/{id}",
                    defaults: new { controller = "Book", action = "Edit", method = "POST" });
                routes.MapRoute(
                    name: "delete_book",
                    template: "Books/{action}/{id}",
                    defaults: new { controller = "Book", action = "Delete", method = "POST" });

                routes.MapRoute(
                    name: "user_id",
                    template: "Books/{action}",
                    defaults: new { controller = "Book", action = "IsLoggedIn", method = "GET" });
                routes.MapRoute(
                    name: "get_all_authors",
                    template: "Authors/{action}",
                    defaults: new { controller = "Author", action = "GetAll" });


            });
        }
    }
}
