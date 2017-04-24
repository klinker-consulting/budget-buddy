using System;
using System.Diagnostics;
using System.IO;

namespace Specs.General.Servers
{
    public class ApiServer : IDisposable
    {
        private Process _process;

        public void Start()
        {
            var startInfo = GetStartInfo();
            _process = Process.Start(startInfo);
            if (_process.HasExited)
                throw new Exception("Unable to start Api");
        }

        private static ProcessStartInfo GetStartInfo()
        {
            return new ProcessStartInfo
            {
                WorkingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "..", "src", "Api"),
                FileName = "dotnet",
                Arguments = "run"
            };
        }

        public void Dispose()
        {
            _process?.Kill();
        }
    }
}
