using System.Threading.Tasks;
using Budget.Buddy.Models.Budgets;
using Microsoft.AspNetCore.Mvc;

namespace Budget.Buddy.Api.Budgets
{
    public class BudgetController : Controller
    {
        public Task<IActionResult> GetCurrent()
        {
            return Task.FromResult<IActionResult>(Ok(new BudgetModel()));
        }
    }
}