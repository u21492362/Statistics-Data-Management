using Backend__SDM_.Entities;
using Backend__SDM_.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Backend__SDM_.Models.Data
{
    public class DbInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            await context.Database.MigrateAsync();

            if (!await context.Districts.AnyAsync())
            {
                var district = new District
                {
                    Name = "Harare West District",
                    Code = "HWD"
                };

                context.Districts.Add(district);
                await context.SaveChangesAsync();

                var circuit = new Circuit
                {
                    Name = "Kuwadzana Circuit",
                    Code = "KWC",
                    DistrictId = district.Id
                };

                context.Circuits.Add(circuit);
                await context.SaveChangesAsync();

                var societies = new List<Society>
                {
                    new Society { Name = "Kuwadzana", Code = "KUW", CircuitId = circuit.Id, IsActive = true },
                    new Society { Name = "Kuwadzana West", Code = "KUW-W", CircuitId = circuit.Id, IsActive = true },
                    new Society { Name = "Crowborough", Code = "CRB", CircuitId = circuit.Id, IsActive = true }
                };

                context.Societies.AddRange(societies);
                await context.SaveChangesAsync();
            }

            if (!await context.StatisticalYears.AnyAsync())
            {
                var currentYear = DateTime.UtcNow.Year;
                var years = new List<StatisticalYear>();

                for (var year = currentYear - 2; year <= currentYear + 3; year++)
                {
                    years.Add(new StatisticalYear
                    {
                        Year = year,
                        IsOpen = year == currentYear,
                        IsClosed = false
                    });
                }

                context.StatisticalYears.AddRange(years);
                await context.SaveChangesAsync();
            }

            if (!await context.StatisticalCategories.AnyAsync())
            {
                var categories = new List<StatisticalCategory>
                {
                    new() { Code = "MEN_ABOVE_18", Name = "Men Above 18 Years", CategoryGroup = CategoryGroup.Demographic, IsBoolean = true, IsSystemGenerated = true, DisplayOrder = 1 },
                    new() { Code = "WOMEN_ABOVE_18", Name = "Women Above 18 Years", CategoryGroup = CategoryGroup.Demographic, IsBoolean = true, IsSystemGenerated = true, DisplayOrder = 2 },
                    new() { Code = "BOYS_6_18", Name = "Boys 6-18 Years", CategoryGroup = CategoryGroup.Demographic, IsBoolean = true, IsSystemGenerated = true, DisplayOrder = 3 },
                    new() { Code = "GIRLS_6_18", Name = "Girls 6-18 Years", CategoryGroup = CategoryGroup.Demographic, IsBoolean = true, IsSystemGenerated = true, DisplayOrder = 4 },
                    new() { Code = "INFANT_BOYS_UNDER_5", Name = "Infant Boys Under 5", CategoryGroup = CategoryGroup.Demographic, IsBoolean = true, IsSystemGenerated = true, DisplayOrder = 5 },
                    new() { Code = "INFANT_GIRLS_UNDER_5", Name = "Infant Girls Under 5", CategoryGroup = CategoryGroup.Demographic, IsBoolean = true, IsSystemGenerated = true, DisplayOrder = 6 },

                    new() { Code = "CHURCH_MEMBERSHIP", Name = "Church Membership", CategoryGroup = CategoryGroup.Membership, IsBoolean = true, IsSystemGenerated = false, DisplayOrder = 7 },
                    new() { Code = "PREACHER", Name = "Preacher", CategoryGroup = CategoryGroup.Leadership, IsBoolean = true, IsSystemGenerated = false, DisplayOrder = 8 },
                    new() { Code = "MCU", Name = "MCU", CategoryGroup = CategoryGroup.Organisation, IsBoolean = true, IsSystemGenerated = false, DisplayOrder = 9 },
                    new() { Code = "MENS_FELLOWSHIP", Name = "Men's Fellowship", CategoryGroup = CategoryGroup.Organisation, IsBoolean = true, IsSystemGenerated = false, DisplayOrder = 10 },
                    new() { Code = "RUWADZANO_MANYANO", Name = "Ruwadzano / Manyano", CategoryGroup = CategoryGroup.Organisation, IsBoolean = true, IsSystemGenerated = false, DisplayOrder = 11 },
                    new() { Code = "WOMENS_ASSOCIATION", Name = "Women's Association", CategoryGroup = CategoryGroup.Organisation, IsBoolean = true, IsSystemGenerated = false, DisplayOrder = 12 },
                    new() { Code = "WOMENS_FELLOWSHIP", Name = "Women's Fellowship", CategoryGroup = CategoryGroup.Organisation, IsBoolean = true, IsSystemGenerated = false, DisplayOrder = 13 },
                    new() { Code = "CLASS_MEMBER", Name = "Class Member", CategoryGroup = CategoryGroup.Membership, IsBoolean = true, IsSystemGenerated = false, DisplayOrder = 14 },
                    new() { Code = "BCU", Name = "BCU", CategoryGroup = CategoryGroup.Organisation, IsBoolean = true, IsSystemGenerated = false, DisplayOrder = 15 },
                    new() { Code = "GCU", Name = "GCU", CategoryGroup = CategoryGroup.Organisation, IsBoolean = true, IsSystemGenerated = false, DisplayOrder = 16 },
                    new() { Code = "JUNIOR_BCU", Name = "Junior BCU", CategoryGroup = CategoryGroup.Organisation, IsBoolean = true, IsSystemGenerated = false, DisplayOrder = 17 },
                    new() { Code = "TSUNGARE", Name = "Tsungare", CategoryGroup = CategoryGroup.Organisation, IsBoolean = true, IsSystemGenerated = false, DisplayOrder = 18 },
                    new() { Code = "MYD", Name = "MYD", CategoryGroup = CategoryGroup.Organisation, IsBoolean = true, IsSystemGenerated = false, DisplayOrder = 19 },
                    new() { Code = "YOUNG_ADULTS", Name = "Young Adults", CategoryGroup = CategoryGroup.Organisation, IsBoolean = true, IsSystemGenerated = false, DisplayOrder = 20 },
                    new() { Code = "SUNDAY_SCHOOL_TEACHER", Name = "Sunday School Teacher", CategoryGroup = CategoryGroup.Education, IsBoolean = true, IsSystemGenerated = false, DisplayOrder = 21 },
                    new() { Code = "SUNDAY_SCHOLAR", Name = "Sunday Scholar", CategoryGroup = CategoryGroup.Education, IsBoolean = true, IsSystemGenerated = false, DisplayOrder = 22 }
                };

                context.StatisticalCategories.AddRange(categories);
                await context.SaveChangesAsync();
            }

            if (!await context.AppUsers.AnyAsync())
            {
                var circuit = await context.Circuits.FirstAsync();
                var societyKuwadzana = await context.Societies.FirstAsync(x => x.Name == "Kuwadzana");
                var societyWest = await context.Societies.FirstAsync(x => x.Name == "Kuwadzana West");
                var societyCrowborough = await context.Societies.FirstAsync(x => x.Name == "Crowborough");

                var users = new List<AppUser>
                {
                    new()
                    {
                        FullName = "System Administrator",
                        Email = "admin@methodiststats.local",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                        Role = UserRole.SystemAdministrator,
                        IsActive = true
                    },
                    new()
                    {
                        FullName = "Circuit Administrator",
                        Email = "circuitadmin@methodiststats.local",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                        Role = UserRole.CircuitAdministrator,
                        CircuitId = circuit.Id,
                        IsActive = true
                    },
                    new()
                    {
                        FullName = "Kuwadzana Society Administrator",
                        Email = "kuwadzana@methodiststats.local",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                        Role = UserRole.SocietyAdministrator,
                        CircuitId = circuit.Id,
                        SocietyId = societyKuwadzana.Id,
                        IsActive = true
                    },
                    new()
                    {
                        FullName = "Kuwadzana West Society Administrator",
                        Email = "kuwadzanawest@methodiststats.local",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                        Role = UserRole.SocietyAdministrator,
                        CircuitId = circuit.Id,
                        SocietyId = societyWest.Id,
                        IsActive = true
                    },
                    new()
                    {
                        FullName = "Crowborough Society Administrator",
                        Email = "crowborough@methodiststats.local",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                        Role = UserRole.SocietyAdministrator,
                        CircuitId = circuit.Id,
                        SocietyId = societyCrowborough.Id,
                        IsActive = true
                    }
                };

                context.AppUsers.AddRange(users);
                await context.SaveChangesAsync();
            }
        }
    }
}
