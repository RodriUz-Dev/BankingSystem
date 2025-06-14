public class Transaction
{
  public int TransactionId { get; set; }
  public int AccountId { get; set; } // Foreign key to Account
  public decimal Amount { get; set; }
  public DateTime TransactionDate { get; set; }
  public string TransactionType { get; set; } // e.g., Deposit, Withdrawal

  // Constructor to initialize a new transaction
  public Transaction()
  {
    TransactionId = 0;
    AccountId = 0;
    Amount = 0.0m;
    TransactionDate = DateTime.Now;
    TransactionType = string.Empty; // Default type can be set later
  }

  public Transaction(int transactionId, int accountId, decimal amount, DateTime transactionDate, string transactionType)
  {
    TransactionId = transactionId;
    AccountId = accountId;
    Amount = amount;
    TransactionDate = transactionDate;
    TransactionType = transactionType;
  }

  public override string ToString()
  {
    return $"Transaction ID: {TransactionId}, Account ID: {AccountId}, Amount: {Amount:C}, Date: {TransactionDate}, Type: {TransactionType}";
  }
}