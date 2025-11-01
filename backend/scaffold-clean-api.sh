#!/bin/bash

################################################################################
# ASP.NET Clean Architecture Scaffolding Script
#
# This script automates the creation of a complete ASP.NET Web API solution
# following Clean Architecture principles with multiple layers.
#
# Usage:
#   ./scaffold-clean-api.sh --name MyProject [OPTIONS]
#
# Options:
#   --name          (Required) Solution name
#   --enable-cqrs   Enable CQRS pattern with MediatR
#   --framework     .NET version (default: net8.0)
#   --db-provider   Database provider: sqlserver, postgres, sqlite (default: sqlserver)
#   --include-tests Include test projects
#   --help          Display this help message
################################################################################

set -e  # Exit on error

# Color codes for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Default values
SOLUTION_NAME=""
ENABLE_CQRS=false
FRAMEWORK="net8.0"
DB_PROVIDER="sqlserver"
INCLUDE_TESTS=false

# Display banner
show_banner() {
    echo -e "${BLUE}"
    echo "╔═══════════════════════════════════════════════════════════╗"
    echo "║   ASP.NET Clean Architecture Scaffolding Script           ║"
    echo "║   Creates a complete multi-layer Web API solution         ║"
    echo "╚═══════════════════════════════════════════════════════════╝"
    echo -e "${NC}"
}

# Display help
show_help() {
    show_banner
    cat << EOF
Usage: $0 --name <SolutionName> [OPTIONS]

Options:
  --name <name>         (Required) Name of the solution
  --enable-cqrs         Enable CQRS pattern with MediatR
  --framework <version> .NET version (net6.0, net7.0, net8.0, net9.0)
                        Default: net8.0
  --db-provider <type>  Database provider: sqlserver, postgres, sqlite
                        Default: sqlserver
  --include-tests       Include test projects
  --help                Display this help message

Examples:
  $0 --name MyWebApi
  $0 --name MyWebApi --enable-cqrs --framework net8.0
  $0 --name MyWebApi --enable-cqrs --db-provider postgres --include-tests

EOF
    exit 0
}

# Parse command-line arguments
parse_arguments() {
    while [[ $# -gt 0 ]]; do
        case $1 in
            --name)
                SOLUTION_NAME="$2"
                shift 2
                ;;
            --enable-cqrs)
                ENABLE_CQRS=true
                shift
                ;;
            --framework)
                FRAMEWORK="$2"
                shift 2
                ;;
            --db-provider)
                DB_PROVIDER="$2"
                shift 2
                ;;
            --include-tests)
                INCLUDE_TESTS=true
                shift
                ;;
            --help)
                show_help
                ;;
            *)
                echo -e "${RED}Error: Unknown option: $1${NC}"
                echo "Use --help for usage information"
                exit 1
                ;;
        esac
    done

    # Validate required parameters
    if [[ -z "$SOLUTION_NAME" ]]; then
        echo -e "${RED}Error: --name is required${NC}"
        echo "Use --help for usage information"
        exit 1
    fi

    # Validate framework version
    if [[ ! "$FRAMEWORK" =~ ^net[6-9]\.0$ ]]; then
        echo -e "${YELLOW}Warning: Unusual framework version '$FRAMEWORK'. Proceeding anyway...${NC}"
    fi

    # Validate database provider
    if [[ ! "$DB_PROVIDER" =~ ^(sqlserver|postgres|sqlite)$ ]]; then
        echo -e "${RED}Error: Invalid database provider '$DB_PROVIDER'. Use: sqlserver, postgres, or sqlite${NC}"
        exit 1
    fi
}

# Print configuration summary
print_config() {
    echo -e "${GREEN}Configuration:${NC}"
    echo "  Solution Name: $SOLUTION_NAME"
    echo "  Framework: $FRAMEWORK"
    echo "  CQRS Enabled: $ENABLE_CQRS"
    echo "  Database Provider: $DB_PROVIDER"
    echo "  Include Tests: $INCLUDE_TESTS"
    echo ""
}

# Check prerequisites
check_prerequisites() {
    echo -e "${BLUE}[1/8] Checking prerequisites...${NC}"

    if ! command -v dotnet &> /dev/null; then
        echo -e "${RED}Error: .NET SDK is not installed${NC}"
        echo "Please install .NET SDK from https://dotnet.microsoft.com/download"
        exit 1
    fi

    local dotnet_version=$(dotnet --version)
    echo -e "${GREEN}✓ .NET SDK found (version: $dotnet_version)${NC}"
    echo ""
}

# Create solution structure
create_solution() {
    echo -e "${BLUE}[2/8] Creating solution structure...${NC}"

    # Check if directory already exists
    if [[ -d "$SOLUTION_NAME" ]]; then
        echo -e "${YELLOW}Warning: Directory '$SOLUTION_NAME' already exists${NC}"
        read -p "Do you want to continue? This may overwrite existing files. (y/N): " -n 1 -r
        echo
        if [[ ! $REPLY =~ ^[Yy]$ ]]; then
            echo "Operation cancelled."
            exit 1
        fi
    else
        mkdir -p "$SOLUTION_NAME"
    fi

    cd "$SOLUTION_NAME"

    # Create solution file
    dotnet new sln -n "$SOLUTION_NAME"
    echo -e "${GREEN}✓ Solution created${NC}"

    # Create directory structure
    mkdir -p src/Core
    mkdir -p src/Infrastructure
    mkdir -p src/Presentation
    echo ""
}

