namespace Blog.Api.ViewModels.Transactions;

public class CreateTransactionViewModel
{
    public long OrderId { get; set; }
    public string SuccessCallBackUrl { get; set; }
    public string ErrorCallBackUrl { get; set; }

    public CreateTransactionViewModel(long orderId, string successCallBackUrl, string errorCallBackUrl)
    {
        OrderId = orderId;
        SuccessCallBackUrl = successCallBackUrl;
        ErrorCallBackUrl = errorCallBackUrl;
    }
}