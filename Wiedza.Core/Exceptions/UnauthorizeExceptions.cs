namespace Wiedza.Core.Exceptions;

public class UnauthorizedException(string message) : Exception(message);

public class InvalidCredentialsException(string message) : UnauthorizedException(message);
public class InvalidToken(string message) : UnauthorizedException(message);