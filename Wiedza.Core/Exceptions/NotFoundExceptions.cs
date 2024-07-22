namespace Wiedza.Core.Exceptions;

public class NotFoundException(string message) : Exception(message);

public class PersonNotFoundException : NotFoundException
{
    public PersonNotFoundException(Guid personId) : base($"Person with id `{personId}` not found!") { }
    public PersonNotFoundException(string usernameOrEmail) : base($"Person with username or email `{usernameOrEmail}` not found!") { }
}

public class ProjectNotFoundException(ulong publicationId) : NotFoundException($"Project with id `{publicationId}` not found!");
public class ServiceNotFoundException(ulong publicationId) : NotFoundException($"Service with id `{publicationId}` not found!");
public class OfferNotFoundException(Guid offerId) : NotFoundException($"Offer with id `{offerId}` not found!");

