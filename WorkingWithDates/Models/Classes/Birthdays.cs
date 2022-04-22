using System;
using WorkingWithDates.LanguageExtensions;

namespace WorkingWithDates.Models
{
    public partial class Birthdays
    {
        public string[] ItemArray => new[]
        {
            FirstName,
            LastName,
            BirthDate?.ToString("yyyy-MM-dd"),
            Age
        };
        public string Age
            => BirthDate?.Age(DateTime.Now).YearsMonthsDays;

        public string FullName => $"{FirstName} {LastName}";
    }
}
