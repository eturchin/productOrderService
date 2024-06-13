namespace ProductOrderService.AbstractResponses;

public interface IResponse
{
    string Message { get; set; }

    int StatusCode { get; set; }
}