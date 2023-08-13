using Bookify.Application.Reviews.CreateApartmentReview;
using Bookify.Application.Reviews.GetApartmentReviews;
using Bookify.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Bookify.Api.MinimalApi.Endpoints.ApartmentReviews;

public static class ApartmentReviewsEndpoints
{
    public static IEndpointRouteBuilder MapReviewsEndpoints(this IEndpointRouteBuilder builder)
    {
        var routeGroupBuilder = builder.MapGroup("api/reviews");

        routeGroupBuilder.MapGet("{apartmentId}", GetApartmentReviews)
            .WithName(nameof(GetApartmentReviews));

        routeGroupBuilder.MapPost("", CreateApartmentReview)
            .RequireAuthorization();

        return builder;
    }

    public static async Task<Results<Ok<IReadOnlyCollection<ApartmentReviewResponse>>, NotFound>> GetApartmentReviews(
        Guid apartmentId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new GetApartmentReviewsQuery(apartmentId);

        var result = await sender.Send(query, cancellationToken);

        return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound();
    }

    public static async Task<Results<CreatedAtRoute<Guid>, BadRequest<Error>>> CreateApartmentReview(
        ApartmentReviewRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new CreateApartmentReviewCommand(
            request.UserId,
            request.BookingId,
            request.Rating,
            request.Comment);

        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return TypedResults.BadRequest(result.Error);
        }

        return TypedResults.CreatedAtRoute(result.Value, nameof(GetApartmentReviews), new { apartmentId = request.ApartmentId });
    }
}