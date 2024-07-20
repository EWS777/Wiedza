namespace Wiedza.Core.Exceptions;

public class NotFoundException(string message) : Exception(message);

public class PersonNotFoundException : NotFoundException
{
    public PersonNotFoundException(Guid personId) : base($"Person with id `{personId}` not found!") { }
    public PersonNotFoundException(string usernameOrEmail) : base($"Person with username or email `{usernameOrEmail}` not found!") { }
}

public class PublicationNotFoundException(ulong? publicationId) : NotFoundException($"Publication with id `{publicationId}` not found!");

