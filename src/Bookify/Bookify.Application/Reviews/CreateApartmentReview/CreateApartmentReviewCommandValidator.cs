using FluentValidation;

namespace Bookify.Application.Reviews.CreateApartmentReview;

public class CreateApartmentReviewCommandValidator : AbstractValidator<CreateApartmentReviewCommand>
{
    public CreateApartmentReviewCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();

        RuleFor(c => c.BookingId).NotEmpty();

        RuleFor(c => c.Rating).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5);
    }
}