# Create projects
create_projects() {
    echo -e "${BLUE}[3/8] Creating projects...${NC}"

    # Create Domain project in src/Core/
    cd src/Core
    echo "Creating Domain layer..."
    dotnet new classlib -n "${SOLUTION_NAME}.Domain" -f "$FRAMEWORK"
    rm -f "${SOLUTION_NAME}.Domain/Class1.cs"
    mkdir -p "${SOLUTION_NAME}.Domain/Entities"
    mkdir -p "${SOLUTION_NAME}.Domain/ValueObjects"
    mkdir -p "${SOLUTION_NAME}.Domain/Enums"
    mkdir -p "${SOLUTION_NAME}.Domain/Interfaces"
    echo -e "${GREEN}✓ Domain project created${NC}"

    # Create Application project in src/Core/
    echo "Creating Application layer..."
    dotnet new classlib -n "${SOLUTION_NAME}.Application" -f "$FRAMEWORK"
    rm -f "${SOLUTION_NAME}.Application/Class1.cs"
    mkdir -p "${SOLUTION_NAME}.Application/Interfaces"
    mkdir -p "${SOLUTION_NAME}.Application/DTOs"
    mkdir -p "${SOLUTION_NAME}.Application/Services"
    mkdir -p "${SOLUTION_NAME}.Application/Mappings"
    mkdir -p "${SOLUTION_NAME}.Application/Validators"

    if [[ "$ENABLE_CQRS" == true ]]; then
        mkdir -p "${SOLUTION_NAME}.Application/Commands"
        mkdir -p "${SOLUTION_NAME}.Application/Queries"
        mkdir -p "${SOLUTION_NAME}.Application/Handlers"
    fi
    echo -e "${GREEN}✓ Application project created${NC}"
    cd ../Infrastructure

    # Create Infrastructure project in src/Infrastructure/
    echo "Creating Infrastructure layer..."
    dotnet new classlib -n "${SOLUTION_NAME}.Infrastructure" -f "$FRAMEWORK"
    rm -f "${SOLUTION_NAME}.Infrastructure/Class1.cs"
    mkdir -p "${SOLUTION_NAME}.Infrastructure/Data"
    mkdir -p "${SOLUTION_NAME}.Infrastructure/Repositories"
    mkdir -p "${SOLUTION_NAME}.Infrastructure/Services"
    mkdir -p "${SOLUTION_NAME}.Infrastructure/Configurations"
    echo -e "${GREEN}✓ Infrastructure project created${NC}"
    cd ../Presentation

    # Create WebApi project in src/Presentation/
    echo "Creating WebApi layer..."
    dotnet new webapi -n "${SOLUTION_NAME}.WebApi" -f "$FRAMEWORK"
    mkdir -p "${SOLUTION_NAME}.WebApi/Middleware"
    mkdir -p "${SOLUTION_NAME}.WebApi/Filters"
    mkdir -p "${SOLUTION_NAME}.WebApi/Extensions"
    echo -e "${GREEN}✓ WebApi project created${NC}"
    cd ../..

    echo ""
}

# Create test projects
create_test_projects() {
    if [[ "$INCLUDE_TESTS" == false ]]; then
        return
    fi

    echo -e "${BLUE}[4/8] Creating test projects...${NC}"

    mkdir -p tests
    cd tests

    # Unit tests for Domain
    echo "Creating Domain unit tests..."
    dotnet new xunit -n "${SOLUTION_NAME}.Domain.Tests" -f "$FRAMEWORK"
    echo -e "${GREEN}✓ Domain tests created${NC}"

    # Unit tests for Application
    echo "Creating Application unit tests..."
    dotnet new xunit -n "${SOLUTION_NAME}.Application.Tests" -f "$FRAMEWORK"
    echo -e "${GREEN}✓ Application tests created${NC}"

    # Integration tests for WebApi
    echo "Creating WebApi integration tests..."
    dotnet new xunit -n "${SOLUTION_NAME}.WebApi.Tests" -f "$FRAMEWORK"
    echo -e "${GREEN}✓ WebApi integration tests created${NC}"

    cd ..
    echo ""
}

# Add projects to solution
add_projects_to_solution() {
    echo -e "${BLUE}[5/8] Adding projects to solution...${NC}"

    # Add source projects
    dotnet sln add "src/Core/${SOLUTION_NAME}.Domain/${SOLUTION_NAME}.Domain.csproj"
    dotnet sln add "src/Core/${SOLUTION_NAME}.Application/${SOLUTION_NAME}.Application.csproj"
    dotnet sln add "src/Infrastructure/${SOLUTION_NAME}.Infrastructure/${SOLUTION_NAME}.Infrastructure.csproj"
    dotnet sln add "src/Presentation/${SOLUTION_NAME}.WebApi/${SOLUTION_NAME}.WebApi.csproj"

    # Add test projects if they exist
    if [[ "$INCLUDE_TESTS" == true ]]; then
        dotnet sln add "tests/${SOLUTION_NAME}.Domain.Tests/${SOLUTION_NAME}.Domain.Tests.csproj"
        dotnet sln add "tests/${SOLUTION_NAME}.Application.Tests/${SOLUTION_NAME}.Application.Tests.csproj"
        dotnet sln add "tests/${SOLUTION_NAME}.WebApi.Tests/${SOLUTION_NAME}.WebApi.Tests.csproj"
    fi

    echo -e "${GREEN}✓ Projects added to solution${NC}"
    echo ""
}

