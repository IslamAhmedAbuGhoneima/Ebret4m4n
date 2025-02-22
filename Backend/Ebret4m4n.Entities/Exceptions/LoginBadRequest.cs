namespace Ebret4m4n.Entities.Exceptions;

public class LoginBadRequest : BadRequestException
{
    public LoginBadRequest() 
        : base("Wrong email or password")
    { }
}
