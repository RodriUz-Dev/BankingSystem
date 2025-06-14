public class CheckingAccount : Account
{
  public decimal OverdraftLimit { get; set; }

  public CheckingAccount()
      : base()
  {
  }

  public CheckingAccount(int accountId, string accountNumber, int customerId, decimal balance, decimal overdraftLimit)
      : base(accountId, accountNumber, customerId, balance, "Checking")
  {
    OverdraftLimit = overdraftLimit;
  }

  public override void Deposit(decimal amount)
  {
    if (amount <= 0)
      throw new ArgumentException("Deposit amount must be positive.");
    Balance += amount;
  }

  public override void Withdraw(decimal amount)
  {
    if (amount <= 0)
      throw new ArgumentException("Withdrawal amount must be positive.");
    if (Balance + OverdraftLimit < amount)
      throw new InvalidOperationException("Insufficient funds including overdraft limit.");
    Balance -= amount;
  }

  public override string ToString()
  {
    return $"{base.ToString()}, Overdraft Limit: {OverdraftLimit:C}";
  }
}