# Add project references
add_project_references() {
    echo -e "${BLUE}[6/8] Adding project references...${NC}"

    # Application references Domain
    dotnet add "src/Core/${SOLUTION_NAME}.Application/${SOLUTION_NAME}.Application.csproj" reference \
        "src/Core/${SOLUTION_NAME}.Domain/${SOLUTION_NAME}.Domain.csproj"
    echo -e "${GREEN}✓ Application → Domain${NC}"

    # Infrastructure references Application and Domain
    dotnet add "src/Infrastructure/${SOLUTION_NAME}.Infrastructure/${SOLUTION_NAME}.Infrastructure.csproj" reference \
        "src/Core/${SOLUTION_NAME}.Application/${SOLUTION_NAME}.Application.csproj" \
        "src/Core/${SOLUTION_NAME}.Domain/${SOLUTION_NAME}.Domain.csproj"
    echo -e "${GREEN}✓ Infrastructure → Application, Domain${NC}"

    # WebApi references Application and Infrastructure
    dotnet add "src/Presentation/${SOLUTION_NAME}.WebApi/${SOLUTION_NAME}.WebApi.csproj" reference \
        "src/Core/${SOLUTION_NAME}.Application/${SOLUTION_NAME}.Application.csproj" \
        "src/Infrastructure/${SOLUTION_NAME}.Infrastructure/${SOLUTION_NAME}.Infrastructure.csproj"
    echo -e "${GREEN}✓ WebApi → Application, Infrastructure${NC}"

    # Add test project references
    if [[ "$INCLUDE_TESTS" == true ]]; then
        dotnet add "tests/${SOLUTION_NAME}.Domain.Tests/${SOLUTION_NAME}.Domain.Tests.csproj" reference \
            "src/Core/${SOLUTION_NAME}.Domain/${SOLUTION_NAME}.Domain.csproj"
        echo -e "${GREEN}✓ Domain.Tests → Domain${NC}"

        dotnet add "tests/${SOLUTION_NAME}.Application.Tests/${SOLUTION_NAME}.Application.Tests.csproj" reference \
            "src/Core/${SOLUTION_NAME}.Application/${SOLUTION_NAME}.Application.csproj" \
            "src/Core/${SOLUTION_NAME}.Domain/${SOLUTION_NAME}.Domain.csproj"
        echo -e "${GREEN}✓ Application.Tests → Application, Domain${NC}"

        dotnet add "tests/${SOLUTION_NAME}.WebApi.Tests/${SOLUTION_NAME}.WebApi.Tests.csproj" reference \
            "src/Presentation/${SOLUTION_NAME}.WebApi/${SOLUTION_NAME}.WebApi.csproj" \
            "src/Core/${SOLUTION_NAME}.Application/${SOLUTION_NAME}.Application.csproj" \
            "src/Infrastructure/${SOLUTION_NAME}.Infrastructure/${SOLUTION_NAME}.Infrastructure.csproj"
        echo -e "${GREEN}✓ WebApi.Tests → WebApi, Application, Infrastructure${NC}"
    fi

    echo ""
}

