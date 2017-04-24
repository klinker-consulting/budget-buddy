using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Specs
{
    public static class HttpClientExtensions
    {
        public static async Task<T> GetAsync<T>(this HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
