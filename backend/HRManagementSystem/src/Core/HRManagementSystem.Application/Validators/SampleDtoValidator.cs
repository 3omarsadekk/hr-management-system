using FluentValidation;
using HRManagementSystem.Application.DTOs;

namespace HRManagementSystem.Application.Validators;

/// <summary>
/// Sample validator - Replace with your actual validators
/// </summary>
public class SampleDtoValidator : AbstractValidator<SampleDto>
{
    public SampleDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty);
    }
}
