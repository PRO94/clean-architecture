using Bookify.Application.Abstractions.Clock;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;
using Bookify.Domain.Reviews;
using Bookify.Domain.Users;

namespace Bookify.Application.Reviews.CreateApartmentReview;

internal sealed class CreateApartmentReviewCommandHandler : ICommandHandler<CreateApartmentReviewCommand, Guid>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateApartmentReviewCommandHandler(
        IReviewRepository reviewRepository,
        IUserRepository userRepository,
        IBookingRepository bookingRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _reviewRepository = reviewRepository;
        _userRepository = userRepository;
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(CreateApartmentReviewCommand request, CancellationToken cancellationToken)
    {
        // TODO: need to get userId from the logged in user JWT as this request is available only for logged in users
        var user = await _userRepository.GetByIdAsync(new UserId(request.UserId), cancellationToken);
        
        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        var booking = await _bookingRepository.GetByIdAsync(new BookingId(request.BookingId), cancellationToken);

        if (booking is null)
        {
            return Result.Failure<Guid>(BookingErrors.NotFound);
        }

        var rating = Rating.Create(request.Rating);

        if (rating.IsFailure)
        {
            return Result.Failure<Guid>(rating.Error);
        }

        var review = Review.Create(
            booking,
            rating.Value,
            new Comment(request.Comment),
            _dateTimeProvider.UtcNow);

        if (review.IsFailure)
        {
            return Result.Failure<Guid>(review.Error);
        }

        _reviewRepository.Add(review.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return review.Value.Id.Value;
    }
}