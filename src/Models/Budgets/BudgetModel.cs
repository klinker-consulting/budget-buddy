using System;

namespace Budget.Buddy.Models.Budgets
{
    public class BudgetModel
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public CategoryModel[] Categories { get; }
    }
}
