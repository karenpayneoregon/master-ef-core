
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthWindLibrary.Data;
using NorthWindLibrary.Models;
using NorthWindLibrary.Projections;

namespace NorthWindLibrary.Classes
{
    public class CustomerOperations
    {
        /// <summary>
        /// An example for obtaining a narrow view of a Customer via <see cref="CustomerItem.Projection"/>
        /// </summary>
        /// <returns></returns>
        public static async Task<List<CustomerItem>> GetCustomersWithProjection()
        {

            return await Task.Run(async () =>
            {
                await using var context = new NorthwindContext();
                return await context.Customers
                    .Select(CustomerItem.Projection)
                    .ToListAsync();
            });
        }

        /// <summary>
        /// Get customers by country
        /// </summary>
        /// <param name="countryIdentifier">country id</param>
        /// <returns>All customers in <seealso cref="countryIdentifier"/></returns>
        public static async Task<List<Customers>> ByCountry(int countryIdentifier)
        {
            return await Task.Run(async () =>
            {
                await using var context = new NorthwindContext();
                return await context.Customers
                    .Where(customer => customer.CountryIdentifier == countryIdentifier)
                    .Select(customer => customer)
                    .ToListAsync();
            });
        }

        #region Identical queries without joins, one LINQ, one Lambda

        /// <summary>
        /// Get Customer by id without any navigation properties populated via Lambda
        /// </summary>
        /// <param name="id">customer key</param>
        /// <returns>customer by id</returns>
        public static Customers LambdaByIdNoIncludes(int id)
        {
            using var context = new NorthwindContext();

            return context.Customers.FirstOrDefault(customer => customer.CustomerIdentifier == id);
        }

        /// <summary>
        /// Get Customer by id without any navigation properties populated via LINQ
        /// </summary>
        /// <param name="id">customer key</param>
        /// <returns>customer by id</returns>
        public static Customers LinqByIdNoIncludes(int id)
        {
            using var context = new NorthwindContext();

            return (
                from customer in context.Customers 
                select customer).FirstOrDefault(customerItem => customerItem.CustomerIdentifier == id);
        }
        #endregion

        #region Identical queries with one join, one LINQ, one Lambda

        /// <summary>
        /// Get Customer by id using LINQ, including contacts
        /// </summary>
        /// <param name="id">customer key</param>
        /// <returns>customer by id</returns>
        public static Customers LinqByIdIncludeContacts(int id)
        {
            using var context = new NorthwindContext();

            return (
                from customer in context.Customers.Include(cust => cust.Contact)
                join contact in context.Contacts on customer.ContactId equals contact.ContactId
                select customer)
                .FirstOrDefault(cust => cust.CustomerIdentifier == id);
        }

        /// <summary>
        /// Get Customer by id using LINQ, includes contact and country navigation
        /// </summary>
        /// <param name="id">customer key</param>
        /// <returns>customer by id</returns>
        public static Customers LinqByIdIncludeContactsAndCounty(int id)
        {
            using var context = new NorthwindContext();

            return (
                from customer in context.Customers
                    .Include(cust => cust.Contact)
                    .Include(cust => cust.CountryIdentifierNavigation)
                join contact in context.Contacts on customer.ContactId equals contact.ContactId
                join country in context.Countries on customer.CountryIdentifier equals country.CountryIdentifier 
                select customer)
                .FirstOrDefault(cust => cust.CustomerIdentifier == id);
        }

        /// <summary>
        /// Get Customer by id using Lambda, including contacts
        /// </summary>
        /// <param name="id">customer key</param>
        /// <returns>customer by id</returns>
        public static Customers LambdaByIdIncludeContacts(int id)
        {
            using var context = new NorthwindContext();

            return context
                .Customers
                .Include(customer => customer.Contact)
                .FirstOrDefault(customer => customer.CustomerIdentifier == id);
        }

        #endregion

        public static async Task<List<CustomerItem>> GetCustomersAsync()
        {

            var currentExecutable = Process.GetCurrentProcess().MainModule.FileName;

            return await Task.Run(async () =>
            {
                await using var context = new NorthwindContext();
                return await context.Customers.AsNoTracking()
                    .Include(customer => customer.Contact)
                    .ThenInclude(contact => contact.ContactDevices)
                    .ThenInclude(contactDevices => contactDevices.PhoneTypeIdentifierNavigation)
                    .Include(customer => customer.ContactTypeIdentifierNavigation)
                    .Include(customer => customer.CountryIdentifierNavigation)
                    .Select(customer => new CustomerItem()
                    {
                        CustomerIdentifier = customer.CustomerIdentifier,
                        CompanyName = customer.CompanyName,
                        ContactId = customer.Contact.ContactId,
                        Street = customer.Street,
                        City = customer.City,
                        PostalCode = customer.PostalCode,
                        CountryIdentifier = customer.CountryIdentifier,
                        Phone = customer.Phone,
                        ContactTypeIdentifier = customer.ContactTypeIdentifier,
                        Country = customer.CountryIdentifierNavigation.Name,
                        FirstName = customer.Contact.FirstName,
                        LastName = customer.Contact.LastName,
                        ContactTitle = customer.ContactTypeIdentifierNavigation.ContactTitle,
                        OfficePhoneNumber = customer.Contact.ContactDevices.FirstOrDefault(contactDevices => 
                            contactDevices.PhoneTypeIdentifier == 3).PhoneNumber
                    })
                    .TagWith($"App name: {currentExecutable}")
                    .TagWith($"From: {nameof(CustomerOperations)}.{nameof(GetCustomersAsync)}")
                    .TagWith("Parameters: None")
                    .ToListAsync();
            });
        }

    }
}
