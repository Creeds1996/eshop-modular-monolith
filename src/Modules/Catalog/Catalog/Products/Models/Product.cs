namespace Catalog.Products.Models;

public class Product : Aggregate<Guid>
{
    public string Name { get; private set; } = null!;
    public List<string> Categories { get; private set; } = [];
    public string Description { get; private set; } = null!;
    public string ImageFile { get; private set; } = null!;
    public decimal Price { get; private set; }
    
    public static Product Create(Guid id, string name, List<string> categories, string description, string imageFile, decimal price)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        
        var newProduct = new Product
        {
            Id = id,
            Name = name,
            Categories = categories,
            Description = description,
            ImageFile = imageFile,
            Price = price
        };
        
        newProduct.AddDomainEvent(new ProductCreatedEvent(newProduct));

        return newProduct;
    }

    public void Update(string name, List<string> categories, string description, string imageFile, decimal price)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        
        Name = name;
        Categories = categories;
        Description = description;
        ImageFile = imageFile;
        
        if (Price == price) return;
        
        // if price is updated, raise ProductPriceChanged domain event
        Price = price;
        AddDomainEvent(new ProductPriceChangedEvent(this));
    }
}