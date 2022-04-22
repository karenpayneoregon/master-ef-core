using BaseUnitTestProject.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BaseUnitTestProject.LanguageExtensions;
using NFluent;
using NorthWindLibrary.Classes;
using NorthWindLibrary.Data;
using NorthWindLibrary.Models;
using Oed.EntityFrameworkCoreHelpers.LanguageExtensions;
using NorthWindLibrary.Projections;
using Oed.EntityFrameworkCoreHelpers.Classes;


namespace BaseUnitTestProject
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        /// <summary>
        /// Validate connection to the database in appsettings.json
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public void ConnectToDatabaseTest()
        {
            // arrange

            using var context = new NorthwindContext();

            // act

            var (success, _) = context.CanConnect();

            // assert
            Check.That(success).IsTrue();
        }

        /// <summary>
        /// Validate <seealso cref="CustomerItem.Projection"/> which is an oddity
        /// in that no equal assertion will work without mocking <seealso cref="CustomerItem.Projection"/>
        /// which is much simpler with a mocking framework.
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public async Task CustomersProjection()
        {

            // arrange
            int customerIdentifier = 2;
            CustomerItem expected = JsonSerializer.Deserialize<CustomerItem>(await File.ReadAllTextAsync("CustomerItem.json"));

            // act
            var customers = await CustomerOperations.GetCustomersWithProjection();

            CustomerItem customer = customers.FirstOrDefault(cust => cust.CustomerIdentifier == customerIdentifier);

            // assert - differences
            List<Variance> differenceCompare = customer.DetailedCompare(expected);
            Assert.IsTrue(differenceCompare.FirstOrDefault().PropertyName == "Projection");

        }

        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public async Task CustomersByCountryTest()
        {
            // arrange 
            int identifier = 12;
            int expected = 6;

            // act
            var customers = await CustomerOperations.ByCountry(identifier);

            // assert
            Check.That(customers.Count).Equals(expected);

        }

        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public async Task ContactsByContactTypeTestAsync()
        {
            // arrange 
            int owner = 7;
            int salesRepresentative = 12;
            List<int> contactTypes = new() { owner, salesRepresentative };

            // act
            var contacts = await ContactOperations.ByType(contactTypes);

            // assert
            Check.That(contacts.Count).Equals(33);

        }

        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public async Task GroupByEmployeeIdentifierGetHighCountInOrders()
        {

            IGrouping<int, Employees> employee = await EmployeeOperations.HighCountInOrders();

            Check.That(employee!.Count()).Equals(156);
            Check.That(employee.Key).Equals(4);

        }

        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public async Task FindCustomerByPrimaryKey()
        {
            // arrange
            int key = 60;
            string expected = "Wolski  Zajazd";

            // act
            await using var context = new NorthwindContext();
            var customer = await context.Customers.FindAsync(key);

            // assert
            Assert.AreEqual(expected, customer.CompanyName);

        }

        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public async Task FindCustomerByPrimaryKeyNotFound()
        {
            // arrange
            int key = 99;

            // act
            await using var context = new NorthwindContext();
            var customer = await context.Customers.FindAsync(key);

            // assert
            Assert.IsNull(customer);

        }

        /// <summary>
        /// EF Core permits finding by key via DbSet&lt;TEntity&gt;.Find(object[]) but not by
        /// multiple keys which FindAllAsync does
        /// https://social.technet.microsoft.com/wiki/contents/articles/53841.entity-framework-core-find-all-by-primary-key-c.aspx
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public async Task FindAllCustomersByPrimaryKeyTest()
        {
            // arrange
            string[] expected = { "Bon app'", "QUICK Stop", "Wolski  Zajazd" };
            var primaryKeys = new[] { 9, 42, 60 };

            // act
            await using var context = new NorthwindContext();
            Customers[] results = await context.FindAllAsync<Customers>(primaryKeys.ToObjectArray());
            string[] names = results.Select(customer => customer.CompanyName).ToArray();

            // assert
            CollectionAssert.AreEqual(expected, names);
        }


        [TestMethod]
        [TestTraits(Trait.Conversions)]
        public void IntLengthTest()
        {
            // arrange
            int value = 102345;
            int expected = 6;

            // act
            var result = value.GetLength();
            var result1 = value.GetLengthConventional();

            // assert
            Check.That(result).Equals(expected);
        }

    }
}