# Install NuGet packages
install_packages() {
    echo -e "${BLUE}[7/8] Installing NuGet packages...${NC}"

    # Application layer packages
    echo "Installing Application layer packages..."
    dotnet add "src/Core/${SOLUTION_NAME}.Application/${SOLUTION_NAME}.Application.csproj" package FluentValidation --version 11.9.2
    dotnet add "src/Core/${SOLUTION_NAME}.Application/${SOLUTION_NAME}.Application.csproj" package FluentValidation.DependencyInjectionExtensions --version 11.9.2

    if [[ "$ENABLE_CQRS" == true ]]; then
        echo "Installing CQRS packages (MediatR)..."
        dotnet add "src/Core/${SOLUTION_NAME}.Application/${SOLUTION_NAME}.Application.csproj" package MediatR --version 12.4.1
    fi
    echo -e "${GREEN}✓ Application packages installed${NC}"

    # Infrastructure layer packages
    echo "Installing Infrastructure layer packages..."

    # Determine EF Core version based on framework
    local ef_version="8.0.11"
    if [[ "$FRAMEWORK" == "net9.0" ]]; then
        ef_version="9.0.0"
    fi

    # Entity Framework Core and database provider
    case $DB_PROVIDER in
        sqlserver)
            dotnet add "src/Infrastructure/${SOLUTION_NAME}.Infrastructure/${SOLUTION_NAME}.Infrastructure.csproj" package Microsoft.EntityFrameworkCore.SqlServer --version "$ef_version"
            ;;
        postgres)
            dotnet add "src/Infrastructure/${SOLUTION_NAME}.Infrastructure/${SOLUTION_NAME}.Infrastructure.csproj" package Npgsql.EntityFrameworkCore.PostgreSQL --version "$ef_version"
            ;;
        sqlite)
            dotnet add "src/Infrastructure/${SOLUTION_NAME}.Infrastructure/${SOLUTION_NAME}.Infrastructure.csproj" package Microsoft.EntityFrameworkCore.Sqlite --version "$ef_version"
            ;;
    esac

    dotnet add "src/Infrastructure/${SOLUTION_NAME}.Infrastructure/${SOLUTION_NAME}.Infrastructure.csproj" package Microsoft.EntityFrameworkCore.Design --version "$ef_version"
    dotnet add "src/Infrastructure/${SOLUTION_NAME}.Infrastructure/${SOLUTION_NAME}.Infrastructure.csproj" package Microsoft.EntityFrameworkCore.Tools --version "$ef_version"
    echo -e "${GREEN}✓ Infrastructure packages installed${NC}"

    # WebApi layer packages
    echo "Installing WebApi layer packages..."
    dotnet add "src/Presentation/${SOLUTION_NAME}.WebApi/${SOLUTION_NAME}.WebApi.csproj" package Swashbuckle.AspNetCore --version 6.8.1

    # Install JwtBearer based on framework version
    if [[ "$FRAMEWORK" == "net9.0" ]]; then
        dotnet add "src/Presentation/${SOLUTION_NAME}.WebApi/${SOLUTION_NAME}.WebApi.csproj" package Microsoft.AspNetCore.Authentication.JwtBearer --version 9.0.0
    else
        dotnet add "src/Presentation/${SOLUTION_NAME}.WebApi/${SOLUTION_NAME}.WebApi.csproj" package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.11
    fi
    echo -e "${GREEN}✓ WebApi packages installed${NC}"

    # Test packages
    if [[ "$INCLUDE_TESTS" == true ]]; then
        echo "Installing test packages..."
        for test_project in "Domain.Tests" "Application.Tests" "WebApi.Tests"; do
            dotnet add "tests/${SOLUTION_NAME}.${test_project}/${SOLUTION_NAME}.${test_project}.csproj" package Moq
            dotnet add "tests/${SOLUTION_NAME}.${test_project}/${SOLUTION_NAME}.${test_project}.csproj" package FluentAssertions
        done

        # Add additional packages for integration tests
        dotnet add "tests/${SOLUTION_NAME}.WebApi.Tests/${SOLUTION_NAME}.WebApi.Tests.csproj" package Microsoft.AspNetCore.Mvc.Testing
        dotnet add "tests/${SOLUTION_NAME}.WebApi.Tests/${SOLUTION_NAME}.WebApi.Tests.csproj" package Microsoft.EntityFrameworkCore.InMemory
        echo -e "${GREEN}✓ Test packages installed${NC}"
    fi

    echo ""
}

# Create starter files
create_starter_files() {
    echo -e "${BLUE}[8/8] Creating starter files...${NC}"

    # Create base entity in Domain
    cat > "src/Core/${SOLUTION_NAME}.Domain/Entities/BaseEntity.cs" << 'EOF'
namespace SOLUTION_NAME.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
EOF
    sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "src/Core/${SOLUTION_NAME}.Domain/Entities/BaseEntity.cs"

    # Create sample enum in Domain
    cat > "src/Core/${SOLUTION_NAME}.Domain/Enums/Status.cs" << 'EOF'
namespace SOLUTION_NAME.Domain.Enums;

/// <summary>
/// Generic status enumeration
/// </summary>
public enum Status
{
    Active = 1,
    Inactive = 2,
    Pending = 3,
    Archived = 4
}
EOF
    sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "src/Core/${SOLUTION_NAME}.Domain/Enums/Status.cs"

    # Create sample value object in Domain
    cat > "src/Core/${SOLUTION_NAME}.Domain/ValueObjects/Address.cs" << 'EOF'
namespace SOLUTION_NAME.Domain.ValueObjects;

/// <summary>
/// Example value object for address
/// </summary>
public class Address
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string ZipCode { get; private set; }
    public string Country { get; private set; }

    public Address(string street, string city, string state, string zipCode, string country)
    {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
    }

    public override string ToString()
    {
        return $"{Street}, {City}, {State} {ZipCode}, {Country}";
    }
}
EOF
    sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "src/Core/${SOLUTION_NAME}.Domain/ValueObjects/Address.cs"

    # Create repository interface in Domain
    cat > "src/Core/${SOLUTION_NAME}.Domain/Interfaces/IRepository.cs" << 'EOF'
namespace SOLUTION_NAME.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
EOF
    sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "src/Core/${SOLUTION_NAME}.Domain/Interfaces/IRepository.cs"

    # Create DbContext in Infrastructure
    cat > "src/Infrastructure/${SOLUTION_NAME}.Infrastructure/Data/ApplicationDbContext.cs" << 'EOF'
using Microsoft.EntityFrameworkCore;

namespace SOLUTION_NAME.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations from current assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
EOF
    sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "src/Infrastructure/${SOLUTION_NAME}.Infrastructure/Data/ApplicationDbContext.cs"

    # Create generic repository in Infrastructure
    cat > "src/Infrastructure/${SOLUTION_NAME}.Infrastructure/Repositories/Repository.cs" << 'EOF'
