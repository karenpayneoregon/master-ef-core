using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BaseUnitTestProject.Base;
using BaseUnitTestProject.Classes;
using BaseUnitTestProject.LanguageExtensions;
using BaseUnitTestProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using NorthWindLibrary.Classes;
using NorthWindLibrary.Data;
using NorthWindLibrary.Models;
using NorthWindLibrary.Projections;
using Oed.EntityFrameworkCoreHelpers.Classes;
using Oed.EntityFrameworkCoreHelpers.LanguageExtensions;

namespace BaseUnitTestProject
{
    /// <summary>
    /// Code samples
    /// </summary>
    [TestClass]
    public partial class NotTestsButCodeSamples : TestBase
    {

        /// <summary>
        /// Demo for obtaining order details for an order.
        /// Only includes the order, nothing for products
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public void SingleOrderDetailsWithoutNavigationTest()
        {
            int orderId = 10248;
            using var context = new NorthwindContext();

            OrderDetails details = context.OrderDetails
                .Include(ord => ord.Order)
                .FirstOrDefault(ord => ord.OrderId == orderId);

            
        }

        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public void SingleOrderDetailsHasNavigationTest()
        {
            string DumpQuery(NorthwindContext context, Orders orders)
            {
                return context.OrderDetails
                    .Include(ordd => ordd.Product)
                    .Include(ordd => ordd.Product.Category)
                    .Include(ordd => ordd.Product.Supplier)
                    .Where(ord => ord.OrderId == orders.OrderId).ToQueryString();
            }

            // arrange
            int expected = 3;
            int orderId = 10248;

            using var context = new NorthwindContext();

            // act
            Orders order = context.Orders
                .Include(ord => ord.Employee)
                .Include(ord => ord.CustomerIdentifierNavigation)
                .Include(ord => ord.ShipViaNavigation)
                .FirstOrDefault(ord => ord.OrderId == orderId);


            List<OrderDetails> details = context.OrderDetails
                .Include(ordd => ordd.Product)
                .Include(ordd => ordd.Product.Category)
                .Include(ordd => ordd.Product.Supplier)
                .Where(ord => ord.OrderId == order.OrderId).ToList();

            // assert
            Check.That(details.Select(orderDetail => orderDetail.Product).Count())
                .Equals(expected);


            Console.WriteLine(DumpQuery(context, order));


        }


        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public async Task ToQueryStringProjection()
        {
            await using var context = new NorthwindContext();

            var result = context.Customers.Select(CustomerItem.Projection).ToQueryString();
            Console.WriteLine(result);

        }

        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public async Task IQueryableBetweenCustomersTest()
        {
            // arrange
            int startValue = 12;
            int endValue = 24;
            string countryName = "Germany";

            // act
            await using var context = new NorthwindContext();

            List<CustomerCountry> customerList = context.
                Customers
                .Include(country => country.CountryIdentifierNavigation)
                /*
                 * Between is the focus
                 */
                .Between(cust => cust.CustomerIdentifier, startValue, endValue)
                .Select(cust => new CustomerCountry()
                {
                    Id = cust.CustomerIdentifier,
                    Name = cust.CompanyName,
                    Country = cust.CountryIdentifierNavigation.Name
                })
                .Where(item => item.Country == countryName)
                .ToList();

            // assert
            Assert.IsTrue(customerList.SequenceEqual(_customerCountries(), new CustomerCountryEqualityComparer()));

        }

        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public void EditInspectOriginalAndCurrentValue()
        {
            using var context = new NorthwindContext();
            int categoryIdentifier = 2;

            List<Products> products = context
                .Products
                .Include(product => product.OrderDetails)
                .Include(product => product.Supplier)
                .Include(product => product.Category)
                .Where(product => product.CategoryId == categoryIdentifier)
                .Take(3)
                .ToList();

            Products firstProduct = products.FirstOrDefault();
            Debug.WriteLine($"First product name: {firstProduct.ProductName,-15} Current state: {context.Entry(firstProduct).State}");


            firstProduct.ProductName = "ABC";
            Debug.WriteLine($"First product name: {firstProduct.ProductName,-15} Current state: {context.Entry(firstProduct).State}");
            //context.ChangeTracker.DetectChanges();

            Debug.WriteLine("");

            Debug.WriteLine($"Remove: {context.BusinessEntityPhone.Remove(context.BusinessEntityPhone.Find(1))}");

            string[] tokens = { "ProductName", "ProductId" };

            File.WriteAllText(_InspectFileName1, context.ChangeTracker.DebugView.CustomViewByChunks(tokens, 10));
            File.WriteAllText(_InspectFileName2, context.ChangeTracker.DebugView.LongView);

            //Console.WriteLine(context.ChangeTracker.DebugView.CustomView(new[] { "Products" }, 2));
            
            var originalProductName = context.Entry(firstProduct).Property(x => x.ProductName).OriginalValue;

            var currentProductName = context.Entry(firstProduct).Property(x => x.ProductName)
                .CurrentValue;

            Debug.WriteLine($"original name: '{originalProductName}' current: '{currentProductName}'");

        }

        /// <summary>
        /// Demonstrates using ToQueryString to get a string representation of the query used
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public void ToQueryString()
        {
            // arrange
            var expected = @"SELECT [c].[CategoryID], [c].[CategoryName], [c].[Description], [c].[Picture]
FROM [Categories] AS [c]";

            using var context = new NorthwindContext();

            // act
            var categoryQueryString = context.Categories.ToQueryString();

            Debug.WriteLine(categoryQueryString);

            // assert
            Assert.AreEqual(expected, categoryQueryString);

            Debug.WriteLine("");

            int categoryIdentifier = 2;

            var productsQueryString = context
                .Products
                .Include(product => product.OrderDetails)
                .Include(product => product.Supplier)
                .Include(product => product.Category)
                .Where(product => product.CategoryId == categoryIdentifier).ToQueryString();

            Debug.WriteLine(productsQueryString);

            Assert.IsTrue(productsQueryString.Contains("DECLARE @__categoryIdentifier_0 int = 2;"));

        }

        [TestMethod]
        [TestTraits(Trait.PlaceHolder)]
        public void SqlCodeSample()
        {
            // arrange
            int franceId = 8;

            // act
            var customersList = FromSqlOperations.CustomersWhereSql(franceId);

            // assert
            Check.That(customersList.Count).Equals(10);
        }


    }

}