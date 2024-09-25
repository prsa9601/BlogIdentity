namespace Blog.Api.Infrastructure.Gateways.Zibal.DTOs;

public class ZibalVeriyfyResponse
{
    public DateTime PaidAt { get; set; }
    public int Amount { get; set; }
    public int Result { get; set; }
    public int Status { get; set; }
    public int? RefNumber { get; set; }
    public string Description { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public string OrderId { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}