namespace Budget.Buddy.Models.Budgets
{
    public class CategoryModel
    {
        public string Name { get; }

        public BudgetItemModel[] Items { get; }
    }
}