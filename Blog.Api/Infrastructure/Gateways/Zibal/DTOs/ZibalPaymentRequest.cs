using Newtonsoft.Json;

namespace Blog.Api.Infrastructure.Gateways.Zibal.DTOs;

public class ZibalPaymentRequest
{
    [JsonProperty("merchant")]
    public string Merchant { get; set; } = string.Empty;
    public int Amount { get; set; }

    [JsonProperty("callbackUrl")]
    public string CallBackUrl { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public bool LinkToPay { get; set; }
    [JsonProperty("sms")]
    public bool SendSms { get; set; }
}