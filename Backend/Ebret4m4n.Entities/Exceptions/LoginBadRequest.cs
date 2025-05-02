namespace Ebret4m4n.Entities.Exceptions;

public class LoginBadRequest : BadRequestException
{
    public LoginBadRequest() 
        : base("خطا في الايمل او كلمه المرور")
    { }
}
