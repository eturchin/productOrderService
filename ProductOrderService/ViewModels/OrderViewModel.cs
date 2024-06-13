namespace ProductOrderService.ViewModels;

public class OrderViewModel
{
    public Guid Id { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public DateTime ModificationDate { get; set; }
    
    public DateTime OrderDate { get; set; }

    public List<Guid> ProductIds { get; set; }
}