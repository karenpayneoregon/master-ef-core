using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseUnitTestProject.LanguageExtensions;
using FluentValidation;

namespace BaseUnitTestProject.Models
{
    public class Human
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Claim : Human
    {
        public int Id { get; set; }
        public string SocialNumber { get; set; }
        public string PIN { get; set; }
    }


    public class ClaimValidator : AbstractValidator<Claim>
    {
        public ClaimValidator()
        {
            RuleFor(claim => claim.FirstName).NotNull();
            RuleFor(claim => claim.LastName).NotNull();
            Transform(from: claim => claim.SocialNumber, to: value => value.IsValidSsn()).Must(success => success);
            RuleFor(claim => claim.PIN).NotEmpty();
        }
    }
}
