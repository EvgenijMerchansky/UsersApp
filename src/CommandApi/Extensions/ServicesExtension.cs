using Users.Example.DBLayer.Repositories;
using Users.Example.DBLayer.Repositories.Interfaces;
using Users.Example.Services.Services;

namespace Users.Example.CommandApi.Site.Extensions;

public static class ServicesExtension
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IMockUserService, MockUserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
    }
}