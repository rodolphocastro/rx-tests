using System;
using System.Linq;
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
            var users = await api.ListUsers();
            Console.WriteLine("Recovered {0} users from the API", users.Count());
            foreach (var user in users)
            {
                Console.WriteLine("{0} works to {1}", user.Name, user.Company.Name);
            }
        }
    }
}
