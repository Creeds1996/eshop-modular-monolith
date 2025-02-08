namespace Catalog.Data.Seed;

public static class InitialData
{
    public static IEnumerable<Product> Products =>
        new List<Product>
        {
            Product.Create(Guid.NewGuid(), "Phone #1", ["Brand #1"], "This is phone #1.", "imagefile", 100),
            Product.Create(Guid.NewGuid(), "Phone #2", ["Brand #2"], "This is phone #2.", "imagefile", 200),
            Product.Create(Guid.NewGuid(), "Phone #3", ["Brand #1"], "This is phone #3.", "imagefile", 300),
            Product.Create(Guid.NewGuid(), "Phone #4", ["Brand #2"], "This is phone #4.", "imagefile", 400)
        };
}