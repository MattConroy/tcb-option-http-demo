using System;
using System.Net.Http;
using System.Threading.Tasks;
using TCB.Option.Tasks;

namespace TCB.Option.Http.Demo.Client
{
    internal static class Program
    {
        private static async Task Main()
        {
            var client = new HttpClient();

            const string requestUri = "https://localhost:5001/demo";

            await client.Request()
                .GetAsync<ServerResponse>(requestUri)
                .MatchAsync(OnRequestSuccess, OnRequestFailure)
                .ConfigureAwait(false);
        }

        private static void OnRequestSuccess(ServerResponse serverResponse)
        {
            Console.WriteLine($"Response returned at: '{serverResponse.Date.ToLongTimeString()}'." +
                              $"{Environment.NewLine}Summary: '{serverResponse.Summary}'");
        }

        private static void OnRequestFailure(ErrorMessage errorMessage)
        {
            throw new DemoRequestException(errorMessage);
        }
    }
}
