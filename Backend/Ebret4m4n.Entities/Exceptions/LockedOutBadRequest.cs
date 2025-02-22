namespace Ebret4m4n.Entities.Exceptions;

public class LockedOutBadRequest : BadRequestException
{
    public LockedOutBadRequest() 
        : base("You are Locked try again later")
    { }
}
