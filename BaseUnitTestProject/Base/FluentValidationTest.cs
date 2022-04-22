using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWindLibrary.Models;
using NorthWindLibrary.Classes;

// ReSharper disable once CheckNamespace
namespace BaseUnitTestProject
{
    public partial class FluentValidationTest
    {
        [TestInitialize]
        public void Initialization()
        {

        }

        /// <summary>
        /// Perform any initialize for the class
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            TestResults = new List<TestContext>();
        }

        /// <summary>
        /// For <see cref="OrderDetailsWithNoItemsTest"/> to validate custom
        /// custom fluent validator <see cref="OrderHasDetailsValidator"/>
        /// </summary>
        /// <returns></returns>
        public static Orders OrderNoDetails() =>
            new Orders
            {
                OrderDetails = new List<OrderDetails>()
            };
    }

}