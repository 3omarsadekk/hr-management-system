namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class DesignationSeedData
{
    public static void SeedDesignations(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Designation>().HasData(
            // IT Designations
            new Designation
            {
                Id = 1,
                Title = "Software Engineer",
                Description = "Develops and maintains software applications using modern programming languages and frameworks. Designs technical solutions, writes clean code, performs code reviews, and collaborates with cross-functional teams.",
                EmployeeCount = 2,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 2,
                Title = "Senior Software Engineer",
                Description = "Lead developer responsible for complex technical solutions, system architecture, mentoring junior developers, and technical decision-making. Drives best practices and innovation.",
                EmployeeCount = 1,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 3,
                Title = "Team Lead",
                Description = "Manages and guides team members to achieve project goals. Responsible for team performance, resource allocation, sprint planning, stakeholder communication, and ensuring timely delivery.",
                EmployeeCount = 1,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 4,
                Title = "DevOps Engineer",
                Description = "Manages CI/CD pipelines, cloud infrastructure, containerization, monitoring systems, and automates deployment processes. Ensures high availability and system reliability.",
                EmployeeCount = 1,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 5,
                Title = "QA Engineer",
                Description = "Ensures software quality through manual and automated testing, creates test plans, identifies bugs, performs regression testing, and maintains test automation frameworks.",
                EmployeeCount = 0,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // HR Designations
            new Designation
            {
                Id = 6,
                Title = "HR Manager",
                Description = "Oversees all human resources operations including recruitment strategy, employee relations, performance management, policy development, and ensuring legal compliance with labor laws.",
                EmployeeCount = 1,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 7,
                Title = "HR Specialist",
                Description = "Handles recruitment, onboarding, employee relations, benefits administration, maintaining HR records, and supporting talent development initiatives.",
                EmployeeCount = 1,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 8,
                Title = "Recruiter",
                Description = "Focuses on talent acquisition, candidate sourcing and screening, conducting interviews, negotiating offers, and building talent pipelines for current and future needs.",
                EmployeeCount = 1,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Finance Designations
            new Designation
            {
                Id = 9,
                Title = "Financial Analyst",
                Description = "Analyzes financial data, prepares reports and forecasts, conducts variance analysis, supports budgeting processes, and provides insights for strategic decision-making.",
                EmployeeCount = 1,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 10,
                Title = "Accountant",
                Description = "Manages accounting records, processes financial transactions, prepares journal entries, reconciles accounts, handles accounts payable/receivable, and ensures accuracy of financial data.",
                EmployeeCount = 1,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 11,
                Title = "Finance Manager",
                Description = "Oversees financial planning, budget management, financial reporting, cash flow optimization, coordinates audits, and leads the finance team to achieve organizational financial goals.",
                EmployeeCount = 2,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Sales & Marketing Designations
            new Designation
            {
                Id = 12,
                Title = "Sales Representative",
                Description = "Drives sales through prospecting, lead qualification, product presentations, negotiation, and closing deals. Builds and maintains strong customer relationships for long-term growth.",
                EmployeeCount = 2,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 13,
                Title = "Marketing Specialist",
                Description = "Develops and executes marketing campaigns, manages social media presence, creates content, conducts market research, analyzes campaign performance, and supports brand positioning.",
                EmployeeCount = 1,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 14,
                Title = "Business Development Manager",
                Description = "Identifies growth opportunities, develops partnerships, expands market presence, leads strategic initiatives, and drives revenue through new business channels and relationships.",
                EmployeeCount = 1,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Operations Designations
            new Designation
            {
                Id = 15,
                Title = "Operations Manager",
                Description = "Manages daily operations, drives process improvements, optimizes resource allocation, ensures quality standards, and coordinates cross-functional activities for operational excellence.",
                EmployeeCount = 1,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 16,
                Title = "Project Manager",
                Description = "Plans, executes, and closes projects successfully. Manages scope, timeline, budget, resources, stakeholder communication, and risk mitigation to ensure project objectives are met.",
                EmployeeCount = 1,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Customer Support Designations
            new Designation
            {
                Id = 17,
                Title = "Customer Support Representative",
                Description = "Provides customer assistance, resolves technical and service issues, handles support tickets, maintains customer satisfaction, and documents customer interactions.",
                EmployeeCount = 2,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Designation
            {
                Id = 18,
                Title = "Support Team Lead",
                Description = "Leads customer support team, ensures service quality standards, manages escalations, monitors performance metrics, trains team members, and drives customer satisfaction improvements.",
                EmployeeCount = 1,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