using Microsoft.EntityFrameworkCore;
using SOLUTION_NAME.Domain.Interfaces;
using SOLUTION_NAME.Infrastructure.Data;

namespace SOLUTION_NAME.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
EOF
    sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "src/Infrastructure/${SOLUTION_NAME}.Infrastructure/Repositories/Repository.cs"

    # Create DI extension for Infrastructure
    cat > "src/Infrastructure/${SOLUTION_NAME}.Infrastructure/DependencyInjection.cs" << 'EOF'
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SOLUTION_NAME.Domain.Interfaces;
using SOLUTION_NAME.Infrastructure.Data;
using SOLUTION_NAME.Infrastructure.Repositories;

namespace SOLUTION_NAME.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.DB_PROVIDER_CONFIG("DefaultConnection")));

        // Register repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}
EOF
    sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "src/Infrastructure/${SOLUTION_NAME}.Infrastructure/DependencyInjection.cs"

    case $DB_PROVIDER in
        sqlserver)
            sed -i "s/DB_PROVIDER_CONFIG/UseSqlServer(configuration.GetConnectionString/g" "src/Infrastructure/${SOLUTION_NAME}.Infrastructure/DependencyInjection.cs"
            ;;
        postgres)
            sed -i "s/DB_PROVIDER_CONFIG/UseNpgsql(configuration.GetConnectionString/g" "src/Infrastructure/${SOLUTION_NAME}.Infrastructure/DependencyInjection.cs"
            ;;
        sqlite)
            sed -i "s/DB_PROVIDER_CONFIG/UseSqlite(configuration.GetConnectionString/g" "src/Infrastructure/${SOLUTION_NAME}.Infrastructure/DependencyInjection.cs"
            ;;
    esac

    # Create sample configuration in Infrastructure
    cat > "src/Infrastructure/${SOLUTION_NAME}.Infrastructure/Configurations/SampleEntityConfiguration.cs" << 'EOF'
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOLUTION_NAME.Domain.Entities;

namespace SOLUTION_NAME.Infrastructure.Configurations;

/// <summary>
/// Sample entity configuration - Replace with your actual entity configurations
/// </summary>
public class SampleEntityConfiguration : IEntityTypeConfiguration<BaseEntity>
{
    public void Configure(EntityTypeBuilder<BaseEntity> builder)
    {
        // Example configuration
        builder.HasKey(e => e.Id);
        builder.Property(e => e.CreatedAt).IsRequired();
    }
}
EOF
    sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "src/Infrastructure/${SOLUTION_NAME}.Infrastructure/Configurations/SampleEntityConfiguration.cs"

    # Create sample service in Infrastructure
    cat > "src/Infrastructure/${SOLUTION_NAME}.Infrastructure/Services/SampleExternalService.cs" << 'EOF'
namespace SOLUTION_NAME.Infrastructure.Services;

/// <summary>
/// Sample external service - Replace with your actual external services (Email, SMS, etc.)
/// </summary>
public class SampleExternalService
{
    public async Task<string> GetExternalDataAsync()
    {
        await Task.CompletedTask;
        // Add your implementation here
        return "Sample external data";
    }
}
EOF
    sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "src/Infrastructure/${SOLUTION_NAME}.Infrastructure/Services/SampleExternalService.cs"

    # ==================== APPLICATION LAYER ====================

    # Create DI extension for Application
    cat > "src/Core/${SOLUTION_NAME}.Application/DependencyInjection.cs" << 'EOF'
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SOLUTION_NAME.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // Add FluentValidation
        services.AddValidatorsFromAssembly(assembly);

        MEDIATR_CONFIG

        return services;
    }
}
EOF
    sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "src/Core/${SOLUTION_NAME}.Application/DependencyInjection.cs"

    if [[ "$ENABLE_CQRS" == true ]]; then
        sed -i "s/MEDIATR_CONFIG/\/\/ Add MediatR\n        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));/g" "src/Core/${SOLUTION_NAME}.Application/DependencyInjection.cs"

        # Create sample command
        cat > "src/Core/${SOLUTION_NAME}.Application/Commands/SampleCommand.cs" << 'EOF'
using MediatR;

namespace SOLUTION_NAME.Application.Commands;

/// <summary>
/// Sample command - Replace with your actual commands
/// </summary>
public record SampleCommand(string Name) : IRequest<Guid>;
EOF
        sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "src/Core/${SOLUTION_NAME}.Application/Commands/SampleCommand.cs"

        # Create sample query
        cat > "src/Core/${SOLUTION_NAME}.Application/Queries/GetSampleQuery.cs" << 'EOF'
using MediatR;
using SOLUTION_NAME.Application.DTOs;

namespace SOLUTION_NAME.Application.Queries;

/// <summary>
/// Sample query - Replace with your actual queries
/// </summary>
public record GetSampleQuery(Guid Id) : IRequest<SampleDto?>;
EOF
        sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "src/Core/${SOLUTION_NAME}.Application/Queries/GetSampleQuery.cs"

        # Create sample handler
        cat > "src/Core/${SOLUTION_NAME}.Application/Handlers/SampleCommandHandler.cs" << 'EOF'
using MediatR;
using SOLUTION_NAME.Application.Commands;

namespace SOLUTION_NAME.Application.Handlers;

