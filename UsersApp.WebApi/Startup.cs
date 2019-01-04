using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using UsersApp.BLL.Configurations;
using UsersApp.BLL.Contracts;
using UsersApp.BLL.Services;
using UsersApp.BLL.Validation;
using UsersApp.DAL;
using UsersApp.EF.Context;
using UsersApp.EF.Repositories;
using UsersApp.WebApi.Configurations;
using UsersApp.WebApi.Middlewares;
using UsersApp.WebApi.Validation;

namespace UsersApp.WebApi
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
            ConnectionConfig connectionConfig = new ConnectionConfig();
            Configuration.Bind("ConnectionStrings", connectionConfig);
            services.AddSingleton(connectionConfig);

            ConfigureDatabase(services, connectionConfig);

            MapperConfiguration config = new MapperConfiguration(c =>
            {
                c.AddProfile<OrganizationProfile>();
            });

            services.AddSingleton(c => config.CreateMapper());

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "UsersApp public API", Version = "v1" });
                x.SchemaFilter<FluentValidationRules>();
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<UpdateUserValidator>();
                    fv.ValidatorFactoryType = typeof(ScopedServiceProviderValidatorFactory);
                });
        }

        public virtual void ConfigureDatabase(IServiceCollection services, ConnectionConfig connectionConfig)
        {
            services.AddDbContext<UsersContext>(
            options => options.UseSqlServer(
                connectionConfig.DefaultConnection,
                builder => builder.MigrationsAssembly("UsersApp.EF")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            InitializeMigration(app);

            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "UsersApp API (v1).");
            });

            app.UseCors("AllowAllOrigins");

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
