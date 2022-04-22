using System;
using System.Linq;
using WorkingWithDates.Models;

namespace WorkingWithDates.LanguageExtensions
{
    public static class DateExtensionHelpers
    {

        public static IQueryable<Events> BetweenStartDate(this IQueryable<Events> events, DateTime startDate, DateTime endDate) 
            => events.Where(@event 
                => startDate <= @event.StartDate && @event.StartDate <= endDate);

        public static IQueryable<Events> BetweenEndDate(this IQueryable<Events> events, DateTime startDate, DateTime endDate)
            => events.Where(@event 
                => startDate <= @event.EndDate && @event.EndDate <= endDate);

        public static IQueryable<Birthdays> BirthDatesBetween(this IQueryable<Birthdays> events, DateTime startDate, DateTime endDate)
            => events.Where(@event
                => startDate <= @event.BirthDate && @event.BirthDate <= endDate);

    }
}
