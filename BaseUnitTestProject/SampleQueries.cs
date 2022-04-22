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
using HasFilterLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using NorthWindLibrary.Classes;
using Customers = NorthWindLibrary.Models.Customers;

namespace BaseUnitTestProject
{
    /// <summary>
    /// Each test method will have two asserts based on LINQ/Lambda
    /// using live data via methods in <seealso cref="SqlServerOperations"/>
    /// </summary>
    [TestClass]
    public partial class SampleQueries : TestBase
    {
 
        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public void SelectCustomerTest()
        {
            int identifier = 4;
            var customerData = SqlServerOperations.CustomerData(identifier);

            Customers customer1 = CustomerOperations.LinqByIdNoIncludes(identifier);

            Check.That(customer1.CompanyName).Equals(customerData.CompanyName);

            Customers customer2 = CustomerOperations.LambdaByIdNoIncludes(identifier);

            Check.That(customer2.CompanyName).Equals(customerData.CompanyName);
        }

        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public void SelectCustomerContactJoinTest()
        {
            int identifier = 4;
            var customerData = SqlServerOperations.CustomerData(identifier);

            var customer1 = CustomerOperations.LambdaByIdIncludeContacts(identifier);

            Check.That(customer1.Contact.LastName).Equals(customerData.LastName);

            var customer2 = CustomerOperations.LinqByIdIncludeContacts(identifier);
            Check.That(customer2.Contact.LastName).Equals(customerData.LastName);

        }

        [TestMethod]
        [TestTraits(Trait.EntityFramework)]
        public void SelectCustomerContactCountryJoinTest()
        {
            int identifier = 4;
            var customerData = SqlServerOperations.CustomerData(identifier);

            var customer1 = CustomerOperations.LinqByIdIncludeContactsAndCounty(identifier);

            Check.That(customer1.CountryIdentifierNavigation.Name).Equals(customerData.Country);

            var customer2 = CustomerOperations.LinqByIdIncludeContacts(identifier);
            Check.That(customer2.Contact.LastName).Equals(customerData.LastName);

        }

    }

}