/// <summary>
/// Sample command handler - Replace with your actual handlers
/// </summary>
public class SampleCommandHandler : IRequestHandler<SampleCommand, Guid>
{
    public async Task<Guid> Handle(SampleCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // Add your implementation here
        return Guid.NewGuid();
    }
}
EOF
        sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "src/Core/${SOLUTION_NAME}.Application/Handlers/SampleCommandHandler.cs"
    else
        sed -i "s/MEDIATR_CONFIG//g" "src/Core/${SOLUTION_NAME}.Application/DependencyInjection.cs"
    fi

    # Create sample DTO in Application
    cat > "src/Core/${SOLUTION_NAME}.Application/DTOs/SampleDto.cs" << 'EOF'
namespace SOLUTION_NAME.Application.DTOs;

/// <summary>
/// Sample DTO - Replace with your actual DTOs
/// </summary>
public class SampleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
EOF
    sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "src/Core/${SOLUTION_NAME}.Application/DTOs/SampleDto.cs"

    # Create sample service interface in Application
    cat > "src/Core/${SOLUTION_NAME}.Application/Interfaces/ISampleService.cs" << 'EOF'
namespace SOLUTION_NAME.Application.Interfaces;

/// <summary>
/// Sample service interface - Replace with your actual service interfaces
/// </summary>
public interface ISampleService
{
    Task<IEnumerable<string>> GetAllAsync();
    Task<string?> GetByIdAsync(Guid id);
}
EOF
    sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "src/Core/${SOLUTION_NAME}.Application/Interfaces/ISampleService.cs"

    # Create sample service in Application
    cat > "src/Core/${SOLUTION_NAME}.Application/Services/SampleService.cs" << 'EOF'
using SOLUTION_NAME.Application.Interfaces;

namespace SOLUTION_NAME.Application.Services;

/// <summary>
/// Sample service implementation - Replace with your actual services
/// </summary>
public class SampleService : ISampleService
{
    public async Task<IEnumerable<string>> GetAllAsync()
    {
        await Task.CompletedTask;
        return new List<string> { "Sample1", "Sample2" };
    }

    public async Task<string?> GetByIdAsync(Guid id)
    {
        await Task.CompletedTask;
        return $"Sample-{id}";
    }
}
EOF
    sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "src/Core/${SOLUTION_NAME}.Application/Services/SampleService.cs"

    # Create sample mapping helper in Application
    cat > "src/Core/${SOLUTION_NAME}.Application/Mappings/MappingHelper.cs" << 'EOF'
namespace SOLUTION_NAME.Application.Mappings;

/// <summary>
/// Manual mapping helper - Replace with your actual mapping logic
/// You can install AutoMapper later if needed
/// </summary>
public static class MappingHelper
{
    // Example: Map entity to DTO manually
    // public static SampleDto ToDto(this SampleEntity entity)
    // {
    //     return new SampleDto
    //     {
    //         Id = entity.Id,
    //         Name = entity.Name
    //     };
    // }
}
EOF
    sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "src/Core/${SOLUTION_NAME}.Application/Mappings/MappingHelper.cs"

    # Create sample validator in Application
    cat > "src/Core/${SOLUTION_NAME}.Application/Validators/SampleDtoValidator.cs" << 'EOF'
using FluentValidation;
using SOLUTION_NAME.Application.DTOs;

namespace SOLUTION_NAME.Application.Validators;

/// <summary>
/// Sample validator - Replace with your actual validators
/// </summary>
public class SampleDtoValidator : AbstractValidator<SampleDto>
{
    public SampleDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty);
    }
}
EOF
    sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "src/Core/${SOLUTION_NAME}.Application/Validators/SampleDtoValidator.cs"

    # ==================== WEBAPI LAYER ====================

    # Create sample middleware
    cat > "src/Presentation/${SOLUTION_NAME}.WebApi/Middleware/SampleMiddleware.cs" << 'EOF'
namespace SOLUTION_NAME.WebApi.Middleware;

/// <summary>
/// Sample middleware - Replace with your actual middleware
/// </summary>
public class SampleMiddleware
{
    private readonly RequestDelegate _next;

    public SampleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Add your logic before the next middleware
        await _next(context);
        // Add your logic after the next middleware
    }
}
EOF
    sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "src/Presentation/${SOLUTION_NAME}.WebApi/Middleware/SampleMiddleware.cs"

    # Create sample filter
    cat > "src/Presentation/${SOLUTION_NAME}.WebApi/Filters/SampleActionFilter.cs" << 'EOF'
using Microsoft.AspNetCore.Mvc.Filters;

namespace SOLUTION_NAME.WebApi.Filters;

/// <summary>
/// Sample action filter - Replace with your actual filters
/// </summary>
public class SampleActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Code to execute before the action
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Code to execute after the action
    }
}
EOF
    sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "src/Presentation/${SOLUTION_NAME}.WebApi/Filters/SampleActionFilter.cs"

    # Create sample extension
    cat > "src/Presentation/${SOLUTION_NAME}.WebApi/Extensions/ServiceCollectionExtensions.cs" << 'EOF'
namespace SOLUTION_NAME.WebApi.Extensions;

