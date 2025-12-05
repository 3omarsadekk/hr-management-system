namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class TrainingCourseSeedData
{
    public static void SeedTrainingCourses(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TrainingCourse>().HasData(
            // Technical Skills Training
            new TrainingCourse
            {
                Id = 1,
                Title = "Advanced C# Programming",
                Description = "Master advanced C# concepts including async/await, LINQ, delegates, generics, and design patterns for enterprise application development.",
                DurationHours = 40,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new TrainingCourse
            {
                Id = 2,
                Title = "ASP.NET Core Web Development",
                Description = "Comprehensive training on building modern web applications with ASP.NET Core, Entity Framework Core, RESTful APIs, and authentication/authorization.",
                DurationHours = 48,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new TrainingCourse
            {
                Id = 3,
                Title = "Angular Framework Fundamentals",
                Description = "Learn to build dynamic single-page applications using Angular, TypeScript, RxJS, and modern frontend development practices.",
                DurationHours = 36,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new TrainingCourse
            {
                Id = 4,
                Title = "Cloud Computing with Azure",
                Description = "Hands-on training covering Azure services, cloud architecture, deployment strategies, monitoring, and cost optimization.",
                DurationHours = 32,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new TrainingCourse
            {
                Id = 5,
                Title = "DevOps and CI/CD Pipelines",
                Description = "Master continuous integration and deployment using Azure DevOps, Git, Docker, Kubernetes, and automation best practices.",
                DurationHours = 40,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new TrainingCourse
            {
                Id = 6,
                Title = "Database Design and SQL Server",
                Description = "Comprehensive training on database design, normalization, T-SQL, stored procedures, indexing strategies, and performance tuning.",
                DurationHours = 32,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new TrainingCourse
            {
                Id = 7,
                Title = "Cybersecurity Fundamentals",
                Description = "Essential cybersecurity concepts including threat analysis, security protocols, encryption, secure coding practices, and incident response.",
                DurationHours = 24,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new TrainingCourse
            {
                Id = 8,
                Title = "Machine Learning and AI Basics",
                Description = "Introduction to machine learning algorithms, data science, Python for ML, model training, and practical AI applications.",
                DurationHours = 48,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Business and Soft Skills Training
            new TrainingCourse
            {
                Id = 9,
                Title = "Leadership and Management Skills",
                Description = "Develop essential leadership competencies including team management, conflict resolution, strategic thinking, and decision-making.",
                DurationHours = 24,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new TrainingCourse
            {
                Id = 10,
                Title = "Effective Communication in the Workplace",
                Description = "Enhance communication skills for presentations, meetings, email etiquette, active listening, and cross-cultural communication.",
                DurationHours = 16,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new TrainingCourse
            {
                Id = 11,
                Title = "Project Management Professional (PMP) Prep",
                Description = "Comprehensive PMP certification preparation covering PMBOK guidelines, project lifecycle, risk management, and agile methodologies.",
                DurationHours = 60,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new TrainingCourse
            {
                Id = 12,
                Title = "Time Management and Productivity",
                Description = "Master productivity techniques, prioritization strategies, task management tools, and work-life balance practices.",
                DurationHours = 12,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Business-Specific Training
            new TrainingCourse
            {
                Id = 13,
                Title = "Financial Analysis and Reporting",
                Description = "Advanced financial analysis techniques, financial statement interpretation, budgeting, forecasting, and Excel skills for finance professionals.",
                DurationHours = 28,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new TrainingCourse
            {
                Id = 14,
                Title = "Sales Techniques and Customer Relations",
                Description = "Proven sales strategies, customer acquisition, negotiation tactics, CRM usage, and building long-term client relationships.",
                DurationHours = 20,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new TrainingCourse
            {
                Id = 15,
                Title = "Digital Marketing Strategies",
                Description = "Modern digital marketing including SEO, social media marketing, content strategy, email campaigns, and analytics.",
                DurationHours = 32,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Compliance and Safety
            new TrainingCourse
            {
                Id = 16,
                Title = "Workplace Safety and Health",
                Description = "Essential workplace safety protocols, emergency procedures, ergonomics, and health compliance standards.",
                DurationHours = 8,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new TrainingCourse
            {
                Id = 17,
                Title = "HR Ethics and Labor Law Compliance",
                Description = "Understanding Egyptian labor laws, employee rights, ethical HR practices, and regulatory compliance requirements.",
                DurationHours = 16,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new TrainingCourse
            {
                Id = 18,
                Title = "Customer Service Excellence",
                Description = "Best practices for exceptional customer service, complaint handling, customer satisfaction metrics, and service quality improvement.",
                DurationHours = 16,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
