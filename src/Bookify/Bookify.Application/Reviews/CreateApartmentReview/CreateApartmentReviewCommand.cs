using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Reviews.CreateApartmentReview;

public record CreateApartmentReviewCommand(
    Guid UserId,
    Guid BookingId,
    int Rating,
    string Comment) : ICommand<Guid>;