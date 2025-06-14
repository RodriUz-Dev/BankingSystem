public class SavingsAccount : Account
{
    public decimal InterestRate { get; set; }
    // Default constructor for serialization or ORM purposes
    public SavingsAccount()
        : base()
    {
        InterestRate = 0.0m; // Default interest rate
    }

    public SavingsAccount(int accountId, string accountNumber, int customerId, decimal balance, decimal interestRate)
        : base(accountId, accountNumber, customerId, balance, "Savings")
    {
        InterestRate = interestRate;
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
        if (Balance < amount)
            throw new InvalidOperationException("Insufficient funds.");
        Balance -= amount;
    }

    public void ApplyInterest()
    {
        Balance += Balance * InterestRate / 100;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Interest Rate: {InterestRate}%";
    }
}