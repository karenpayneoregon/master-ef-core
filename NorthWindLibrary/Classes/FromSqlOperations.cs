using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthWindLibrary.Data;
using NorthWindLibrary.Models;

namespace NorthWindLibrary.Classes
{
    /// <summary>
    /// Several super simple examples for reading data via SQL statements
    /// </summary>
    public class FromSqlOperations
    {
        public static void CountriesViaSql()
        {
            using var context = new NorthwindContext();

            List<Countries> countriesList = context.Countries
                .FromSqlRaw("SELECT CountryIdentifier, Name FROM dbo.Countries")
                .ToList();

        }
        /// <summary>
        /// Note with parameters they are parameterized before sent to the server
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns>List of customer from specified country</returns>
        public static List<Customers> CustomersWhereSql(int countryId)
        {
            using var context = new NorthwindContext();

            return context.Customers
                .FromSqlInterpolated($"SELECT * FROM Customers WHERE  (CountryIdentifier = {countryId})")
                .Include(customer => customer.Contact)
                .Include(customer => customer.ContactTypeIdentifierNavigation)
                .ToList();

        }
    }
}
