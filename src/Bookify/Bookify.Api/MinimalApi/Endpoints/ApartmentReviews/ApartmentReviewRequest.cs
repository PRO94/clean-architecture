namespace Bookify.Api.MinimalApi.Endpoints.ApartmentReviews;

public sealed record ApartmentReviewRequest(
    Guid ApartmentId,
    Guid UserId,
    Guid BookingId,
    int Rating,
    string Comment);