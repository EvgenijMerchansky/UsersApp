using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsersApp.EF.Context;
using UsersApp.WebApi.Configurations;

namespace UsersApp.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConnectionConfig connectionConfig = new ConnectionConfig();
            Configuration.Bind("ConnectionStrings", connectionConfig);
            services.AddSingleton(connectionConfig);

            services.AddDbContext<UsersContext>
            (options => options.UseSqlServer(connectionConfig.DefaultConnection,
                builder => builder.MigrationsAssembly("UsersApp.EF")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            /*if (env.IsDevelopment())
            {*/
            InitializeMigration(app);

            //app.UseDeveloperExceptionPage();

            app.UseCors("AllowAllOrigins");
            /*}
            else
            {*/
            //app.UseHsts();
            //}

            //app.UseHttpsRedirection();
            app.UseMvc();
        }

        private static void InitializeMigration(IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (UsersContext context = serviceScope.ServiceProvider.GetService<UsersContext>())
                {
                    context.Database.Migrate();
                    context.Database.EnsureCreated();
                }
            }
        }
    }
}
