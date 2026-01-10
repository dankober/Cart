# Stage 1: Build the .NET API
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build-api
WORKDIR /src

# Copy and restore
COPY src/Cart.Api/Cart.Api.csproj ./Cart.Api/
RUN dotnet restore ./Cart.Api/Cart.Api.csproj

# Copy source and build
COPY src/Cart.Api/ ./Cart.Api/
WORKDIR /src/Cart.Api
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Build the React frontend
FROM --platform=$BUILDPLATFORM node:20-alpine AS build-web
WORKDIR /src

# Copy package files and install dependencies
COPY src/Cart.Web/package*.json ./
RUN npm ci

# Copy source and build
COPY src/Cart.Web/ ./
RUN npm run build

# Stage 3: Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Create data directory for SQLite
RUN mkdir -p /app/data

# Copy API build
COPY --from=build-api /app/publish .

# Copy frontend build to wwwroot
COPY --from=build-web /src/dist ./wwwroot

# Set environment variables
ENV ASPNETCORE_URLS=http://+:7100
ENV Database__Path=/app/data/cart.db

EXPOSE 7100

ENTRYPOINT ["dotnet", "Cart.Api.dll"]
