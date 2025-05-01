namespace Ebret4m4n.Shared.DTOs;

public class GeneralResponse<T>
{
    public bool Success { get; private set; } 

    public T? Data { get; private set; }

    public T? Message { get; private set; }


    public static GeneralResponse<T> SuccessResponse(T data)
        => new GeneralResponse<T>() { Success = true, Data = data, Message = default };

    public static GeneralResponse<T> FailureResponse(T message)
        => new GeneralResponse<T> { Success = false, Data = default, Message = message };
}
