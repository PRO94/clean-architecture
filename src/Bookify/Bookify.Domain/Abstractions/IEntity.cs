namespace Bookify.Domain.Abstractions;

public interface IEntity
{
    IReadOnlyCollection<IDomainEvent> GetDomainEvents();

    void ClearDomainEvents();
}