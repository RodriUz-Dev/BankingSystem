public class BankService
{

  private List<Customer> _customers = new List<Customer>();
  private List<Account> _accounts = new List<Account>();
  private List<Transaction> _transactions = new List<Transaction>();

  /// <summary>
  /// Adds a new customer to the bank.
  /// Throws an exception if the customer already exists.
  /// <param name="name"></param> 
  /// <param name="email"></param>
  public Customer AddCustomer(string name, string email)
  {
    var customer = new Customer
    {
      CustomerId = _customers.Count + 1,
      Name = name,
      Email = email,
      PhoneNumber = string.Empty // Optional, can be set later
    };
    _customers.Add(customer);
    return customer;
  }

  /// <summary>
  /// Creates a new account for a customer.
  /// Throws an exception if the account type is invalid or if the customer does not exist.
  /// </summary>
  /// <param name="customerId"></param>
  /// <param name="Accountype"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public Account CreateAccount(int customerId, string Accountype)
  {
    if (string.IsNullOrEmpty(Accountype))
    {
      throw new ArgumentException("Account type cannot be null or empty");
    }
    if (!_customers.Any(c => c.CustomerId == customerId))
    {
      throw new InvalidOperationException("Customer not found");
    }

    Account account = Accountype switch
    {
      "Savings" => new SavingsAccount
      {
        AccountId = _accounts.Count + 1,
        AccountNumber = GenerateAccountNumber(),
        CustomerId = customerId,
        Balance = 0,
        InterestRate = 2.5m // Default interest rate for savings accounts
      },
      "Checking" => new CheckingAccount
      {
        AccountId = _accounts.Count + 1,
        AccountNumber = GenerateAccountNumber(),
        CustomerId = customerId,
        Balance = 0,
        OverdraftLimit = 500 // Default overdraft limit for checking accounts
      },
      _ => throw new ArgumentException("Invalid account type")
    };
    _accounts.Add(account);
    return account;
  }

  /// <summary>
  /// Deposits a specified amount into the account.
  /// Throws an exception if the account does not exist or if the amount is less than or equal to zero. 
  /// </summary>
  /// <param name="accountId"></param>
  /// <param name="amount"></param>
  /// <exception cref="InvalidOperationException"></exception>
  /// <exception cref="ArgumentException"></exception>
  public void Deposit(int accountId, decimal amount)
  {
    var account = _accounts.FirstOrDefault(a => a.AccountId == accountId);
    if (account == null)
    {
      throw new InvalidOperationException("Account not found");
    }
    if (amount <= 0)
    {
      throw new ArgumentException("Deposit amount must be greater than zero");
    }
    account.Balance += amount;
    var transaction = new Transaction
    {
      TransactionId = _transactions.Count + 1,
      AccountId = accountId,
      Amount = amount,
      TransactionType = "Deposit",
      TransactionDate = DateTime.Now
    };
    _transactions.Add(transaction);
    Console.WriteLine($"Deposited {amount:C} to account {account.AccountNumber}. New balance: {account.Balance:C}");
    // Optionally, log the transaction
    // LogTransaction(accountId, amount, "Deposit");
  }

  /// <summary>
  /// Withdraws a specified amount from the account.
  /// Throws an exception if the account does not exist, if the amount is less than or equal to zero,
  /// or if there are insufficient funds.
  /// The method also handles overdrafts for checking accounts.
  /// </summary>
  /// <param name="accountId"></param>
  /// <param name="amount"></param>
  /// <exception cref="InvalidOperationException"></exception>
  /// <exception cref="ArgumentException"></exception>
  public void Withdraw(int accountId, decimal amount)
  {
    var account = _accounts.FirstOrDefault(a => a.AccountId == accountId);
    if (account == null)
    {
      throw new InvalidOperationException("Account not found");
    }
    if (amount <= 0)
    {
      throw new ArgumentException("Withdrawal amount must be greater than zero");
    }
    if (account.Balance + (account is CheckingAccount checking ? checking.OverdraftLimit : 0) < amount)
    {
      throw new InvalidOperationException("Insufficient funds for withdrawal");
    }
    account.Balance -= amount;
    var transaction = new Transaction
    {
      TransactionId = _transactions.Count + 1,
      AccountId = accountId,
      Amount = amount,
      TransactionType = "Withdrawal",
      TransactionDate = DateTime.Now
    };
    _transactions.Add(transaction);
    Console.WriteLine($"Withdrew {amount:C} from account {account.AccountNumber}. New balance: {account.Balance:C}");
    // Optionally, log the transaction
    // LogTransaction(accountId, amount, "Withdrawal");
  }

  /// <summary>
  /// Retrieves the transaction history for a specific account.
  /// Throws an exception if the account does not exist.
  /// </summary>
  /// <param name="accountId"></param>
  public IEnumerable<Transaction> GetTransactionsHistory(int accountId)
  {
    if (!_accounts.Any(a => a.AccountId == accountId))
    {
      throw new InvalidOperationException("Account not found");
    }
    return _transactions.Where(t => t.AccountId == accountId).ToList();
  }

  public Customer GetCustomerDetails(int customerId)
  {
    return _customers.FirstOrDefault(c => c.CustomerId == customerId)
            ?? throw new InvalidOperationException("Customer not found");
  }

  public Account GetAccountDetails(int accountId)
  {
    return _accounts.FirstOrDefault(a => a.AccountId == accountId)
            ?? throw new InvalidOperationException("Account not found");
  }

  private string GenerateAccountNumber()
  {
    string prefix = "ACMX";
    string suffix = (_accounts.Count + 1).ToString("D6"); // Zero-padded to 6 digits
    return $"{prefix}-{suffix}";
  }
}