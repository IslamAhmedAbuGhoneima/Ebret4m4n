namespace Ebret4m4n.Entities.Exceptions;

public class LockedOutBadRequest : BadRequestException
{
    public LockedOutBadRequest() 
        : base("لقد تم حظرك لمده 15 دقيقه")
    { }
}
