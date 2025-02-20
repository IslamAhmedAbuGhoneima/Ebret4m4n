namespace Ebret4m4n.Entities.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) :
        base(message)
    { }
    
}
