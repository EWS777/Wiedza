namespace Wiedza.Core.Exceptions;

public class NotFoundException(string message) : Exception(message);

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string usernameOrEmail) : base($"User `{usernameOrEmail}` not found!")
    {
    }

    public UserNotFoundException(Guid userId) : base($"User `{userId}` not found!")
    {
    }
}

public class PersonNotFoundException : NotFoundException
{
    public PersonNotFoundException(Guid personId) : base($"Person with id `{personId}` not found!")
    {
    }

    public PersonNotFoundException(string usernameOrEmail) : base($"Person `{usernameOrEmail}` not found!")
    {
    }
}

public class PublicationNotFoundException(ulong publicationId)
    : NotFoundException($"Publication with id `{publicationId}` not found!");

public class ProjectNotFoundException(ulong projectId) : NotFoundException($"Project with id `{projectId}` not found!");

public class ServiceNotFoundException(ulong serviceId) : NotFoundException($"Service with id `{serviceId}` not found!");

public class OfferNotFoundException(Guid offerId) : NotFoundException($"Offer with id `{offerId}` not found!");

public class ComplaintNotFoundException(Guid complaintId) : NotFoundException($"Complaint with id `{complaintId}` not found!");

public class WithdrawNotFoundException(Guid withdrawId) : NotFoundException($"Withdraw with id `{withdrawId}` not found!");
public class VerificationNotFoundException(Guid verificationId) : NotFoundException($"Verification with id `{verificationId}` not found!");