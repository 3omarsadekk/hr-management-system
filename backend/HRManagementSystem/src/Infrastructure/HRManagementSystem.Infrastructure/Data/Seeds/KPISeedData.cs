namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class KPISeedData
{
    public static void SeedKPIs(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KPI>().HasData(
            // Sales KPIs
            new KPI
            {
                Id = 1,
                Code = "SALES-REV",
                Name = "Monthly Sales Revenue",
                Unit = "EGP",
                Target = 500000.00m,
                Description = "Total monthly sales revenue target for the sales team",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 2,
                Code = "SALES-CONV",
                Name = "Lead Conversion Rate",
                Unit = "%",
                Target = 25.00m,
                Description = "Percentage of leads converted to customers",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 3,
                Code = "SALES-DEAL",
                Name = "Average Deal Size",
                Unit = "EGP",
                Target = 15000.00m,
                Description = "Average value of closed deals",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // HR KPIs
            new KPI
            {
                Id = 4,
                Code = "HR-RETENTION",
                Name = "Employee Retention Rate",
                Unit = "%",
                Target = 90.00m,
                Description = "Percentage of employees retained over a 12-month period",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 5,
                Code = "HR-TTH",
                Name = "Time to Hire",
                Unit = "Days",
                Target = 30.00m,
                Description = "Average number of days to fill a position from job posting to offer acceptance",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 6,
                Code = "HR-SATISFACTION",
                Name = "Employee Satisfaction Score",
                Unit = "Score",
                Target = 4.20m,
                Description = "Average employee satisfaction rating on a 5-point scale",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 7,
                Code = "HR-TRAINING",
                Name = "Training Hours per Employee",
                Unit = "Hours",
                Target = 40.00m,
                Description = "Average training hours completed per employee annually",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // IT KPIs
            new KPI
            {
                Id = 8,
                Code = "IT-UPTIME",
                Name = "System Uptime",
                Unit = "%",
                Target = 99.50m,
                Description = "Percentage of time systems are operational and available",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 9,
                Code = "IT-RESOLUTION",
                Name = "Average Ticket Resolution Time",
                Unit = "Hours",
                Target = 4.00m,
                Description = "Average time to resolve IT support tickets",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 10,
                Code = "IT-DEPLOY",
                Name = "Deployment Frequency",
                Unit = "Count",
                Target = 20.00m,
                Description = "Number of successful deployments per month",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 11,
                Code = "IT-BUGFIX",
                Name = "Bug Fix Rate",
                Unit = "%",
                Target = 95.00m,
                Description = "Percentage of reported bugs fixed within SLA",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Finance KPIs
            new KPI
            {
                Id = 12,
                Code = "FIN-ROI",
                Name = "Return on Investment",
                Unit = "%",
                Target = 15.00m,
                Description = "Overall return on company investments",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 13,
                Code = "FIN-VARIANCE",
                Name = "Budget Variance",
                Unit = "%",
                Target = 5.00m,
                Description = "Acceptable variance between budgeted and actual expenses",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 14,
                Code = "FIN-COLLECT",
                Name = "Receivables Collection Period",
                Unit = "Days",
                Target = 30.00m,
                Description = "Average days to collect accounts receivable",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 15,
                Code = "FIN-MARGIN",
                Name = "Net Profit Margin",
                Unit = "%",
                Target = 20.00m,
                Description = "Net profit as a percentage of total revenue",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Customer Support KPIs
            new KPI
            {
                Id = 16,
                Code = "CS-CSAT",
                Name = "Customer Satisfaction Score",
                Unit = "Score",
                Target = 4.50m,
                Description = "Average customer satisfaction rating on a 5-point scale",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 17,
                Code = "CS-FRT",
                Name = "First Response Time",
                Unit = "Minutes",
                Target = 15.00m,
                Description = "Average time to first response for customer inquiries",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 18,
                Code = "CS-FCR",
                Name = "First Contact Resolution",
                Unit = "%",
                Target = 75.00m,
                Description = "Percentage of issues resolved on first contact",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Operations KPIs
            new KPI
            {
                Id = 19,
                Code = "OPS-EFFICIENCY",
                Name = "Operational Efficiency",
                Unit = "%",
                Target = 85.00m,
                Description = "Overall operational efficiency rating",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 20,
                Code = "OPS-CYCLE",
                Name = "Process Cycle Time",
                Unit = "Hours",
                Target = 24.00m,
                Description = "Average time to complete key operational processes",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Marketing KPIs
            new KPI
            {
                Id = 21,
                Code = "MKT-LEADS",
                Name = "Monthly Lead Generation",
                Unit = "Count",
                Target = 200.00m,
                Description = "Number of qualified leads generated per month",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 22,
                Code = "MKT-ENGAGEMENT",
                Name = "Social Media Engagement Rate",
                Unit = "%",
                Target = 5.00m,
                Description = "Percentage engagement rate on social media posts",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new KPI
            {
                Id = 23,
                Code = "MKT-CAC",
                Name = "Customer Acquisition Cost",
                Unit = "EGP",
                Target = 1000.00m,
                Description = "Average cost to acquire a new customer",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
