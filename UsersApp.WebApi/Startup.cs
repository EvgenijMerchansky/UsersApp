using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using UsersApp.BLL.Configurations;
using UsersApp.BLL.Contracts;
using UsersApp.BLL.Services;
using UsersApp.BLL.Validation;
using UsersApp.DAL;
using UsersApp.DAL.EF.Context;
using UsersApp.DAL.EF.Repositories;
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
                x.SwaggerDoc(
                    "v1",
#pragma warning disable SA1118 // multiple lines
                    new Info
                    {
                        Version = "v1",
                        Title = "UsersApp public API",
                        Description = "Playground - UsersApp(Web.Api)",
                        TermsOfService = "None",
                        Contact = new Contact
                        {
                            Name = "Eugene Merchansky",
                            Email = "ymerc@softserveinc.com",
                            Url = "https://github.com/EvgenijMerchansky/UsersApp"
                        },
                        License = new License
                        {
                            Name = "MIT License",
                            Url = "https://github.com/EvgenijMerchansky/UsersApp/blob/master/LICENSE"
                        }
                    });
#pragma warning restore SA1118 // end

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);

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
                builder => builder.MigrationsAssembly("UsersApp.DAL.EF")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            InitializeMigration(app);

            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "UsersApp API V1.");
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
