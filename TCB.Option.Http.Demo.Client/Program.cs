using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TCB.Option.Http.Demo.Client
{
    internal static class Program
    {
        private static async Task Main()
        {
            var client = new HttpClient();

            const string requestUri = "https://localhost:5001/demo";

            var response = await SendRequest(client, requestUri)
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                Console.WriteLine($"Request to '{requestUri}' failed. ({(int)response.StatusCode}) '{response.StatusCode}'");

            var jsonContent = await response.Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            var content = DeserializeJson<ServerResponse>(requestUri, jsonContent);

            Console.WriteLine($"Response returned at: '{content.Date.ToLongTimeString()}'." +
                              $"{Environment.NewLine}Summary: '{content.Summary}'");
        }

        private static async Task<HttpResponseMessage> SendRequest(HttpClient client, string requestUri)
        {
            try
            {
                return await client
                    .GetAsync(requestUri)
                    .ConfigureAwait(false);
            }
            catch (HttpRequestException)
            {
                Console.WriteLine($"Request to '{requestUri}' failed.");
                throw;
            }
        }

        private static T DeserializeJson<T>(string requestUri, string jsonContent)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonContent);
            }
            catch (JsonException)
            {
                Console.WriteLine($"Request to '{requestUri}' failed to deserialize response as JSON." +
                                  $"{Environment.NewLine}{jsonContent}");
                throw;
            }
        }
    }
}
