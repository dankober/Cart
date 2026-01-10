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