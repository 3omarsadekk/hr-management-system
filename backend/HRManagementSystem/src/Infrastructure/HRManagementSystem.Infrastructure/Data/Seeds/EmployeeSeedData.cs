namespace HRManagementSystem.Infrastructure.Data.Seeds;

public static class EmployeeSeedData
{
    public static void SeedEmployees(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasData(
            // IT Department (5 employees)
            new Employee
            {
                Id = 1,
                FirstName = "Ahmed",
                LastName = "Hassan",
                Email = "ahmed.hassan@company.com",
                DateOfBirth = new DateTime(1990, 5, 15),
                Gender = "Male",
                ContactNumber = "+20-123-456-7890",
                Address = "123 Cairo Street, Nasr City, Cairo, Egypt",
                HireDate = new DateTime(2022, 1, 15),
                DepartmentId = 1,
                DesignationId = 3, // Team Lead
                BasicSalary = 25000.00m,
                EFF_Start = new DateTime(2022, 1, 15),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Employee
            {
                Id = 2,
                FirstName = "Sarah",
                LastName = "Mohamed",
                Email = "sarah.mohamed@company.com",
                DateOfBirth = new DateTime(1992, 8, 22),
                Gender = "Female",
                ContactNumber = "+20-100-555-1234",
                Address = "456 Alexandria Road, Maadi, Cairo, Egypt",
                HireDate = new DateTime(2022, 3, 1),
                DepartmentId = 1,
                DesignationId = 2, // Senior Software Engineer
                BasicSalary = 22000.00m,
                EFF_Start = new DateTime(2022, 3, 1),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Employee
            {
                Id = 3,
                FirstName = "Khaled",
                LastName = "Ibrahim",
                Email = "khaled.ibrahim@company.com",
                DateOfBirth = new DateTime(1994, 11, 10),
                Gender = "Male",
                ContactNumber = "+20-111-222-3333",
                Address = "789 Pyramids Avenue, Giza, Egypt",
                HireDate = new DateTime(2023, 6, 1),
                DepartmentId = 1,
                DesignationId = 1, // Software Engineer
                BasicSalary = 18000.00m,
                EFF_Start = new DateTime(2023, 6, 1),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Employee
            {
                Id = 4,
                FirstName = " Mona",
                LastName = "Samir",
                Email = "mona.samir@company.com",
                DateOfBirth = new DateTime(1991, 3, 18),
                Gender = "Female",
                ContactNumber = "+20-122-888-9999",
                Address = "234 Heliopolis, Cairo, Egypt",
                HireDate = new DateTime(2023, 2, 20),
                DepartmentId = 1,
                DesignationId = 4, // DevOps Engineer
                BasicSalary = 21000.00m,
                EFF_Start = new DateTime(2023, 2, 20),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Employee
            {
                Id = 5,
                FirstName = "Youssef",
                LastName = "Tamer",
                Email = "youssef.tamer@company.com",
                DateOfBirth = new DateTime(1995, 7, 25),
                Gender = "Male",
                ContactNumber = "+20-155-444-5555",
                Address = "567 New Cairo, Egypt",
                HireDate = new DateTime(2024, 1, 10),
                DepartmentId = 1,
                DesignationId = 1, // Software Engineer
                BasicSalary = 17000.00m,
                EFF_Start = new DateTime(2024, 1, 10),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            
            // HR Department (3 employees)
            new Employee
            {
                Id = 6,
                FirstName = "Fatima",
                LastName = "Ali",
                Email = "fatima.ali@company.com",
                DateOfBirth = new DateTime(1987, 4, 18),
                Gender = "Female",
                ContactNumber = "+20-122-333-4444",
                Address = "321 Nile Corniche, Zamalek, Cairo, Egypt",
                HireDate = new DateTime(2021, 2, 15),
                DepartmentId = 2,
                DesignationId = 6, // HR Manager
                BasicSalary = 24000.00m,
                EFF_Start = new DateTime(2021, 2, 15),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Employee
            {
                Id = 7,
                FirstName = "Omar",
                LastName = "Mahmoud",
                Email = "omar.mahmoud@company.com",
                DateOfBirth = new DateTime(1991, 9, 5),
                Gender = "Male",
                ContactNumber = "+20-155-666-7777",
                Address = "654 Heliopolis Street, Cairo, Egypt",
                HireDate = new DateTime(2022, 5, 10),
                DepartmentId = 2,
                DesignationId = 7, // HR Specialist
                BasicSalary = 18000.00m,
                EFF_Start = new DateTime(2022, 5, 10),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Employee
            {
                Id = 8,
                FirstName = "Nada",
                LastName = "Khaled",
                Email = "nada.khaled@company.com",
                DateOfBirth = new DateTime(1993, 12, 8),
                Gender = "Female",
                ContactNumber = "+20-100-222-3333",
                Address = "890 Garden City, Cairo, Egypt",
                HireDate = new DateTime(2023, 8, 1),
                DepartmentId = 2,
                DesignationId = 8, // Recruiter
                BasicSalary = 16000.00m,
                EFF_Start = new DateTime(2023, 8, 1),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            
            // Finance Department (4 employees)
            new Employee
            {
                Id = 9,
                FirstName = "Mariam",
                LastName = "Youssef",
                Email = "mariam.youssef@company.com",
                DateOfBirth = new DateTime(1989, 12, 25),
                Gender = "Female",
                ContactNumber = "+20-101-888-9999",
                Address = "987 Garden City, Cairo, Egypt",
                HireDate = new DateTime(2021, 8, 1),
                DepartmentId = 3,
                DesignationId = 11, // Finance Manager
                BasicSalary = 26000.00m,
                EFF_Start = new DateTime(2021, 8, 1),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Employee
            {
                Id = 10,
                FirstName = "Hassan",
                LastName = "Saleh",
                Email = "hassan.saleh@company.com",
                DateOfBirth = new DateTime(1993, 3, 14),
                Gender = "Male",
                ContactNumber = "+20-127-111-2222",
                Address = "147 Downtown, Cairo, Egypt",
                HireDate = new DateTime(2023, 1, 20),
                DepartmentId = 3,
                DesignationId = 10, // Accountant
                BasicSalary = 17000.00m,
                EFF_Start = new DateTime(2023, 1, 20),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Employee
            {
                Id = 11,
                FirstName = "Dina",
                LastName = "Fathy",
                Email = "dina.fathy@company.com",
                DateOfBirth = new DateTime(1990, 6, 30),
                Gender = "Female",
                ContactNumber = "+20-110-555-6666",
                Address = "345 Mohandessin, Giza, Egypt",
                HireDate = new DateTime(2022, 4, 15),
                DepartmentId = 3,
                DesignationId = 9, // Financial Analyst
                BasicSalary = 19000.00m,
                EFF_Start = new DateTime(2022, 4, 15),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Employee
            {
                Id = 12,
                FirstName = "Amr",
                LastName = "Nasser",
                Email = "amr.nasser@company.com",
                DateOfBirth = new DateTime(1988, 10, 5),
                Gender = "Male",
                ContactNumber = "+20-106-777-8888",
                Address = "678 Dokki, Giza, Egypt",
                HireDate = new DateTime(2020, 11, 1),
                DepartmentId = 3,
                DesignationId = 11, // Finance Manager
                BasicSalary = 27000.00m,
                EFF_Start = new DateTime(2020, 11, 1),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            
            // Sales & Marketing Department (4 employees)
            new Employee
            {
                Id = 13,
                FirstName = "Nour",
                LastName = "Abdel",
                Email = "nour.abdel@company.com",
                DateOfBirth = new DateTime(1990, 7, 8),
                Gender = "Female",
                ContactNumber = "+20-150-333-4444",
                Address = "258 New Cairo, Egypt",
                HireDate = new DateTime(2022, 9, 1),
                DepartmentId = 4,
                DesignationId = 14, // Business Development Manager
                BasicSalary = 25000.00m,
                EFF_Start = new DateTime(2022, 9, 1),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Employee
            {
                Id = 14,
                FirstName = "Tarek",
                LastName = "Farid",
                Email = "tarek.farid@company.com",
                DateOfBirth = new DateTime(1994, 2, 20),
                Gender = "Male",
                ContactNumber = "+20-106-555-6666",
                Address = "369 6th October City, Egypt",
                HireDate = new DateTime(2023, 4, 15),
                DepartmentId = 4,
                DesignationId = 12, // Sales Representative
                BasicSalary = 16000.00m,
                EFF_Start = new DateTime(2023, 4, 15),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Employee
            {
                Id = 15,
                FirstName = "Hala",
                LastName = "Magdy",
                Email = "hala.magdy@company.com",
                DateOfBirth = new DateTime(1992, 5, 12),
                Gender = "Female",
                ContactNumber = "+20-122-999-0000",
                Address = "456 Zamalek, Cairo, Egypt",
                HireDate = new DateTime(2022, 11, 10),
                DepartmentId = 4,
                DesignationId = 13, // Marketing Specialist
                BasicSalary = 18000.00m,
                EFF_Start = new DateTime(2022, 11, 10),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Employee
            {
                Id = 16,
                FirstName = "Karim",
                LastName = "Said",
                Email = "karim.said@company.com",
                DateOfBirth = new DateTime(1989, 8, 28),
                Gender = "Male",
                ContactNumber = "+20-155-111-2222",
                Address = "123 Maadi, Cairo, Egypt",
                HireDate = new DateTime(2021, 7, 1),
                DepartmentId = 4,
                DesignationId = 12, // Sales Representative
                BasicSalary = 17000.00m,
                EFF_Start = new DateTime(2021, 7, 1),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            
            // Operations Department (3 employees)
            new Employee
            {
                Id = 17,
                FirstName = "Layla",
                LastName = "Kamal",
                Email = "layla.kamal@company.com",
                DateOfBirth = new DateTime(1986, 10, 30),
                Gender = "Female",
                ContactNumber = "+20-128-777-8888",
                Address = "741 Mohandessin, Giza, Egypt",
                HireDate = new DateTime(2021, 4, 10),
                DepartmentId = 5,
                DesignationId = 15, // Operations Manager
                BasicSalary = 28000.00m,
                EFF_Start = new DateTime(2021, 4, 10),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Employee
            {
                Id = 18,
                FirstName = "Waleed",
                LastName = "Adel",
                Email = "waleed.adel@company.com",
                DateOfBirth = new DateTime(1991, 1, 15),
                Gender = "Male",
                ContactNumber = "+20-100-333-4444",
                Address = "852 Nasr City, Cairo, Egypt",
                HireDate = new DateTime(2022, 6, 20),
                DepartmentId = 5,
                DesignationId = 16, // Project Manager
                BasicSalary = 23000.00m,
                EFF_Start = new DateTime(2022, 6, 20),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Employee
            {
                Id = 19,
                FirstName = "Salma",
                LastName = "Rashad",
                Email = "salma.rashad@company.com",
                DateOfBirth = new DateTime(1993, 9, 22),
                Gender = "Female",
                ContactNumber = "+20-127-555-6666",
                Address = "963 New Cairo, Egypt",
                HireDate = new DateTime(2023, 3, 5),
                DepartmentId = 5,
                DesignationId = 16, // Project Manager
                BasicSalary = 22000.00m,
                EFF_Start = new DateTime(2023, 3, 5),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            
            // Customer Support Department (3 employees)
            new Employee
            {
                Id = 20,
                FirstName = "Hossam",
                LastName = "Eldin",
                Email = "hossam.eldin@company.com",
                DateOfBirth = new DateTime(1992, 11, 18),
                Gender = "Male",
                ContactNumber = "+20-106-888-9999",
                Address = "147 Heliopolis, Cairo, Egypt",
                HireDate = new DateTime(2022, 8, 15),
                DepartmentId = 6,
                DesignationId = 18, // Support Team Lead
                BasicSalary = 20000.00m,
                EFF_Start = new DateTime(2022, 8, 15),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Employee
            {
                Id = 21,
                FirstName = "Rana",
                LastName = "Mostafa",
                Email = "rana.mostafa@company.com",
                DateOfBirth = new DateTime(1995, 4, 7),
                Gender = "Female",
                ContactNumber = "+20-122-000-1111",
                Address = "258 5th Settlement, Cairo, Egypt",
                HireDate = new DateTime(2023, 5, 1),
                DepartmentId = 6,
                DesignationId = 17, // Customer Support Representative
                BasicSalary = 14000.00m,
                EFF_Start = new DateTime(2023, 5, 1),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Employee
            {
                Id = 22,
                FirstName = "Ziad",
                LastName = "Hosny",
                Email = "ziad.hosny@company.com",
                DateOfBirth = new DateTime(1996, 6, 14),
                Gender = "Male",
                ContactNumber = "+20-155-222-3333",
                Address = "369 Maadi, Cairo, Egypt",
                HireDate = new DateTime(2024, 2, 1),
                DepartmentId = 6,
                DesignationId = 17, // Customer Support Representative
                BasicSalary = 13500.00m,
                EFF_Start = new DateTime(2024, 2, 1),
                ApplicationUserId = null,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
