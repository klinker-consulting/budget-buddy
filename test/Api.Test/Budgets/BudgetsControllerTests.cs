using System;
using System.Linq;
using System.Threading.Tasks;
using Budget.Buddy.Api.Budgets;
using Budget.Buddy.Models.Budgets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Budget.Buddy.Api.Test.Budgets
{
    [TestClass]
    public class BudgetsControllerTests
    {
        [TestMethod]
        public async Task GetCurrentShouldReturnEmptyBudget()
        {
            var controller = new BudgetController();
            var result = (OkObjectResult)await controller.GetCurrent();
            var budget = (BudgetModel) result.Value;
            Assert.AreEqual(new DateTime(2017, 3, 1), budget.StartDate);
            Assert.AreEqual(new DateTime(2017, 3, 31), budget.EndDate);
            Assert.AreEqual(0, budget.Categories.Length);
            Assert.AreEqual(0, budget.Categories.SelectMany(c => c.Items).Count());
        }
    }
}
