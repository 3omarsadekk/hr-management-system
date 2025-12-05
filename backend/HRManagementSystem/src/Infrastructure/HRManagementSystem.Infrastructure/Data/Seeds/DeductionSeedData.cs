namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class DeductionSeedData
{
    public static void SeedDeductions(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Deduction>().HasData(
            // Percentage-Based Deductions (Egyptian Context)
            new Deduction
            {
                Id = 1,
                Name = "Social Insurance",
                Amount = 14.00m, // 14% employee contribution in Egypt
                IsPercentage = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Deduction
            {
                Id = 2,
                Name = "Income Tax - Tier 1",
                Amount = 2.50m, // 2.5% for first bracket
                IsPercentage = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Deduction
            {
                Id = 3,
                Name = "Income Tax - Tier 2",
                Amount = 10.00m, // 10% for second bracket
                IsPercentage = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Deduction
            {
                Id = 4,
                Name = "Income Tax - Tier 3",
                Amount = 15.00m, // 15% for third bracket
                IsPercentage = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Deduction
            {
                Id = 5,
                Name = "Professional Syndicate Fee",
                Amount = 1.00m, // 1% for professional syndicate
                IsPercentage = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Fixed Amount Deductions
            new Deduction
            {
                Id = 6,
                Name = "Late Arrival Fine",
                Amount = 50.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Deduction
            {
                Id = 7,
                Name = "Absence Penalty",
                Amount = 200.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Deduction
            {
                Id = 8,
                Name = "Loan Repayment - Personal",
                Amount = 500.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Deduction
            {
                Id = 9,
                Name = "Advance Salary Deduction",
                Amount = 1000.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Deduction
            {
                Id = 10,
                Name = "Equipment Damage Fee",
                Amount = 300.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Deduction
            {
                Id = 11,
                Name = "Uniform Replacement",
                Amount = 150.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Deduction
            {
                Id = 12,
                Name = "Health Insurance Premium",
                Amount = 250.00m,
                IsPercentage = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
