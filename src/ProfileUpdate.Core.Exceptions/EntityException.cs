namespace ProfileUpdate.Core.Exceptions;

public class EntityException : Exception
{
    public EntityException(string? message) : base(message)
    {
    }
}