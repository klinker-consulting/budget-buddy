using System;
using System.Net.Http;
using Specs.General;
using Specs.General.Servers;

namespace Specs
{
    public static class SpecContextExtensions
    {
        public static HttpClient CreateClient(this SpecContext context)
        {
            return new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000")
            };
        }

        public static void StartApiServer(this SpecContext context)
        {
            var server = context.Get<ApiServer>();
            if (server == null)
                context.Set(new ApiServer());

            context.Get<ApiServer>().Start();
        }
    }
}
