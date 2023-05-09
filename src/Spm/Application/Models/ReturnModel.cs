namespace Application.Models;

public class ReturnModel<T>
{
    public T Data { get; set; } // orjinali
    public ReturnTypeStatus Status { get; set; }
    public string Message { get; set; }
    public string ErrorMessage { get; set; }

}

public enum ReturnTypeStatus
{
    Error = 0,
    Success = 1,
    UnSuccess = 2,
    NotAuthorized = 3
}
