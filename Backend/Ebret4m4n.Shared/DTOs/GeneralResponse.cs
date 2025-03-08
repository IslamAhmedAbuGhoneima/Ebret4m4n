namespace Ebret4m4n.Shared.DTOs;

public class GeneralResponse<T>
{
    public int Status { get; private set; }
    public T Data { get; private set; }

    public GeneralResponse(int status,T data)
    {
        Status = status;
        Data = data;
    }
}
