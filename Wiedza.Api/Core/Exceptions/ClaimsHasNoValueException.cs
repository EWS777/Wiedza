namespace Wiedza.Api.Core.Exceptions;

public class MissingClaimException(string nameType) : Exception($"Claims doesn't contains `{nameType}`");