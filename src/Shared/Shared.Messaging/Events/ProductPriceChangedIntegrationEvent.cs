namespace Shared.Messaging.Events;

public record ProductPriceChangedIntegrationEvent : IntegrationEvent
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = null!;
    public List<string> Categories { get; set; } = [];
    public string Description { get; set; } = null!;
    public string ImageFile = null!;
    public decimal Price { get; set; }
}