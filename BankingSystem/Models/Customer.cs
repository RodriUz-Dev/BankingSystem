public class Customer
{
  public int CustomerId { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string PhoneNumber { get; set; }

  public Customer()
  {
    // Default constructor for serialization or ORM purposes
    CustomerId = 0;
    Name = string.Empty;
    Email = string.Empty;
    PhoneNumber = string.Empty;
  }

  public Customer(int customerId, string name, string email, string phoneNumber)
  {
    CustomerId = customerId;
    Name = name;
    Email = email;
    PhoneNumber = phoneNumber;
  }

  public override string ToString()
  {
    return $"Customer ID: {CustomerId}, Name: {Name}, Email: {Email}, Phone: {PhoneNumber}";
  }
}