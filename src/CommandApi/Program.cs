using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Users.Example.CommandApi.Site.Extensions;
using Users.Example.CommandApi.Site.Validators;
using Users.Example.CommandService.CommandHandlers;
using Users.Example.DBLayer.EntityFramework;
using Users.Example.QueryService.QueryHandlers;
using Users.Example.Utilities.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MapperProfile).Assembly));
builder.Services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.AddServices();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetUserQueryHandler).Assembly, typeof(CreateUserCommandHadnler).Assembly));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(UserDtoValidator));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
    await context.Database.EnsureCreatedAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
