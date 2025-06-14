/// <summary>
/// BankingConsoleUI.cs
/// 
/// This class provides a simple text-based user interface for the banking system.
/// It allows users to add customers, create accounts, and perform various banking operations.
/// The UI is designed to be user-friendly and guides the user through the available options.
/// It uses the BankService class to handle the core business logic, such as managing customers, accounts, and transactions.
/// </summary>

public class BankingConsoleUI
{
    private readonly BankService _bankService;

    public BankingConsoleUI(BankService bankService)
    {
        _bankService = bankService;
    }

    /// <summary>
    /// Runs the banking console application, displaying the menu and handling user input.
    /// 
    /// This method is the main loop of the application, allowing users to perform various banking operations.
    /// It continuously displays the menu until the user chooses to exit.   
    /// </summary>

    public void Run()
    {
        Console.WriteLine("Welcome to the Banking System!");

        while (true)
        {
            ShowMenu();
            var choice = Console.ReadLine();
            if (string.IsNullOrEmpty(choice))
            {
                Console.WriteLine("Please select a valid option.");
                continue;
            }

            switch (choice)
            {
                case "1":
                    // Add Customer
                    AddCustomer();

                    break;
                case "2":
                    OpenAccount();
                    break;
                case "3":
                    // Deposit
                    DepositFunds();
                    break;
                case "4":
                    // Withdraw
                    WithdrawFunds();
                    break;
                case "5":
                    // View Transactions
                    ViewTransactions();
                    break;
                case "6":
                    // Exit
                    Console.WriteLine("Thank you for using the Banking System. Goodbye!");
                    Console.ReadKey();
                    Environment.Exit(0);
                    return;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void ViewTransactions()
    {
        string symbol = "+";
        do
        {
            Console.Clear();
            Console.WriteLine("View Transactions");
            Console.WriteLine("-------------------");
            Console.Write("Enter account ID: ");
            if (!int.TryParse(Console.ReadLine(), out int accountId))
            {
                Console.WriteLine("Invalid account ID.");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
                continue;
            }
            try
            {
                var transactions = _bankService.GetTransactionsHistory(accountId);
                if (transactions != null && transactions.Any())
                {
                    Console.WriteLine($"Transactions for Account ID {accountId}:");
                    foreach (var transaction in transactions)
                    {
                        if (transaction.TransactionType == "Withdrawal")
                        {
                            symbol = "-";
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else if (transaction.TransactionType == "Deposit")
                        {
                            symbol = "+";
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                        }
                        string transactionType = transaction.TransactionType.PadLeft(10);
                        string transactionDate = transaction.TransactionDate.ToString("yyyy-MM-dd HH:mm:ss");
                        string amount = transaction.Amount.ToString("C").PadLeft(10);
                        string transactionId = transaction.TransactionId.ToString().PadLeft(5);
                        Console.WriteLine($"{transactionId} | {transactionDate} | {transactionType} | {symbol} {amount}");
                        // Console.WriteLine($"{transaction.TransactionId} | {transaction.TransactionDate} | {transaction.TransactionType} | {symbol} {transaction.Amount:C}");                        
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.WriteLine("No transactions found for this account.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving transactions: {ex.Message}");
            }
            // Wait for user input before returning to the menu            
            Console.WriteLine(Environment.NewLine + "Press any key to return to the menu...");
            Console.ReadKey();
            break;
        } while (true);
    }

    private void WithdrawFunds()
    {
        do
        {
            Console.Clear();
            Console.WriteLine("Withdraw Funds");
            Console.WriteLine("----------------");
            Console.Write("Enter account ID: ");
            if (!int.TryParse(Console.ReadLine(), out int accountId))
            {
                Console.WriteLine("Invalid account ID.");
                Console.ReadKey();
                continue;
            }
            Console.Write("Enter amount to withdraw: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount. Please enter a positive number.");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
                continue;
            }
            try
            {
                _bankService.Withdraw(accountId, amount);
                Console.WriteLine($"Successfully withdrew {amount:C} from account {accountId}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error withdrawing funds: {ex.Message}");
            }
            // Wait for user input before returning to the menu
            Console.WriteLine(Environment.NewLine + "Press any key to return to the menu...");
            Console.ReadKey();
            break;
        } while (true);
    }

    private void DepositFunds()
    {
        do
        {
            Console.Clear();
            Console.WriteLine("Deposit Funds");
            Console.WriteLine("----------------");
            Console.Write("Enter account ID: ");
            if (!int.TryParse(Console.ReadLine(), out int accountId))
            {
                Console.WriteLine("Invalid account ID.");
                Console.ReadKey();
                continue;
            }
            Console.Write("Enter amount to deposit: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount. Please enter a positive number.");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
                continue;
            }
            try
            {
                _bankService.Deposit(accountId, amount);
                Console.WriteLine($"Successfully deposited {amount:C} into account {accountId}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error depositing funds: {ex.Message}");
            }
            // Wait for user input before returning to the menu
            Console.WriteLine(Environment.NewLine + "Press any key to return to the menu...");
            Console.ReadKey();
            break;
        } while (true);
    }

    /// <summary>
    /// Opens a new account for a customer. 
    /// This method prompts the user for customer ID and account type,
    /// validates the input, and then calls the BankService to create the account.
    /// If the account is created successfully, it displays the account number.
    /// If there is an error, it displays an error message.
    /// </summary>
    private void OpenAccount()
    {
        do
        {
            Console.Clear();
            Console.WriteLine("Open Account");
            Console.WriteLine("----------------");
            Console.Write("Enter customer ID: ");
            if (!int.TryParse(Console.ReadLine(), out int customerId))
            {
                Console.WriteLine("Invalid customer ID.");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
                continue;
            }
            Console.Write("Enter account type (Savings/Checking): ");
            var accountType = Console.ReadLine();
            if (string.IsNullOrEmpty(accountType))
            {
                Console.WriteLine("Account type cannot be empty.");
                Console.ReadKey();
                continue;
            }
            try
            {
                var account = _bankService.CreateAccount(customerId, accountType);
                Console.WriteLine($"Account created successfully! Account Number: {account.AccountNumber}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating account: {ex.Message}");
            }
            // Wait for user input before returning to the menu
            Console.WriteLine(Environment.NewLine + "Press any key to return to the menu...");
            Console.ReadKey();
            break;
        } while (true);
    }

    /// <summary>
    /// Adds a new customer to the banking system.
    /// 
    /// This method prompts the user for customer details such as name and email,
    /// validates the input, and then calls the BankService to add the customer.
    /// If the customer is added successfully, it displays the customer ID.
    /// If there is an error, it displays an error message.
    /// </summary>
    private void AddCustomer()
    {
        do
        {
            Console.Clear();
            Console.WriteLine("Add customer");
            Console.WriteLine("----------------");
            Console.Write("Enter customer name: ");
            var name = Console.ReadLine();
            Console.Write("Enter customer email: ");
            var email = Console.ReadLine();
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
            {
                Console.WriteLine("Name and email cannot be empty.");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
                continue;
            }
            try
            {
                var customer = _bankService.AddCustomer(name, email);
                Console.WriteLine($"Customer added successfully! Customer ID: {customer.CustomerId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding customer: {ex.Message}");
            }
            // Wait for user input before returning to the menu
            Console.WriteLine(Environment.NewLine + "Press any key to return to the menu...");
            Console.ReadKey();
            break;
        } while (true);
    }

    /// <summary>
    /// Displays the main menu options to the user.
    /// 
    /// This method clears the console and prints the menu options for the banking system.
    /// It includes options to add a customer, open an account, deposit funds, withdraw funds, view transactions, and exit the application.
    /// </summary>
    private void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine(".::Banking System Menu::.");
        Console.WriteLine("-------------------------");
        Console.WriteLine("1. Add Customer");
        Console.WriteLine("2. Open Account");
        Console.WriteLine("3. Deposit");
        Console.WriteLine("4. Withdraw");
        Console.WriteLine("5. View Transactions");
        Console.WriteLine("6. Exit");
        Console.WriteLine("-------------------------");
        Console.Write("Select an option: ");

    }


}
