using Application.Comments.Commands;
using FluentValidation;

namespace Application.Comments.Validators
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.Body).NotEmpty();
        }
    }
}