namespace IntraManage.Data.Models;

public class ApiResponse<T>
{
    public int code { get; set; }
    public bool hasError { get; set; }
    public T? payload { get; set; }
}
