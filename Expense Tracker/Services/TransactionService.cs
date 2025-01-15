using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Expense_Tracker.Model;

public class TransactionService : ITransactionService
{
    private readonly string transactionFilePath = Path.Combine(
             Environment.GetFolderPath(Environment.SpecialFolder.Personal), 
             "TransactionDetails.json");
    
    private List<Transaction> transact;

    public TransactionService()
    {
        transact = LoadTransactionsFromJson();
    }

    private List<Transaction> LoadTransactionsFromJson()
    {
        if (!File.Exists(transactionFilePath))
        {
            // If the file does not exist, return an empty list
            return new List<Transaction>();
        }

        var jsonData = File.ReadAllText(transactionFilePath);
        return JsonSerializer.Deserialize<List<Transaction>>(jsonData) ?? new List<Transaction>();
    }

    private void SaveTransactionsToJson()
    {
        var jsonData = JsonSerializer.Serialize(transact, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(transactionFilePath, jsonData);
    }

    public Task<List<Transaction>> GetTransactionsAsync()
    {
        return Task.FromResult(transact);
    }

    public Task<decimal> GetTotalIncomeAsync()
    {
        var totalIncome = transact.Where(t => t.Type == "Income").Sum(t => t.Amount);
        return Task.FromResult(totalIncome);
    }

    public Task<decimal> GetTotalExpenseAsync()
    {
        var totalExpense = transact.Where(t => t.Type == "Expense").Sum(t => t.Amount);
        return Task.FromResult(totalExpense);
    }

    public Task<decimal> GetTotalDebtAsync()
    {
        var totalDebt = transact.Where(t => t.Type == "Debt").Sum(t => t.Amount);
        return Task.FromResult(totalDebt);
    }

    public Task<decimal> GetPendingDebtAsync()
    {
        var pendingDebt = transact.Where(t => t.Type == "Debt" && t.Status == "Pending").Sum(t => t.Amount);
        return Task.FromResult(pendingDebt);
    }

    public Task<decimal> GetClearedDebtAsync()
    {
        var clearedDebt = transact.Where(t => t.Type == "Debt" && t.Status == "Cleared").Sum(t => t.Amount);
        return Task.FromResult(clearedDebt);
    }

    public Task SaveTransactionAsync(Transaction transaction)
    {
        transaction.Date = DateTime.Now;
        transact.Add(transaction);
        SaveTransactionsToJson();
        Console.WriteLine($"Transaction '{transaction.Title}' has been saved successfully to the file.");
        return Task.CompletedTask;
        
    }

    public Task UpdateTransactionAsync(Transaction transaction)
    {
        var existingTransaction = transact.FirstOrDefault(t => t.Title == transaction.Title);
        if (existingTransaction == null)
        {
            throw new InvalidOperationException($"Transaction with title '{transaction.Title}' not found.");
        }

        existingTransaction.Title = transaction.Title;
        existingTransaction.Date = transaction.Date;
        existingTransaction.Amount = transaction.Amount;
        existingTransaction.Type = transaction.Type;
        existingTransaction.Status = transaction.Status;
        existingTransaction.Notes = transaction.Notes;
        existingTransaction.Tags = transaction.Tags;
        existingTransaction.DueDate = transaction.DueDate;
        existingTransaction.Source = transaction.Source;

        SaveTransactionsToJson();
        return Task.CompletedTask;
    }
    public Task AddTransactionAsync(Transaction transaction)
    {
        // Do not overwrite the date if it's already set
        if (transaction.Date == default)
        {
            transaction.Date = DateTime.Now; // Assign the date only if it's not provided
        }
        
        transact.Add(transaction);
        return Task.CompletedTask;
    } 


    public Task DeleteTransactionAsync(string title)
    {
        transact.RemoveAll(t => t.Title == title);
        SaveTransactionsToJson();
        return Task.CompletedTask;
    }
    
    // Implement the search functionality
    public Task<List<Transaction>> SearchTransactionsByTitleAsync(string title)
    {
        var filteredTransactions = transact
            .Where(t => t.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Task.FromResult(filteredTransactions);
    }
}