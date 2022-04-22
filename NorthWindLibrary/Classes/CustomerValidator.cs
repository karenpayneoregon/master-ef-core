using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using NorthWindLibrary.Models;

namespace NorthWindLibrary.Classes
{
    /// <summary>
    /// Use to validate <see cref="Customers"/> rules
    /// </summary>
    public class CustomerValidator : AbstractValidator<Customers>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.CompanyName).NotNull();
            RuleFor(customer => customer.CountryIdentifier).NotEmpty();
        }
    }
}
