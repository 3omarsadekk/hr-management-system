namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class AllowanceSeedData
{
    public static void SeedAllowances(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Allowance>().HasData(
            // Fixed Amount Allowances
            new Allowance
            {
                Id = 1,
                Name = "Housing Allowance",
                Amount = 2000.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 2,
                Name = "Transportation Allowance",
                Amount = 800.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 3,
                Name = "Food Allowance",
                Amount = 500.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 4,
                Name = "Mobile Allowance",
                Amount = 300.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 5,
                Name = "Internet Allowance",
                Amount = 200.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 6,
                Name = "Health Insurance",
                Amount = 1500.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 7,
                Name = "Education Allowance",
                Amount = 1000.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 8,
                Name = "Utilities Allowance",
                Amount = 400.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Percentage-Based Allowances
            new Allowance
            {
                Id = 9,
                Name = "Annual Performance Bonus",
                Amount = 15.00m, // 15% of basic salary
                IsPercentage = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 10,
                Name = "Overtime Allowance",
                Amount = 50.00m, // 50% of hourly rate
                IsPercentage = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 11,
                Name = "Sales Commission",
                Amount = 5.00m, // 5% of sales
                IsPercentage = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 12,
                Name = "Night Shift Allowance",
                Amount = 25.00m, // 25% of basic salary for night shifts
                IsPercentage = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 13,
                Name = "Quarterly Bonus",
                Amount = 3000.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Allowance
            {
                Id = 14,
                Name = "Training Completion Bonus",
                Amount = 2000.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
