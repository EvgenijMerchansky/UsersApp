# Users.Example

## Overview

Simple .NET Web API application for working with users.

The project is built around a layered architecture with separation of responsibilities between API, command handling, query handling, services, database layer, shared models, and utilities.

It leverages:

- **ASP.NET Core Web API** for HTTP endpoints
- **Entity Framework Core** for database access
- **MediatR** for command/query handling
- **AutoMapper** for object mapping
- **FluentValidation** for request validation
- **SQL Server / LocalDB** for data storage
- **Docker Compose** for containerized local запуск

## Architecture Layers

### 1. API Layer

- **Users.Example.CommandApi.Site**: entry point of the application
- exposes controllers
- configures DI, middleware, Swagger, AutoMapper, MediatR, validators, EF Core

### 2. Command Layer

- **Users.Example.CommandService**
- contains commands and command handlers
- responsible for write operations:
  - create user
  - update user
  - delete user

### 3. Query Layer

- **Users.Example.QueryService**
- contains queries and query handlers
- responsible for read operations:
  - get user by id
  - get users list

### 4. Service Layer

- **Users.Example.Services**
- contains business/service logic
- works with repositories and unit-of-work style flow
- includes:
  - `UserService`
  - `MockUserService`

### 5. Database Layer

- **Users.Example.DBLayer**
- contains:
  - EF Core `UsersDbContext`
  - entities
  - repositories
  - repository interfaces

### 6. Shared Models

- **Users.Example.Models**
- shared DTOs used across layers

### 7. Utilities

- **Users.Example.Utilities**
- contains mapping profiles and supporting utilities

### 8. Tests

- **Users.Example.CommandApi.Test**
- test project for API / command scenarios

## Folder Structure

```
Users.Example/
│
├── doc/
│
├── pipelines/
│
├── infrastructure/
│   │
│   └── docker/
│       ├── docker-compose.yml
│       ├── Dockerfile
│       ├── sqlserver.env.example
│       └── users-api.env.example
│
├── src/
│   │
│   ├── Client/
│   │   └── Users.Example.Client
│   │
│   ├── CommandApi/
│   │   └── Users.Example.CommandApi.Site/
│   │       ├── Controllers/
│   │       │   ├── BaseController.cs
│   │       │   └── UsersController.cs
│   │       ├── Extensions/
│   │       │   └── ServicesExtension.cs
│   │       ├── Validators/
│   │       │   ├── UpdateUserDtoValidator.cs
│   │       │   ├── CreateUserDtoValidator.cs
│   │       │   └── UserDtoValidator.cs
│   │       ├── appsettings.json
│   │       ├── Program.cs
│   │       └── Users.Example.CommandApi.Site.csproj
│   │
│   ├── CommandService/
│   │   └── Users.Example.CommandService/
│   │       ├── CommandHandlers/
│   │       │   ├── CreateUserCommandHadnler.cs
│   │       │   ├── DeleteUserCommandHadnler.cs
│   │       │   └── UpdateUserCommandHandler.cs
│   │       └── Commands/
│   │           ├── CreateUserCommand.cs
│   │           ├── DeleteUserCommand.cs
│   │           └── UpdateUserCommand.cs
│   │
│   ├── QueryService/
│   │   └── Users.Example.QueryService/
│   │       ├── Queries/
│   │       │   ├── GetUserQuery.cs
│   │       │   └── GetUsersQuery.cs
│   │       └── QueryHandlers/
│   │           ├── GetUserQueryHandler.cs
│   │           └── GetUsersQueryHandler.cs
│   │
│   ├── DBLayer/
│   │   └── Users.Example.DBLayer/
│   │       ├── EntityFramework/
│   │       │   └── UsersDbContext.cs
│   │       ├── Models/
│   │       │   └── User.cs
│   │       └── Repositories/
│   │           ├── Interfaces/
│   │           │   ├── IBaseRepository.cs
│   │           │   └── IUserRepository.cs
│   │           ├── BaseRepository.cs
│   │           └── UserRepository.cs
│   │
│   ├── Shared/
│   │   ├── Users.Example.Models/
│   │   │   └── Dtos/
│   │   │       └── Users/
│   │   │           ├── UpdateUserDto.cs
│   │   │           ├── CreateUserDto.cs
│   │   │           └── UserDto.cs
│   │   │
│   │   ├── Users.Example.Services/
│   │   │   └── Services/
│   │   │       ├── MockUserService.cs
│   │   │       └── UserService.cs
│   │   │
│   │   └── Users.Example.Utilities/
│   │       └── Mapper/
│   │           └── MapperProfile.cs
│   │
│   └── Tests/
│       └── Users.Example.CommandApi.Test/
│
├── .gitignore
├── LICENSE
├── README.md
└── Users.Example.All.sln
```

## Design Approach

The project follows a **CQRS-style separation** of responsibilities:

- **Commands** are responsible for data modification
- **Queries** are responsible for data retrieval

This approach helps keep read and write logic isolated and easier to maintain.

The solution is also split into dedicated layers:

- **API layer** for HTTP endpoints and application bootstrap
- **Command layer** for write operations
- **Query layer** for read operations
- **Service layer** for business logic
- **Database layer** for EF Core context, entities, and repositories
- **Shared layer** for DTOs and reusable components
- **Utilities layer** for mapping and helper logic

## Database Strategy

The application uses **Entity Framework Core** with SQL Server.

For local development, the project can work with:

- **LocalDB** for non-Docker execution
- **SQL Server in Docker** for containerized execution

The database connection is configured differently depending on the environment:

- **local development** uses **User Secrets**
- **Docker development** uses local `.env` files

This allows keeping real connection strings out of tracked configuration files.

## Technology Stack

- **Framework**: ASP.NET Core Web API
- **Language**: C#
- **Runtime**: .NET 10
- **ORM**: Entity Framework Core
- **Validation**: FluentValidation
- **Mediator**: MediatR
- **Object Mapping**: AutoMapper
- **Database**: SQL Server
- **Containerization**: Docker, Docker Compose

## Request Flow

1. HTTP request comes to `UsersController`
2. Controller sends a command or query through MediatR
3. Handler calls the service layer
4. Service layer works with repositories
5. Repository uses EF Core to access the database
6. Response is returned back to the API client

## Getting Started

### Prerequisites

- .NET 10 SDK
- SQL Server LocalDB or SQL Server
- Docker
- Docker Compose
- PowerShell

## Local Development

For local development, it is recommended to keep connection strings out of `appsettings.json` and use **User Secrets** instead.

### Configure local secrets manually

Run from the repository root:

```
#PowerShell
dotnet user-secrets init --project .\src\CommandApi\Users.Example.CommandApi.Site.csproj
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YOUR_LOCAL_CONNECTION_STRING" --project .\src\CommandApi\Users.Example.CommandApi.Site.csproj
```
## Run locally

```
dotnet build .\Users.Example.All.sln
dotnet run --project .\src\CommandApi\Users.Example.CommandApi.Site.csproj
```

Swagger is typically available at:

```
http://localhost:<port>/swagger
```

## Docker

The project can also be started in containers.

### Prepare Docker environment files

Go to: `infrastructure/docker/`

Build: `docker compose build`
Run: `docker compose up`
Build & Run: `docker compose up --build`

After startup, the API should be available at:

```
http://localhost:8080
```

## License

MIT License.