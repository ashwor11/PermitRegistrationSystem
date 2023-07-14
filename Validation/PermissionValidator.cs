using Domain.Entities;
using FluentValidation;

namespace PermitRegistrationSystem.Validation;

public class PermissionValidator : AbstractValidator<Permission>
{
    public PermissionValidator()
    {
        RuleFor(p => p.StartDate)
            .LessThan(p => p.EndDate)
            .WithMessage("Start date cannot be greater than end date.");

        RuleFor(p => p.EndDate)
            .GreaterThan(p => p.StartDate)
            .WithMessage("End date cannot be smaller than start date.");
    }
}