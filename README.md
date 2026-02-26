# Acxess - Subscriptions Management System

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![GitHub Actions](https://img.shields.io/badge/github%20actions-%232671E5.svg?style=for-the-badge&logo=githubactions&logoColor=white)


**Acxess** is a comprehensive SaaS platform designed for gyms and fitness centers or any business that relies on subscriptions to manage memberships, and streamline their daily operations.

Built with a focus on performance, maintainability, and operational excellence, this project showcases a production-ready clean architecture using modern .NET capabilities and a robust deployment pipeline.


## Tech Stack

The application is built using a **Modular Monolith** architecture, ensuring clear boundaries between entities domains while keeping deployment and infrastructure simple.

### Backend
**Framework:** .NET 9 (C#)
**Architecture:** Clean Architecture vertical slice with Domain Driven Design (DDD) principles.
**Database:** SQL Server
**ORM:** Entity Framework Core
**Authentication:** ASP.NET Core Identity

### Frontend
**Framework:** ASP.NET Core 9 Razor Pages
**Libs:** Tailwind CSS
**Interactivity:** HTMX & Alpine.js (chosen to provide a snappy, SPA-like experience without the overhead of a heavy JavaScript framework).

## CI/CD Pipeline & Releases

This project uses a Continuous Deployment approach:
1. **Immutable Releases:** Deployments are triggered automatically when a new GitHub Release is published.
2. **Database Migrations:** A dedicated, ephemeral `acxess-migrator` Docker container safely runs EF Core migrations before the main web application starts.
3. **Rollback Strategy:** Manual rollbacks can be executed instantly via GitHub Actions Workflow Dispatch by providing a previous version tag.

## Architecture modules
### Modular Monolith
**Module.Application:** Features and business logic.
**Module.Domain:** Entities, constants, enums, interfaces, and value objects.
**Module.Infrastructure:** Infrastructure and persistence.

**Acxess.Web:** Central point of entry for the application. It is responsible for imports and communication between bounded contexts using mediator pattern (MediatR).

## Local Development Setup
To run this project locally, you will need the [.NET 9 SDK](https://dotnet.microsoft.com/download) and a running instance of SQL Server (local or via Docker).

### 1. Clone the repository
```bash
git clone [https://github.com/Maumedev/Acxess.git](https://github.com/Maumedev/Acxess.git)
cd Acxess/src
```

### 2. Environment Configuration
* Copy the `.env.template` file to `.env` and fill in your local database credentials.

```bash
cp .env.template .env
```

* Create an appsettings.Localhost.json file in /Acxess.Web/ with the following content:

```json
{
   "ConnectionStrings": {
        "Default": "Data Source=localhost;Database=AcxessDB;User ID=sa;Password={your_password_from_env};Trust Server Certificate=True;"
    }
}
```

### 3 Run docker-compose.yml
Execute the following command to create the SQL Server container and run database migrations:

```bash
docker compose --profile tools up --build -d
```

### 4. Run the application
Use the .NET CLI to run the application targeting your local environment configuration:

```bash
ASPNETCORE_ENVIRONMENT=Localhost dotnet run --project ./Acxess.Web/
```