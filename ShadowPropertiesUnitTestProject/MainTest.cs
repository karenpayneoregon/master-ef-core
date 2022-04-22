using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using ShadowProperties.Classes;
using ShadowPropertiesUnitTestProject.Base;

namespace ShadowPropertiesUnitTestProject
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        /// <summary>
        /// Quick example for soft-delete using shadow properties
        /// - Create database
        /// - Populate Contact1 table
        /// - Soft delete two records
        /// - Verify
        ///
        /// Notes
        ///
        /// In the DbContext
        /// - There is a HasQueryFilter on isDeleted
        /// - SaveChanges is overridden to set created and last updated properties
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [TestTraits(Trait.ShadowProperties)]
        public async Task CreateDatabaseAndPopulateContact1TableTest()
        {
            // create 10 records
            int expected = 10;
            // records to soft delete by primary key
            int[] identifiers = { 3, 5 };

            // create database
            var (success, exception) = await CreateOperations.NewDatabase();
            // verify creation was successful
            Check.That(exception).IsNull();
            // populate Contact1 table
            int count = BogusOperations.PopulateContact1Table(expected);
            // assert there are two less records
            Check.That(count).Equals(expected);

            var currentCount = BogusOperations.RemoveSomeRecords(identifiers).Count;
            Check.That(currentCount).Equals(expected - 2);

            var noFilterCount = BogusOperations.WithNoFilter().Count;
            Check.That(noFilterCount).Equals(expected);


        }
        [TestMethod]
        [TestTraits(Trait.PlaceHolder)]
        public void TestMethod2()
        {
            // TODO
        }
    }
}
