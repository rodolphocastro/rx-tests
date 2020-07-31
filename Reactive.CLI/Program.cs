using System;
using System.Reactive.Linq;

using Reactive.CLI.Connectors;
using Reactive.CLI.Workers;

using Refit;

namespace Reactive.CLI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Creating API Services");
            var api = RestService.For<ITodoApi>("https://jsonplaceholder.typicode.com/");
            Console.WriteLine("API Created for JsonPlaceholder");

            IWorker<User> userWorker = new UsersWorker(api);
            var distinctUser = userWorker.State
                .Distinct(u => u.Id)
                .Subscribe(u => Console.WriteLine("A wild {0} appears!", u.Name));

            Console.WriteLine($"{DateTimeOffset.UtcNow.ToLocalTime()} -> Main Thread is alive");
            using (userWorker.Run())
            {
                Console.WriteLine("Press a key to end");
                Console.ReadLine();
            }
        }
    }
}
