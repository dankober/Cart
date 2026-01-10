# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Test Commands

```bash
# Build entire solution
dotnet build

# Run all tests
dotnet test

# Run a single test class
dotnet test --filter "FullyQualifiedName~HelloServiceTests"

# Run a single test
dotnet test --filter "FullyQualifiedName~GetGreeting_ReturnsExpectedMessage"

# Run API locally (port 7100)
dotnet run --project src/Cart.Api

# Frontend dev server (port 7101)
cd src/Cart.Web && npm install && npm run dev

# Docker (full stack)
docker compose up --build
```

## Architecture

This is a shopping cart app that organizes grocery lists by store aisle.

### Backend (.NET 8)
- **Controller/Service pattern**: Controllers handle HTTP, services contain business logic
- **Dependency Injection**: Services registered in `Program.cs` as scoped dependencies
- **SQLite via EF Core**: `CartDbContext` in `src/Cart.Api/Data/`
- API runs on port 7100, Swagger at `/swagger`

### Frontend (React + Vite + MUI)
- TypeScript SPA with mobile-first responsive design
- Dev server proxies `/api/*` to backend on port 7100
- Built assets served by ASP.NET in production

### Docker
- Multi-stage Dockerfile: builds both .NET and React, serves from single container
- SQLite persisted via volume mount to `/app/data`
- Multi-platform: `linux/amd64`, `linux/arm64`

## Key Patterns

When adding new features:
1. Create interface in `Services/I{Name}Service.cs`
2. Create implementation in `Services/{Name}Service.cs`
3. Register in `Program.cs`: `builder.Services.AddScoped<INameService, NameService>()`
4. Create controller in `Controllers/{Name}Controller.cs` that injects the service
5. Add tests in `Cart.Api.Tests/Services/{Name}ServiceTests.cs`

## Ports
- API: 7100
- Frontend dev: 7101
