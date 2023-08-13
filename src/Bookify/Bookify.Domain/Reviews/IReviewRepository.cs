namespace Bookify.Domain.Reviews;

public interface IReviewRepository
{
    Task<Review?> GetByIdAsync(ReviewId id, CancellationToken cancellationToken = default);

    void Add(Review review);
}