namespace Wiedza.Core.Exceptions;

public class UnauthorizeException(string message) : Exception(message);

public class InvalidCredentialsException(string message) : UnauthorizeException(message);
public class InvalidToken(string message) : UnauthorizeException(message);