/// <summary>
/// Sample extension methods - Replace with your actual extensions
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        // Add your custom service registrations here
        return services;
    }
}
EOF
    sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "src/Presentation/${SOLUTION_NAME}.WebApi/Extensions/ServiceCollectionExtensions.cs"

    # Create appsettings.json with connection string
    cat > "src/Presentation/${SOLUTION_NAME}.WebApi/appsettings.json" << 'EOF'
{
  "ConnectionStrings": {
    "DefaultConnection": "CONNECTION_STRING_PLACEHOLDER"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
EOF

    case $DB_PROVIDER in
        sqlserver)
            sed -i 's/CONNECTION_STRING_PLACEHOLDER/Server=(localdb)\\\\mssqllocaldb;Database=DATABASE_NAME;Trusted_Connection=True;MultipleActiveResultSets=true/g' "src/Presentation/${SOLUTION_NAME}.WebApi/appsettings.json"
            ;;
        postgres)
            sed -i 's/CONNECTION_STRING_PLACEHOLDER/Host=localhost;Database=DATABASE_NAME;Username=postgres;Password=postgres/g' "src/Presentation/${SOLUTION_NAME}.WebApi/appsettings.json"
            ;;
        sqlite)
            sed -i 's/CONNECTION_STRING_PLACEHOLDER/Data Source=DATABASE_NAME.db/g' "src/Presentation/${SOLUTION_NAME}.WebApi/appsettings.json"
            ;;
    esac
    sed -i "s/DATABASE_NAME/${SOLUTION_NAME}/g" "src/Presentation/${SOLUTION_NAME}.WebApi/appsettings.json"

    # Update Program.cs to use DI extensions
    cat > "src/Presentation/${SOLUTION_NAME}.WebApi/Program.cs" << 'EOF'
using SOLUTION_NAME.Application;
using SOLUTION_NAME.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Application and Infrastructure layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
EOF
    sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "src/Presentation/${SOLUTION_NAME}.WebApi/Program.cs"

    # Create README
    cat > "README.md" << 'EOF'
# SOLUTION_NAME

A Clean Architecture ASP.NET Web API project.

## Architecture

This solution follows Clean Architecture principles with the following layers:

- **Domain**: Core business entities, value objects, and interfaces
- **Application**: Business logic, use cases, DTOs, and service interfaces
- **Infrastructure**: Data access, external services, and infrastructure concerns
- **WebApi**: REST API endpoints and presentation layer

## Project Structure

```
SOLUTION_NAME/
├── src/
│   ├── Core/
│   │   ├── SOLUTION_NAME.Domain/          # Enterprise business rules
│   │   └── SOLUTION_NAME.Application/     # Application business rules
│   ├── Infrastructure/
│   │   └── SOLUTION_NAME.Infrastructure/  # External concerns & data access
│   └── Presentation/
│       └── SOLUTION_NAME.WebApi/          # API controllers & HTTP concerns
TEST_STRUCTURE
└── SOLUTION_NAME.sln
```

## Technologies

- .NET FRAMEWORK_VERSION
- Entity Framework Core
- FluentValidation
CQRS_TECH
- Swagger/OpenAPI

## Database

This project is configured to use **DB_PROVIDER_NAME**.

Connection string can be found in `src/Presentation/SOLUTION_NAME.WebApi/appsettings.json`.

## Getting Started

### Prerequisites

- .NET SDK FRAMEWORK_VERSION or later
- DATABASE_PREREQ

### Running the Application

1. Navigate to the solution directory:
   ```bash
   cd SOLUTION_NAME
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Update the database:
   ```bash
   dotnet ef database update --project src/Infrastructure/SOLUTION_NAME.Infrastructure --startup-project src/Presentation/SOLUTION_NAME.WebApi
   ```

4. Run the application:
   ```bash
   dotnet run --project src/Presentation/SOLUTION_NAME.WebApi
   ```

5. Open your browser and navigate to `https://localhost:5001/swagger` to explore the API.

### Running Tests

TEST_COMMANDS

## Development

### Adding a New Entity

1. Create the entity in `Domain/Entities/`
2. Add a repository interface if needed in `Domain/Interfaces/`
3. Create DTOs in `Application/DTOs/`
4. Implement services in `Application/Services/`CQRS_STEPS
5. Add a controller in `WebApi/Controllers/`

### Database Migrations

To create a new migration:
```bash
dotnet ef migrations add MigrationName --project src/Infrastructure/SOLUTION_NAME.Infrastructure --startup-project src/Presentation/SOLUTION_NAME.WebApi
```

To update the database:
```bash
dotnet ef database update --project src/Infrastructure/SOLUTION_NAME.Infrastructure --startup-project src/Presentation/SOLUTION_NAME.WebApi
```

## Contributing

1. Follow Clean Architecture principles
2. Keep dependencies pointing inward (Domain has no dependencies)
3. Use interfaces for external dependencies
4. Write tests for business logic

## License

