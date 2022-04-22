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
using BaseUnitTestProject.Models;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using NorthWindLibrary.Classes;
using NorthWindLibrary.Models;

namespace BaseUnitTestProject
{
    /// <summary>
    /// Focused test for FluentValidation validators
    /// </summary>
    [TestClass]
    public partial class FluentValidationTest : TestBase
    {

        /// <summary>
        /// Rules
        /// - Must have a company name
        /// - Must have a country identifier
        /// In this test both of the above are false
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.FluentValidation)]
        public void NoCompanyNameNoCountryNameTest()
        {
            // arrange
            string[] expected =
            {
                "'Company Name' must not be empty.", 
                "'Country Identifier' must not be empty."
            };

            Customers customer = new Customers();
            CustomerValidator validator = new ();

            // act
            ValidationResult result = validator.Validate(customer);
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToArray();

            // assert
            CollectionAssert.AreEqual(expected, errorMessages);

        }

        /// <summary>
        /// Rules
        /// - Must have a company name
        /// - Must have a country identifier
        /// Here both are supplied
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.FluentValidation)]
        public void HasCompanyNameHasCountryNameTest()
        {
            // arrange
            Customers customer = new Customers() {CompanyName = "ABC", CountryIdentifier = 8};
            CustomerValidator validator = new();

            // act
            ValidationResult result = validator.Validate(customer);
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToArray();

            // assert
            Check.That(errorMessages.Length).Equals(0);
        }

        [TestMethod]
        [TestTraits(Trait.FluentValidation)]
        public async Task OrderDetailsTest()
        {
            var order = await OrderOperations.GetOrder();
            OrderHasDetailsValidator validator = new();
            ValidationResult result = await validator.ValidateAsync(order);

            Check.That(result.Errors.Count).Equals(0);
        }

        [TestMethod]
        [TestTraits(Trait.FluentValidation)]
        public async Task OrderDetailsWithNoItemsTest()
        {
            // arrange
            Orders order = OrderNoDetails();
            string expected = "Must have one or more details";
            OrderHasDetailsValidator validator = new();

            // act
            ValidationResult result = await validator.ValidateAsync(order);

            // assert
            Check.That(
                result.Errors.Select(validationFailure => validationFailure.ErrorMessage))
                .Contains(expected);

        }

        [TestMethod]
        [TestTraits(Trait.FluentValidation)]
        public async Task ValidateSocialSecurityNumber_GoodTest()
        {
            Claim claim = new () { FirstName = "Dan", LastName = "Smith", SocialNumber = "205-57-1245", PIN = "4567"};
            ClaimValidator validator = new();
            ValidationResult result = await validator.ValidateAsync(claim);

            Check.That(result.Errors.Count).Equals(0);
        }

        [TestMethod]
        [TestTraits(Trait.FluentValidation)]
        public async Task ValidateSocialSecurityNumber_Invalid_Social_Test()
        {
            string expected = "The specified condition was not met for 'Social Number'.";
            Claim claim = new() { FirstName = "Dan", LastName = "Smith", SocialNumber = "205-57-12Q5", PIN = "4567" };
            ClaimValidator validator = new();
            ValidationResult result = await validator.ValidateAsync(claim);

            Check.That(result.Errors.Select(validationFailure => validationFailure.ErrorMessage)).Contains(expected);

        }
    }
}