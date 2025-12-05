namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class CompetencySeedData
{
    public static void SeedCompetencies(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Competency>().HasData(
            // Technical Competencies
            new Competency
            {
                Id = 1,
                Name = "C# Programming",
                Description = "Proficiency in C# language, object-oriented programming, and .NET framework development",
                CreatedAt = new DateTime(2024, 1, 1,  0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 2,
                Name = "Database Development",
                Description = "Skills in database design, SQL queries, stored procedures, and database optimization",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 3,
                Name = "Web Development",
                Description = "Expertise in web technologies including HTML, CSS, JavaScript, and modern frameworks",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 4,
                Name = "Cloud Computing",
                Description = "Knowledge of cloud platforms (Azure, AWS), containerization, and cloud-native architecture",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 5,
                Name = "DevOps Practices",
                Description = "Competency in CI/CD pipelines, infrastructure as code, automation, and monitoring",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 6,
                Name = "Software Testing",
                Description = "Proficiency in manual and automated testing, test planning, and quality assurance",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 7,
                Name = "System Architecture",
                Description = "Ability to design scalable, maintainable system architectures and technical solutions",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 8,
                Name = "Cybersecurity",
                Description = "Understanding of security principles, threat analysis, and secure coding practices",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Behavioral Competencies
            new Competency
            {
                Id = 9,
                Name = "Leadership",
                Description = "Ability to lead teams, make decisions, inspire others, and drive organizational success",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 10,
                Name = "Teamwork",
                Description = "Collaborating effectively with others, sharing knowledge, and contributing to team goals",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 11,
                Name = "Communication Skills",
                Description = "Clear and effective verbal and written communication with diverse audiences",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 12,
                Name = "Problem Solving",
                Description = "Analytical thinking, identifying root causes, and developing effective solutions",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 13,
                Name = "Adaptability",
                Description = "Flexibility to adjust to changing circumstances, learn new skills, and embrace change",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 14,
                Name = "Time Management",
                Description = "Prioritizing tasks, meeting deadlines, and managing multiple responsibilities effectively",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 15,
                Name = "Customer Focus",
                Description = "Understanding customer needs, providing excellent service, and building relationships",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Business Competencies
            new Competency
            {
                Id = 16,
                Name = "Financial Analysis",
                Description = "Analyzing financial data, preparing reports, forecasting, and budgeting",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 17,
                Name = "Project Management",
                Description = "Planning, executing, and delivering projects on time, within scope and budget",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 18,
                Name = "Sales and Negotiation",
                Description = "Selling products/services, negotiating deals, and closing business opportunities",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 19,
                Name = "Marketing Strategy",
                Description = "Developing marketing plans, understanding market dynamics, and brand management",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 20,
                Name = "Business Process Optimization",
                Description = "Identifying inefficiencies, implementing improvements, and streamlining operations",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Domain-Specific Competencies
            new Competency
            {
                Id = 21,
                Name = "HR Management",
                Description = "Talent acquisition, employee relations, performance management, and HR compliance",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 22,
                Name = "Accounting Principles",
                Description = "Understanding of accounting standards, financial reporting, and regulatory compliance",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 23,
                Name = "Customer Support",
                Description = "Handling customer inquiries, resolving issues, and maintaining customer satisfaction",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 24,
                Name = "Data Analysis",
                Description = "Collecting, processing, and interpreting data to support business decisions",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Competency
            {
                Id = 25,
                Name = "Strategic Thinking",
                Description = "Long-term planning, understanding industry trends, and aligning actions with strategy",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
