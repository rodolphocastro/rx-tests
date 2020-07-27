using System;
using System.Threading.Tasks;

using Reactive.CLI.Connectors;

using Refit;

namespace Reactive.CLI
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Creating API Services");
            var api = RestService.For<ITodoApi>("https://jsonplaceholder.typicode.com/");
            Console.WriteLine("API Created for JsonPlaceholder");
        }
    }
}
