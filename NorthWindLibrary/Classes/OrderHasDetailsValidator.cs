using FluentValidation;
using NorthWindLibrary.Models;

namespace NorthWindLibrary.Classes
{
    /// <summary>
    /// Use to validate <see cref="Orders"/> rule that an order must
    /// have at least one <see cref="OrderDetails"/>
    /// </summary>
    public class OrderHasDetailsValidator : AbstractValidator<Orders>
    {
        public OrderHasDetailsValidator()
        {
            RuleFor(order => order.OrderDetails).Custom((orderDetails, context) => {
                if (orderDetails.Count == 0)
                {
                    context.AddFailure("Must have one or more details");
                }
            });
        }
    }
}