using FluentValidation;
using QuickNotes.DTOs;

namespace QuickNotes.Validators
{
    public class NoteDtoValidator : AbstractValidator<NoteDto>
    {
        public NoteDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(x => x.Content)
                .MaximumLength(1000).WithMessage("Content cannot exceed 1000 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Content));
        }
    }
}
