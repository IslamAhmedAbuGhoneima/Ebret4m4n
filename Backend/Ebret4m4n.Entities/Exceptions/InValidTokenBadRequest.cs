namespace Ebret4m4n.Entities.Exceptions;

public class InValidTokenBadRequest : BadRequestException
{
    public InValidTokenBadRequest() 
        : base("تم انتهاء صلاحيه هذ الرابط")
    { }
}
