namespace Ebret4m4n.Entities.Exceptions;

public class NotFoundBadRequest : NotFoundException
{
    public NotFoundBadRequest(string message)
        : base(message)
    { }
}
