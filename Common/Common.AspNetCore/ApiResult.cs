namespace Common.AspNetCore;

public class ApiResult
{
    //remind
    public bool IsSuccess { get; set; }
    public MetaData MetaData { get; set; } = new MetaData();
}
public class ApiResult<TData>
{
    //remind
    public bool IsSuccess { get; set; }
    public TData? Data { get; set; }
    public MetaData MetaData { get; set; } = new MetaData();
}
public class MetaData
{
    public string Message { get; set; } = string.Empty;
    public AppStatusCode AppStatusCode { get; set; }
}

public enum AppStatusCode
{
    Success = 1,
    NotFound = 2,
    BadRequest = 3,
    LogicError = 4,
    UnAuthorize = 5,
    ServerError
}