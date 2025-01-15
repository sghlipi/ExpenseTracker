using System.Collections.Generic;
using System.Threading.Tasks;
using Expense_Tracker.Model;

public interface ITransactionService
{
    Task<List<Transaction>> GetTransactionsAsync();
    Task AddTransactionAsync(Transaction transaction);
    Task<decimal> GetTotalIncomeAsync();
    Task<decimal> GetTotalExpenseAsync();
    Task<decimal> GetTotalDebtAsync();
    Task<decimal> GetPendingDebtAsync();
    Task<decimal> GetClearedDebtAsync();
    Task SaveTransactionAsync(Transaction transaction);
    Task UpdateTransactionAsync(Transaction transaction);
    Task DeleteTransactionAsync(string title);
    
}