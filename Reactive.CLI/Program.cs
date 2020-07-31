using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Reactive.CLI.Connectors;
using Reactive.CLI.Workers;

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

            var userWorker = new UsersWorker(api);
            var distinctUser = userWorker.User.Distinct(u => u.Id).Subscribe(u => Console.WriteLine("A wild {0} appears!", u.Name));

            Console.WriteLine($"{DateTimeOffset.UtcNow.ToLocalTime()} -> Main Thread is alive");
            await userWorker.DoWork();


        }
    }
}
