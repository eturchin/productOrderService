namespace ProductOrderService.DataContext.Entities;

public interface IEntity
{
    public Guid Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModificationDate { get; set; }
}