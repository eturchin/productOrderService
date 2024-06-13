namespace ProductOrderService.DataContext.Entities;

public class ProductEntity : IEntity
{
    public Guid Id { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public DateTime ModificationDate { get; set; }
    
    public string Name { get; set; }
    
    public decimal Price { get; set; }
    
    public virtual ICollection<OrderProductEntity> OrderProducts { get; set; }
}