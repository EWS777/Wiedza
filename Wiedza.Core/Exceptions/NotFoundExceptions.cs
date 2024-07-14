namespace Wiedza.Core.Exceptions;

public class NotFoundException(string message) : Exception(message);

public class PersonNotFoundException : NotFoundException
{
    public PersonNotFoundException(Guid personId) : base($"Person with id `{personId}` not found!") { }
    public PersonNotFoundException(string username) : base($"Person with username `{username}` not found!") { }
}

public class PublicationNotFoundException : NotFoundException
{
    public PublicationNotFoundException(Guid publicationId) : base($"Publication with id `{publicationId}` not found!") { }
}

