using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events;

public record BookingCancelledDomainEvent(BookingId BookingId) : IDomainEvent;