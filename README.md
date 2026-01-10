# Cart

A shopping cart assistance application that helps organize grocery lists by store aisle.

## Tech Stack

- **Backend**: .NET 8 Web API with SQLite
- **Frontend**: React + TypeScript + Vite + MUI
- **Infrastructure**: Docker with multi-platform support

## Development Setup

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 20](https://nodejs.org/) (use `nvm use` if you have nvm installed)
- [Docker](https://www.docker.com/) (optional, for containerized deployment)

### Running the API

```bash
cd src/Cart.Api
dotnet run
```

The API will be available at http://localhost:7100

- Swagger UI: http://localhost:7100/swagger

### Running the Frontend

```bash
cd src/Cart.Web
npm install
npm run dev
```

The frontend will be available at http://localhost:7101

## Docker Deployment

### Using Docker Compose (Recommended)

```bash
docker compose up --build
```

The application will be available at http://localhost:7100

### Using the Docker Hub Image

Create a `docker-compose.yml` file:

```yaml
services:
  cart:
    image: dankober/cart:latest
    ports:
      - "7100:7100"
    volumes:
      - ./data:/app/data
    environment:
      - Database__Path=/app/data/cart.db
    restart: unless-stopped
```

Then run:

```bash
docker compose up -d
```

The application will be available at http://localhost:7100

### Environment Variables

| Variable | Description | Default |
|----------|-------------|---------|
| `Database__Path` | Path to SQLite database file | `/app/data/cart.db` |

## Building Docker Image

### Build for current platform

```bash
docker build -t cart:latest .
```

### Build for multiple platforms

```bash
docker buildx build --platform linux/amd64,linux/arm64 -t cart:latest .
```

## Project Structure

```
Cart/
├── src/
│   ├── Cart.Api/          # .NET Web API
│   └── Cart.Web/          # React frontend
├── .github/workflows/     # CI/CD
├── Dockerfile
├── docker-compose.yml
└── REQUIREMENTS.md        # Project requirements
```

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/hello` | Hello world endpoint |
| GET | `/health` | Health check |
| GET | `/swagger` | API documentation |
