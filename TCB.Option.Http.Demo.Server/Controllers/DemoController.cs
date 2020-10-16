using Microsoft.AspNetCore.Mvc;
using System;

namespace TCB.Option.Http.Demo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        [HttpGet]
        public Response Get()
        {
            return new Response
            {
                Date = DateTime.UtcNow,
                Summary = GetRandomSummary()
            };
        }

        private static string GetRandomSummary()
        {
            var summaries = new[]
            {
                "first summary",
                "second summary",
                "third summary",
                "fourth summary",
                "fifth summary",
                "sixth summary",
            };
            return summaries[GetRandomIndex(summaries.Length)];
        }

        private static int GetRandomIndex(int max) =>
            new Random().Next(max);
    }
}
