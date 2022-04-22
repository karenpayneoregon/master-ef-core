using System;
using System.Linq;
using HasFilterLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using Oed.EntityFrameworkCoreHelpers.LanguageExtensions;
using QueryFilterTestProject.Base;


namespace QueryFilterTestProject
{

    [TestClass]
    public partial class HasFilterTests : TestBase
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

        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public void IgnoreQueryFilter()
        {
            // arrange
            var expected = 98;

            using var context = new NorthwindContext();

            // act
            var customers = context
                .Customers
                .Include(cust => cust.CountryIdentfierNavigation)
                /*
                 * This bypasses any query filter set
                 */
                .IgnoreQueryFilters()
                .ToList();

            // assert
            Check.That(customers.Count).Equals(expected);

        }

        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public void QueryFilter()
        {
            // arrange
            var expected = 11;

            using var context = new NorthwindContext();

            // act
            var customers = context
                .Customers
                .Include(cust => cust.CountryIdentfierNavigation)
                .ToList();

            // assert
            Check.That(customers.Count).Equals(expected);
            
        }

    }

}