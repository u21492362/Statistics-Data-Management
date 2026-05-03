using Backend__SDM_.Entities;
using Backend__SDM_.Models.Enums;

namespace Backend__SDM_.Models.Data
{
    public static class StatisticalCategorySeed
    {
        public static List<StatisticalCategory> GetCategories()
        {
            return new List<StatisticalCategory>
            {
                new() { Code = "MEN_ABOVE_18", Name = "Men Above 18 Years", CategoryGroup = CategoryGroup.Demographic, IsSystemGenerated = true, DisplayOrder = 1 },
                new() { Code = "WOMEN_ABOVE_18", Name = "Women Above 18 Years", CategoryGroup = CategoryGroup.Demographic, IsSystemGenerated = true, DisplayOrder = 2 },
                new() { Code = "BOYS_6_18", Name = "Boys 6-18 Years", CategoryGroup = CategoryGroup.Demographic, IsSystemGenerated = true, DisplayOrder = 3 },
                new() { Code = "GIRLS_6_18", Name = "Girls 6-18 Years", CategoryGroup = CategoryGroup.Demographic, IsSystemGenerated = true, DisplayOrder = 4 },
                new() { Code = "INFANT_BOYS_UNDER_5", Name = "Infant Boys Under 5", CategoryGroup = CategoryGroup.Demographic, IsSystemGenerated = true, DisplayOrder = 5 },
                new() { Code = "INFANT_GIRLS_UNDER_5", Name = "Infant Girls Under 5", CategoryGroup = CategoryGroup.Demographic, IsSystemGenerated = true, DisplayOrder = 6 },

                new() { Code = "CHURCH_MEMBERSHIP", Name = "Church Membership", CategoryGroup = CategoryGroup.Membership, DisplayOrder = 7 },
                new() { Code = "PREACHER", Name = "Preacher", CategoryGroup = CategoryGroup.Leadership, DisplayOrder = 8 },
                new() { Code = "MCU", Name = "MCU", CategoryGroup = CategoryGroup.Organisation, DisplayOrder = 9 },
                new() { Code = "MENS_FELLOWSHIP", Name = "Men's Fellowship", CategoryGroup = CategoryGroup.Organisation, DisplayOrder = 10 },
                new() { Code = "RUWADZANO_MANYANO", Name = "Ruwadzano/Manyano", CategoryGroup = CategoryGroup.Organisation, DisplayOrder = 11 },
                new() { Code = "WOMENS_ASSOCIATION", Name = "Women's Association", CategoryGroup = CategoryGroup.Organisation, DisplayOrder = 12 },
                new() { Code = "WOMENS_FELLOWSHIP", Name = "Women's Fellowship", CategoryGroup = CategoryGroup.Organisation, DisplayOrder = 13 },
                new() { Code = "CLASS_MEMBER", Name = "Class Member", CategoryGroup = CategoryGroup.Membership, DisplayOrder = 14 },
                new() { Code = "BCU", Name = "BCU", CategoryGroup = CategoryGroup.Organisation, DisplayOrder = 15 },
                new() { Code = "GCU", Name = "GCU", CategoryGroup = CategoryGroup.Organisation, DisplayOrder = 16 },
                new() { Code = "JUNIOR_BCU", Name = "Junior BCU", CategoryGroup = CategoryGroup.Organisation, DisplayOrder = 17 },
                new() { Code = "TSUNGARE", Name = "Tsungare", CategoryGroup = CategoryGroup.Organisation, DisplayOrder = 18 },
                new() { Code = "MYD", Name = "MYD", CategoryGroup = CategoryGroup.Organisation, DisplayOrder = 19 },
                new() { Code = "YOUNG_ADULTS", Name = "Young Adults", CategoryGroup = CategoryGroup.Organisation, DisplayOrder = 20 },
                new() { Code = "SUNDAY_SCHOOL_TEACHER", Name = "Sunday School Teacher", CategoryGroup = CategoryGroup.Education, DisplayOrder = 21 },
                new() { Code = "SUNDAY_SCHOLAR", Name = "Sunday Scholar", CategoryGroup = CategoryGroup.Education, DisplayOrder = 22 }
            };
        }
    }
}
