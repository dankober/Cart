# Cart

## Overview

This is going to be a shopping cart assistance application. It's very simple. You add items to a singular shopping list
before going to the store, and it organizes the items based on the category and aisle the item is in.

The app will be self-hosted on a personal server accessible via dynamic DNS. It will be used by multiple household
members but does not require authentication - it's a shared household list.

## Business Requirements

- The app has a dictionary of grocery items that can be modified in the app
- The items dictionary has common groceries in it (milk, bread, etc) and users can add items in the interface
- Items in the dictionary have a category (dairy, produce, etc)
- The app has a dictionary of stores with simple metadata (store name, address, etc)
- For each store, categories can be assigned to one or more aisles/departments (e.g., "Beverages" could be in both
  aisle 5 and aisle 12) with manual ordering per store
- After building a grocery list, users can select a store which will organize the items by aisle/department
- Users can mark items as retrieved (checkboxes)

## Technical Specifications

### Requirements

These are the hard and fast technical requirements for building this app.

- SQLite as the data layer
- React SPA front end with mobile-first responsive design
- REST API for frontend-backend communication
- CI/CD builds a multi-platform Docker image (linux/amd64, linux/arm64) for compatibility with standard x86 servers
  and ARM devices (Raspberry Pi, Apple Silicon, etc.)

### Considerations

These are interesting ideas/preferences that should be considered for building this application but Claude is welcome to
provide alternative ideas.

- gitignore should follow language standards
- README is kept up to date and includes an example Docker Compose file with volume mounts and environment variables
- I am a .NET developer so a .NET backend/API is preferred but not required if another solution is simpler
- API layer should provide a Swagger/OpenAPI spec with testing interface (SwaggerUI preferred)
- Application will likely be run out of Docker using Docker Compose that should allow the SQLite DB to live on the host
  using a volume mount; same with any settings that could live in a JSON file if we need it to
- Should use one of the common UI frameworks that has an OK license for personal projects (MUI, Bootstrap, or something
  else I haven't heard of)
- Should publish image to personal Docker registry... but I don't know how

## Approved Tech Stack

Based on requirements analysis, the following tech stack and patterns have been approved for implementation.

### Backend Stack
- **ASP.NET Core 8** (Web API)
  - Minimal API or Controller-based REST endpoints
  - Built-in OpenAPI/Swagger support with SwaggerUI
- **Entity Framework Core** with SQLite provider
  - Code-first migrations for schema management
  - Auto-apply migrations on container startup via `db.Database.Migrate()`
  - Migration history tracked in `__EFMigrationsHistory` table
- **NuGet Packages:**
  - `Microsoft.EntityFrameworkCore.Sqlite`
  - `Microsoft.EntityFrameworkCore.Design`
  - `Swashbuckle.AspNetCore`

### Frontend Stack
- **React 18+** with **Vite** (build tooling)
- **Material-UI (MUI)** for component library
- **React Router** for SPA routing
- **Axios** for HTTP/REST API client
- **React Context API** for global state management (cart, selected store)
- **npm Packages:**
  - `react`, `react-dom`, `react-router-dom`
  - `@mui/material`, `@mui/icons-material`
  - `@emotion/react`, `@emotion/styled` (MUI dependencies)
  - `axios`
  - `vite`, `@vitejs/plugin-react`

### Architecture Patterns

#### Backend:
- **Repository Pattern** for data access abstraction
- **Service Layer** for business logic
- **DTOs (Data Transfer Objects)** for API contracts
- **Dependency Injection** (built into ASP.NET Core)

#### Frontend:
- **Component-based architecture**
- **Custom hooks** for shared logic (e.g., `useItems`, `useStores`, `useCart`)
- **Context providers** for global state

### Project Structure
```
/Cart
├── backend/              # ASP.NET Core API
│   ├── Controllers/      # API endpoints
│   ├── Models/           # EF Core entities
│   ├── DTOs/             # API contracts
│   ├── Services/         # Business logic
│   ├── Repositories/     # Data access
│   └── Data/             # DbContext
├── frontend/             # React SPA
│   ├── src/
│   │   ├── components/   # React components
│   │   ├── hooks/        # Custom hooks
│   │   ├── services/     # API client (Axios)
│   │   └── context/      # Global state
├── Dockerfile            # Multi-stage build
├── docker-compose.yml    # Local deployment
└── .github/
    └── workflows/        # CI/CD
```

### Docker Strategy
- **Multi-stage Dockerfile** (build stage + runtime stage)
- Build images: `mcr.microsoft.com/dotnet/sdk:8.0` and `node:20`
- Runtime image: `mcr.microsoft.com/dotnet/aspnet:8.0`
- Multi-platform builds via Docker Buildx (`linux/amd64`, `linux/arm64`)
- Volume mounts for SQLite DB file and optional configuration JSON
- Frontend assets served by ASP.NET Core in production build

### CI/CD Strategy
- **GitHub Actions** workflow
- Build multi-architecture images using Buildx
- Push to Docker registry with semantic versioning tags
- Automated builds on push to main branch

### Database Migration Strategy
- EF Core migrations created during development: `dotnet ef migrations add <name>`
- Migrations auto-applied on application startup using `db.Database.Migrate()`
- Migration history tracked automatically by EF Core
- SQLite database file persists via Docker volume mount
- No manual migration steps required for deployments

### API Design
REST endpoints following RESTful conventions:
- `GET/POST/PUT/DELETE /api/items` - Item dictionary management
- `GET/POST/PUT/DELETE /api/stores` - Store management
- `GET/POST/PUT /api/stores/{id}/aisles` - Aisle/category mappings per store
- `GET/POST/DELETE /api/list/items` - Shopping list operations
- `PUT /api/list/items/{id}/retrieved` - Mark items as retrieved