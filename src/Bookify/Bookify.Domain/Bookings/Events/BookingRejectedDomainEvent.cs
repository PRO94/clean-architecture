﻿using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events;

public record BookingRejectedDomainEvent(Guid BookingId) : IDomainEvent;
