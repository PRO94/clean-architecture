namespace Bookify.Application.Reviews.GetApartmentReviews;

public class ApartmentReviewResponse
{
    public Guid Id { get; init; }

    public Guid ApartmentId { get; init; }

    public Guid UserId { get; init; }

    public int Rating { get; init; }

    public string Comment { get; init; }

    public DateTime CreatedOnUtc { get; init; }
}