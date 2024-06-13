namespace ProductOrderService.ViewModels;

public class ProductViewModel
{
    public Guid Id { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public DateTime ModificationDate { get; set; }
    
    public string Name { get; set; }
    
    public decimal Price { get; set; }
}