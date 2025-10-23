using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class TicketValidator : AbstractValidator<Ticket>
    {
        public TicketValidator()
        {
            RuleFor(t => t.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(t => t.Title).MaximumLength(50).WithMessage("Title cannot exceed 100 characters.");
            RuleFor(t => t.Status).NotEmpty().WithMessage("Status is required.");
            RuleFor(t => t.Status).InclusiveBetween(0, 5).WithMessage("Status must be between 0 and 5.");
            RuleFor(t => t.Priority).NotEmpty().WithMessage("Priority is required.");
            RuleFor(t => t.Priority).InclusiveBetween(0, 3).WithMessage("Priority must be between 0 and 3.");
        }
    }
}
