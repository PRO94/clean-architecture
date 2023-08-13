using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events;

public record BookingReservedDomainEvent(BookingId BookingId) : IDomainEvent;