using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events;

public record BookingResevedDomainEvent(Guid BookingId) : IDomainEvent;