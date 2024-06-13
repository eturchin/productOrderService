namespace ProductOrderService.AbstractResponses;

public class PageViewResponse<T> : PageView<T>, IResponse where T : class
{
    public string Message { get; set; }

    public int StatusCode { get; set; }
}