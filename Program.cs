using System;

public static class BankingService
{
    private static decimal lastTransaction = 0;

    // Option 1: Check Balance (pass-by-value)
    public static decimal CheckBalance(decimal balance)
    {
        return balance;
    }

    // Option 2: Deposit Money (using ref)
    public static bool Deposit(ref decimal balance, decimal amount)
    {
        if (amount > 0)
        {
            balance += amount;
            lastTransaction = amount;
            return true;
        }
        else
        {
            lastTransaction = 0;
            return false;
        }
    }

    // Option 3: Withdraw Money (using ref and out)
    public static void Withdraw(ref decimal balance, decimal amount, out bool success)
    {
        if (amount > 0 && amount <= balance)
        {
            balance -= amount;
            lastTransaction = amount;
            success = true;
        }
        else
        {
            success = false;
            lastTransaction = 0;
        }
    }

    // Option 4: Mini Statement (pass-by-value)
    public static (decimal balance, decimal lastTransaction) GetMiniStatement(decimal balance)
    {
        return (balance, lastTransaction);
    }
}

public static class BankingView
{
    public static void ShowWelcome(string name)
    {
        Console.WriteLine($"{name} === Simple ATM System ===");
        Console.WriteLine("Initial Balance: ₱1000.00\n");
    }

    public static void ShowMenu()
    {
        Console.WriteLine("1: Check Balance");
        Console.WriteLine("2: Deposit Money");
        Console.WriteLine("3: Withdraw Money");
        Console.WriteLine("4: Print Mini Statement");
        Console.WriteLine("5: Exit");
        Console.Write("Select an option: ");
    }

    public static void ShowBalance(decimal balance)
    {
        Console.WriteLine($"Current Balance: ₱{balance}");
    }

    public static void ShowDepositResult(bool success, decimal amount, decimal balance)
    {
        if (success)
        {
            Console.WriteLine("Deposit successful.");
            Console.WriteLine($"Updated Balance: ₱{balance}");
        }
        else
        {
            Console.WriteLine("Invalid deposit amount. Please enter a positive value.");
        }
    }

    public static void ShowWithdrawResult(bool success, decimal amount, decimal balance)
    {
        if (success)
        {
            Console.WriteLine("Withdrawal successful.");
            Console.WriteLine($"Updated Balance: ₱{balance}");
        }
        else if (amount <= 0)
        {
            Console.WriteLine("Invalid withdrawal amount. Please enter a positive value.");
        }
        else
        {
            Console.WriteLine("Withdrawal failed. Insufficient balance.");
        }
    }

    public static void ShowMiniStatement(decimal balance, decimal lastTransaction)
    {
        Console.WriteLine("--- Mini Statement ---");
        Console.WriteLine($"Current Balance: ₱{balance}");
        Console.WriteLine($"Last Transaction Amount: ₱{lastTransaction}");
    }

    public static void ShowExit()
    {
        Console.WriteLine("Thank you for using the ATM. Goodbye!");
    }

    public static void ShowInvalidOption()
    {
        Console.WriteLine("Invalid option selected. Please try again.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        decimal balance = 1000.00m;
        BankingView.ShowWelcome("Aldren Omandam");

        bool running = true;
        while (running)
        {
            BankingView.ShowMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    decimal currentBalance = BankingService.CheckBalance(balance);
                    BankingView.ShowBalance(currentBalance);
                    break;

                case "2":
                    Console.Write("Enter amount to deposit: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
                    {
                        bool success = BankingService.Deposit(ref balance, depositAmount);
                        BankingView.ShowDepositResult(success, depositAmount, balance);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }
                    break;

                case "3":
                    Console.Write("Enter amount to withdraw: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
                    {
                        BankingService.Withdraw(ref balance, withdrawAmount, out bool success);
                        BankingView.ShowWithdrawResult(success, withdrawAmount, balance);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }
                    break;

                case "4":
                    var statement = BankingService.GetMiniStatement(balance);
                    BankingView.ShowMiniStatement(statement.balance, statement.lastTransaction);
                    break;

                case "5":
                    BankingView.ShowExit();
                    running = false;
                    break;

                default:
                    BankingView.ShowInvalidOption();
                    break;
            }

            Console.WriteLine(); // spacing
        }
    }
}

