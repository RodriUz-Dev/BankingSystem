public abstract class Account
{
  public int AccountId { get; set; }
  public string AccountNumber { get; set; }
  public int CustomerId { get; set; } // Foreign key to Customer
  public decimal Balance { get; set; }
  public string AccountType { get; set; } // e.g., Savings, Checking

  // Default constructor for serialization or ORM purposes
  protected Account()
  {
    AccountId = 0;
    AccountNumber = string.Empty;
    CustomerId = 0;
    Balance = 0.0m;
    AccountType = string.Empty;
  }
  protected Account(int accountId, string accountNumber, int customerId, decimal balance, string accountType)
  {
    AccountId = accountId;
    AccountNumber = accountNumber;
    CustomerId = customerId;
    Balance = balance;
    AccountType = accountType;
  }

  public abstract void Deposit(decimal amount);
  public abstract void Withdraw(decimal amount);

  public override string ToString()
  {
    return $"Account ID: {AccountId}, Number: {AccountNumber}, Type: {AccountType}, Balance: {Balance:C}";
  }
}