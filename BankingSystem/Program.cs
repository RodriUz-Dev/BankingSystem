// See https://aka.ms/new-console-template for more information
///
/// This is the entry point for the Banking System application.
Console.WriteLine("Welcome to the Banking System!");

// Initialize the BankService and BankingConsoleUI
// The BankService class contains the core business logic for managing customers, accounts, and transactions.
var bankService = new BankService();
// The BankingConsoleUI class provides a simple text-based interface for interacting with the banking system.
// It allows users to add customers, create accounts, deposit and withdraw funds, and view transactions.
var bankingConsoleUI = new BankingConsoleUI(bankService);
// Run the console UI
// The Run method of BankingConsoleUI starts the application and displays the main menu.
// Users can interact with the system through this menu, performing various banking operations.
bankingConsoleUI.Run();

