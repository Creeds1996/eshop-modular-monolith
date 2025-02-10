namespace Basket.Basket.Models;

public class OutboxMessage : Entity<Guid>
{
    public string Type { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime OccurredOn { get; set; }
    public DateTime? ProccessedOn { get; set; }
}