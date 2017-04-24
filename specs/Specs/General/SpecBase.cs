using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Specs.General
{
    [TestClass]
    public abstract class SpecBase
    {
        private SpecContext _context;
        private Exception _exception;

        [TestInitialize]
        public void Initialize()
        {
            _context = new SpecContext();
        }

        public void Before(Action<SpecContext> before)
        {
            WriteLine($"Executing Before {GetMethodName(before)}", OutputLevel.Information);
            SafeExecute(before);
            WriteLine($"Executed Before {GetMethodName(before)}", OutputLevel.Information);
        }

        public async Task BeforeAsync(Func<SpecContext, Task> before)
        {
            WriteLine($"Executing Before {GetMethodName(before)}", OutputLevel.Information);
            await SafeExecute(before);
            WriteLine($"Executed Before {GetMethodName(before)}", OutputLevel.Information);
        }

        public void Given(Action<SpecContext> given)
        {
            WriteLine($"Executing Given {GetMethodName(given)}", OutputLevel.Information);
            SafeExecute(given);
            WriteLine($"Executed Given {GetMethodName(given)}", OutputLevel.Information);
        }

        public async Task GivenAsync(Func<SpecContext, Task> given)
        {
            WriteLine($"Executing Given {GetMethodName(given)}", OutputLevel.Information);
            await SafeExecute(given);
            WriteLine($"Executed Given {GetMethodName(given)}", OutputLevel.Information);
        }

        public void When(Action<SpecContext> when)
        {
            WriteLine($"Executing When {GetMethodName(when)}", OutputLevel.Information);
            SafeExecute(when);
            WriteLine($"Executed When {GetMethodName(when)}", OutputLevel.Information);
        }

        public async Task WhenAsync(Func<SpecContext, Task> when)
        {
            WriteLine($"Executing When {GetMethodName(when)}", OutputLevel.Information);
            await SafeExecute(when);
            WriteLine($"Executed When {GetMethodName(when)}", OutputLevel.Information);
        }

        public void Then(Action<SpecContext> then)
        {
            WriteLine($"Executing Then {GetMethodName(then)}", OutputLevel.Information);
            SafeExecute(then);
            WriteLine($"Executed Then {GetMethodName(then)}", OutputLevel.Information);
        }

        public async Task ThenAsync(Func<SpecContext, Task> then)
        {
            WriteLine($"Executing Then {GetMethodName(then)}", OutputLevel.Information);
            await SafeExecute(then);
            WriteLine($"Executed Then {GetMethodName(then)}", OutputLevel.Information);
        }

        public void After(Action<SpecContext> after)
        {
            WriteLine($"Executing After {GetMethodName(after)}", OutputLevel.Information);
            SafeExecute(after);
            WriteLine($"Executed After {GetMethodName(after)}", OutputLevel.Information);
        }

        public async Task AfterAsync(Func<SpecContext, Task> after)
        {
            WriteLine($"Executing After {GetMethodName(after)}", OutputLevel.Information);
            await SafeExecute(after);
            WriteLine($"Executed After {GetMethodName(after)}", OutputLevel.Information);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
            if (HasException())
                Assert.Fail($"Failed: {_exception}");
        }

        private void SafeExecute(Action<SpecContext> action)
        {
            if (HasException())
            {
                WriteLine($"Exception already occurred.", OutputLevel.Information);
                return;
            }

            try
            {
                action(_context);
            }
            catch (Exception ex)
            {
                _exception = ex;
                WriteLine($"Exception executing {GetMethodName(action)}: {ex}", OutputLevel.Error);
            }
        }

        private async Task SafeExecute(Func<SpecContext, Task> func)
        {
            if (HasException())
            {
                WriteLine($"Exception already occurred.", OutputLevel.Information);
                return;
            }

            try
            {
                await func(_context);
            }
            catch (Exception ex)
            {
                _exception = ex;
                WriteLine($"Exception executing {GetMethodName(func)}: {ex}", OutputLevel.Error);
            }
        }

        private static void WriteLine(string message, OutputLevel level)
        {
            Console.WriteLine(message);
            ConsoleOutput.Instance.WriteLine(message, level);
        }

        private static string GetMethodName(Delegate @delegate)
        {
            var displayName = @delegate.GetMethodInfo().GetCustomAttribute<DisplayNameAttribute>();
            return displayName != null
                ? displayName.DisplayName
                : @delegate.GetMethodInfo().Name;
        }

        private bool HasException()
        {
            return _exception != null;
        }
    }
}
