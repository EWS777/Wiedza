namespace Wiedza.Core.Exceptions;

public class BadRequestException(string message) : Exception(message);

public class CreationException(string message) : BadRequestException(message);

public class NotEnoughMoneyException(string message) : BadRequestException(message);