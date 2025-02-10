namespace Ordering.Orders.ValueObjects;

public record Address
{
    public string FirstName { get; } = null!;
    public string LastName { get; } = null!;
    public string? Email { get; } = null!;
    public string AddressLine { get; } = null!;
    public string Country { get; } = null!;
    public string State { get; } = null!;
    public string ZipCode { get; } = null!;

    protected Address()
    {
        
    }
    
    private Address(string firstName, string lastName, string email, string addressLine, string country, string state, string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        AddressLine = addressLine;
        Country = country;
        State = state;
        ZipCode = zipCode;
    }

    public static Address Of(string firstName, string lastName, string email, string addressLine, string country, string state, string zipCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);
        
        return new Address(firstName, lastName, email, addressLine, country, state, zipCode);
    }
}