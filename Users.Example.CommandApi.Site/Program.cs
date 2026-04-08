using FluentValidation;
using Users.Example.CommandApi.Site.Validators;
using Users.Example.DBLayer.Repositories;
using Users.Example.DBLayer.Repositories.Interfaces;
using Users.Example.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// refact
builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(Program).Assembly));


//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IMockUserService, MockUserService>();

//builder.Services.AddScoped<IUserRepository, UserRepository>();

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateOrderAuditRecordsHandler).Assembly));

builder.Services.AddValidatorsFromAssemblyContaining(typeof(UserDtoValidator));

// refact

var app = builder.Build();

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
