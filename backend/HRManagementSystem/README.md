# HRManagementSystem

A Clean Architecture ASP.NET Web API project.

## Architecture

This solution follows Clean Architecture principles with the following layers:

- **Domain**: Core business entities, value objects, and interfaces
- **Application**: Business logic, use cases, DTOs, and service interfaces
- **Infrastructure**: Data access, external services, and infrastructure concerns
- **WebApi**: REST API endpoints and presentation layer

## Project Structure

```
HRManagementSystem/
├── src/
│   ├── Core/
│   │   ├── HRManagementSystem.Domain/          # Enterprise business rules
│   │   └── HRManagementSystem.Application/     # Application business rules
│   ├── Infrastructure/
│   │   └── HRManagementSystem.Infrastructure/  # External concerns & data access
│   └── Presentation/
│       └── HRManagementSystem.WebApi/          # API controllers & HTTP concerns

└── HRManagementSystem.sln
```

## Technologies

- .NET net8.0
- Entity Framework Core
- FluentValidation

- Swagger/OpenAPI

## Database

This project is configured to use **SQL Server**.

Connection string can be found in `src/Presentation/HRManagementSystem.WebApi/appsettings.json`.

## Getting Started

### Prerequisites

- .NET SDK net8.0 or later
- SQL Server or LocalDB

### Running the Application

1. Navigate to the solution directory:
   ```bash
   cd HRManagementSystem
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Update the database:
   ```bash
   dotnet ef database update --project src/Infrastructure/HRManagementSystem.Infrastructure --startup-project src/Presentation/HRManagementSystem.WebApi
   ```

4. Run the application:
   ```bash
   dotnet run --project src/Presentation/HRManagementSystem.WebApi
   ```

5. Open your browser and navigate to `https://localhost:5001/swagger` to explore the API.

### Running Tests

No tests included. Use --include-tests flag to add test projects.

## Development

### Adding a New Entity

1. Create the entity in `Domain/Entities/`
2. Add a repository interface if needed in `Domain/Interfaces/`
3. Create DTOs in `Application/DTOs/`
4. Implement services in `Application/Services/`
5. Add a controller in `WebApi/Controllers/`

### Database Migrations

To create a new migration:
```bash
dotnet ef migrations add MigrationName --project src/Infrastructure/HRManagementSystem.Infrastructure --startup-project src/Presentation/HRManagementSystem.WebApi
```

To update the database:
```bash
dotnet ef database update --project src/Infrastructure/HRManagementSystem.Infrastructure --startup-project src/Presentation/HRManagementSystem.WebApi
```

## Contributing

1. Follow Clean Architecture principles
2. Keep dependencies pointing inward (Domain has no dependencies)
3. Use interfaces for external dependencies
4. Write tests for business logic

## License

This project is licensed under the MIT License.
