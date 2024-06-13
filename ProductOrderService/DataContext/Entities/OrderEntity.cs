namespace ProductOrderService.DataContext.Entities;

public class OrderEntity : IEntity
{
    public Guid Id { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public DateTime ModificationDate { get; set; }
        
    public DateTime OrderDate { get; set; }
    
    public virtual ICollection<OrderProductEntity> OrderProducts { get; set; }
}