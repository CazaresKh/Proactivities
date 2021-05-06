using Application.Profiles.Commands;
using FluentValidation;

namespace Application.Profiles.Validators
{
    public class ProfileValidator : AbstractValidator<UpdateProfileCommand>

    {
        public ProfileValidator()
        {
            RuleFor(x => x.DisplayName).NotEmpty();
        }
    }
}