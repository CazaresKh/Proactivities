using Application.Activities.Commands;
using FluentValidation;

namespace Application.Activities.Validators
{
    public class ActivityCommandValidator : AbstractValidator<ActivityCommand>
    {
        public ActivityCommandValidator()
        {
            RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
        }
    }
}