This project is licensed under the MIT License.
EOF
    sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "README.md"
    sed -i "s/FRAMEWORK_VERSION/${FRAMEWORK}/g" "README.md"

    case $DB_PROVIDER in
        sqlserver)
            sed -i "s/DB_PROVIDER_NAME/SQL Server/g" "README.md"
            sed -i "s/DATABASE_PREREQ/SQL Server or LocalDB/g" "README.md"
            ;;
        postgres)
            sed -i "s/DB_PROVIDER_NAME/PostgreSQL/g" "README.md"
            sed -i "s/DATABASE_PREREQ/PostgreSQL/g" "README.md"
            ;;
        sqlite)
            sed -i "s/DB_PROVIDER_NAME/SQLite/g" "README.md"
            sed -i "s/DATABASE_PREREQ/SQLite (no additional installation required)/g" "README.md"
            ;;
    esac

    if [[ "$ENABLE_CQRS" == true ]]; then
        sed -i "s/CQRS_TECH/- MediatR (CQRS pattern)/g" "README.md"
        sed -i "s/CQRS_STEPS/\n5. Or create commands\/queries in \`Application\/Commands\/\` or \`Application\/Queries\/\` with handlers/g" "README.md"
    else
        sed -i "s/CQRS_TECH//g" "README.md"
        sed -i "s/CQRS_STEPS//g" "README.md"
    fi

    if [[ "$INCLUDE_TESTS" == true ]]; then
        sed -i "s/TEST_STRUCTURE/├── tests\/\n│   ├── SOLUTION_NAME.Domain.Tests\/\n│   ├── SOLUTION_NAME.Application.Tests\/\n│   └── SOLUTION_NAME.WebApi.Tests\//g" "README.md"
        sed -i "s/TEST_COMMANDS/Run all tests:\n\`\`\`bash\ndotnet test\n\`\`\`\n\nRun tests for a specific project:\n\`\`\`bash\ndotnet test tests\/SOLUTION_NAME.Application.Tests\n\`\`\`/g" "README.md"
        sed -i "s/SOLUTION_NAME/${SOLUTION_NAME}/g" "README.md"
    else
        sed -i "s/TEST_STRUCTURE//g" "README.md"
        sed -i "s/TEST_COMMANDS/No tests included. Use --include-tests flag to add test projects./g" "README.md"
    fi

    # Create .gitignore
    cat > ".gitignore" << 'EOF'
## Ignore Visual Studio temporary files, build results, and
## files generated by popular Visual Studio add-ons.

# User-specific files
*.suo
*.user
*.userosscache
*.sln.docstates

# Build results
[Dd]ebug/
[Dd]ebugPublic/
[Rr]elease/
[Rr]eleases/
x64/
x86/
[Bb]in/
[Oo]bj/
[Ll]og/

# Visual Studio cache/options directory
.vs/

# Visual Studio Code
.vscode/

# ReSharper
_ReSharper*/
*.[Rr]e[Ss]harper
*.DotSettings.user

# JetBrains Rider
.idea/
*.sln.iml

# NuGet Packages
*.nupkg
**/packages/*

# Database files
*.db
*.db-shm
*.db-wal

# Environment files
*.env
.env.*

# OS generated files
.DS_Store
Thumbs.db
EOF

    echo -e "${GREEN}✓ Starter files created${NC}"
    echo ""
}

# Build solution
build_solution() {
    echo -e "${BLUE}Building solution...${NC}"
    if dotnet build --nologo; then
        echo -e "${GREEN}✓ Solution built successfully${NC}"
    else
        echo -e "${YELLOW}⚠ Build completed with warnings or errors${NC}"
    fi
    echo ""
}

# Print success message
print_success() {
    echo -e "${GREEN}"
    echo "╔═══════════════════════════════════════════════════════════╗"
    echo "║              🎉 Setup Complete! 🎉                        ║"
    echo "╚═══════════════════════════════════════════════════════════╝"
    echo -e "${NC}"
    echo -e "Your Clean Architecture solution '${GREEN}$SOLUTION_NAME${NC}' is ready!"
    echo ""
    echo "Next steps:"
    echo ""
    echo "  1. Navigate to your project:"
    echo "     ${BLUE}cd $SOLUTION_NAME${NC}"
    echo ""
    echo "  2. Open in your IDE or editor:"
    echo "     ${BLUE}code . ${NC}  # VS Code"
    echo "     ${BLUE}rider . ${NC}  # JetBrains Rider"
    echo ""
    echo "  3. Create your first migration:"
    echo "     ${BLUE}dotnet ef migrations add InitialCreate --project src/${SOLUTION_NAME}.Infrastructure --startup-project src/${SOLUTION_NAME}.WebApi${NC}"
    echo ""
    echo "  4. Update the database:"
    echo "     ${BLUE}dotnet ef database update --project src/${SOLUTION_NAME}.Infrastructure --startup-project src/${SOLUTION_NAME}.WebApi${NC}"
    echo ""
    echo "  5. Run the application:"
    echo "     ${BLUE}dotnet run --project src/${SOLUTION_NAME}.WebApi${NC}"
    echo ""
    echo "  6. Access Swagger UI at:"
    echo "     ${BLUE}https://localhost:5001/swagger${NC}"
    echo ""
    echo "📚 Check out the README.md for more information!"
    echo ""
}

# Main execution flow
main() {
    show_banner
    parse_arguments "$@"
    print_config

    check_prerequisites
    create_solution
    create_projects
    create_test_projects
    add_projects_to_solution
    add_project_references
    install_packages
    create_starter_files
    build_solution

    print_success
}

# Run main function
main "$@"
