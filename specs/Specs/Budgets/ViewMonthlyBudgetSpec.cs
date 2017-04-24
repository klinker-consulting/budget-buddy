using System;
using System.Linq;
using System.Threading.Tasks;
using Budget.Buddy.Models.Budgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Specs.General;

namespace Specs.Budgets
{
    [TestClass]
    public class ViewMonthlyBudgetSpec : SpecBase
    {
        [TestMethod]
        public async Task ViewFirstMonthlyBudget()
        {
            Before(c => c.StartApiServer());
            await WhenAsync(ViewBudgetForCurrentMonth);
            Then(ShouldSeeTheBudgetForCurrentMonth);
            Then(ShouldSeeNoBudgetCategories);
            Then(ShouldSeeNoBudgetedItems);
        }

        private static async Task ViewBudgetForCurrentMonth(SpecContext context)
        {
            using (var client = context.CreateClient())
            {
                var budget = await client.GetAsync<BudgetModel>("/budgets/current");
                context.Set(budget);
            }
        }

        private static void ShouldSeeTheBudgetForCurrentMonth(SpecContext context)
        {
            var startOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var endOfMonth = new DateTime(startOfMonth.Year, startOfMonth.Month, DateTime.DaysInMonth(startOfMonth.Year, startOfMonth.Month));

            var budget = context.Get<BudgetModel>();
            Assert.AreEqual(startOfMonth, budget.StartDate);
            Assert.AreEqual(endOfMonth, budget.EndDate);
        }

        private static void ShouldSeeNoBudgetCategories(SpecContext context)
        {
            var budget = context.Get<BudgetModel>();
            Assert.AreEqual(0, budget.Categories.Length);
        }

        private static void ShouldSeeNoBudgetedItems(SpecContext context)
        {
            var budget = context.Get<BudgetModel>();
            Assert.AreEqual(0, budget.Categories.SelectMany(c => c.Items).Count());
        }
    }
}
