using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkingWithDates.Data;
using WorkingWithDates.LanguageExtensions;
using WorkingWithDates.Models;

namespace WorkingWithDates.Classes
{
    public class DataOperations
    {
        /// <summary>
        /// For demo this is okay, not for real apps
        /// </summary>
        public static Context Context = new();

        public static async Task<List<Person>> People() 
            => await Task.Run(async () => await Context.Person.ToListAsync());

        /// <summary>
        /// Demonstration showing how to perform a BETWEEN on dates
        /// with an additional condition on EventId
        /// </summary>
        public static List<Events> BetweenEventDates(DateTime startDate, DateTime endDate, int identifier) 
            => Context.Events
                .BetweenStartDate(startDate, endDate).Where(@event => @event.EventId == identifier)
                .ToList();


        /// <summary>
        /// Return a list of birthday records with a specific date range on <see cref="Birthdays.BirthDate"/>
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static Task<List<Birthdays>> GetBirthdaysList(DateTime startDate, DateTime endDate) 
            => Task.Run(async () 
                => await Context.Birthdays.BirthDatesBetween(startDate, endDate)
                    .OrderBy(birthday => birthday.BirthDate).ToListAsync());

        /// <summary>
        /// Load people into a BindingList with no where conditions
        /// </summary>
        /// <returns></returns>
        public static async Task<BindingList<Person>> PeopleLocal() 
            => await Task.Run(async () =>
            {
                await Context.Person.LoadAsync();
                return Context.Person.Local.ToBindingList();
            });

        /// <summary>
        /// Get changes into a string if there are changes. Will not show deleted records
        /// </summary>
        /// <returns></returns>
        public static List<ChangeContainer> ShowEventsChangesPlain()
        {

            List<ChangeContainer> changedList = new();

            /*
             * Note that Person is alias for Person1 done in the DbContext
             */
            foreach (Person currentPerson in Context.Person.Local)
            {
                if (Context.Entry(currentPerson).State != EntityState.Unchanged)
                {
                    ChangeContainer item = new ()
                    {

                        Id = currentPerson.Id,

                        CurrentFirstName = currentPerson.FirstName,
                        OriginalFirstName = Context.Entry(currentPerson).Property(person => person.FirstName).OriginalValue,

                        CurrentLastName = currentPerson.LastName,
                        OriginalLastName = Context.Entry(currentPerson).Property(person => person.LastName).OriginalValue,

                        CurrentBirthDate = currentPerson.BirthDate,
                        OriginalBirthDate = Context.Entry(currentPerson).Property(person => person.BirthDate).OriginalValue,

                        EntityState = Context.Entry(currentPerson).State
                    };

                    changedList.Add(item);
                }
            }

            return changedList;

        }
    }
}
