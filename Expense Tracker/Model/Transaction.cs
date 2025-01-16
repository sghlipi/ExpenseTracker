namespace Expense_Tracker.Model
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Title { get; set; } //Title of Transaction
        public DateTime Date { get; set; }  //Date of transaction
        public decimal Amount { get; set; } //Amount of transation
        public string Type { get; set; } //Transaction of Type Credit, Debit, Debt

        public string Notes { get; set; } //Note of transation

        public string Tags { get; set; } //Tags of transation
        
        public string Status { get; set; } // Status for debts: "Pending" or "Cleared"
        
        public DateTime DueDate { get; set; }  // Due date for debt
        public string Source { get; set; } // Source of the debt
        public bool IsEditing { get; set; }
    }
}