using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Dapper;

namespace Bookify.Application.Reviews.GetApartmentReviews;

internal sealed class GetApartmentReviewsQueryHandler : IQueryHandler<GetApartmentReviewsQuery, IReadOnlyCollection<ApartmentReviewResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetApartmentReviewsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyCollection<ApartmentReviewResponse>>> Handle(GetApartmentReviewsQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                id AS Id,
                apartment_id AS ApartmentId,
                user_id AS UserId,
                rating AS Rating,
                comment AS Comment,
                created_on_utc AS CreatedOnUtc
            FROM reviews
            WHERE apartment_id = @ApartmentId
            """;

        var apartments = await connection.QueryAsync<ApartmentReviewResponse>(
            sql,
            new
            {
                request.ApartmentId
            });

        return apartments.ToList();
    }
}