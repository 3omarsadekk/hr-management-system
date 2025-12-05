namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class DepartmentSeedData
{
    public static void SeedDepartments(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>().HasData(
            new Department
            {
                Id = 1,
                Name = "Information Technology",
                Description = "Manages all IT infrastructure, software development, technical support, cybersecurity, and digital transformation initiatives. Responsible for maintaining enterprise systems, developing custom applications, and ensuring technology alignment with business objectives.",
                ManagerId = null,
                EmployeeCount = 5,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Department
            {
                Id = 2,
                Name = "Human Resources",
                Description = "Handles recruitment and talent acquisition, employee relations, compensation and benefits administration, performance management, HR policy development, training and development programs, and ensuring regulatory compliance with Egyptian labor laws.",
                ManagerId = null,
                EmployeeCount = 3,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Department
            {
                Id = 3,
                Name = "Finance",
                Description = "Manages financial operations including accounting, budgeting, financial reporting, cash flow management, tax compliance, audit coordination, financial analysis, and strategic financial planning. Ensures accurate financial records and regulatory compliance.",
                ManagerId = null,
                EmployeeCount = 4,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Department
            {
                Id = 4,
                Name = "Sales & Marketing",
                Description = "Drives revenue growth through sales strategy execution, customer acquisition and retention, market research and analysis, digital marketing campaigns, brand management, lead generation, and customer relationship management. Develops and implements go-to-market strategies.",
                ManagerId = null,
                EmployeeCount = 4,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Department
            {
                Id = 5,
                Name = "Operations",
                Description = "Oversees day-to-day business operations, process optimization, supply chain management, quality assurance, resource allocation, operational efficiency improvements, and cross-functional coordination to ensure smooth business execution.",
                ManagerId = null,
                EmployeeCount = 3,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Department
            {
                Id = 6,
                Name = "Customer Support",
                Description = "Provides exceptional customer service, technical support, issue resolution, customer satisfaction management, support ticket handling, product training for customers, and feedback collection to improve service quality and customer experience.",
                ManagerId = null,
                EmployeeCount = 3,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
