using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Reviews.GetApartmentReviews;

public record GetApartmentReviewsQuery(Guid ApartmentId) : IQuery<IReadOnlyCollection<ApartmentReviewResponse>>;