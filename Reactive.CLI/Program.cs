using System;
using System.Linq;
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
            IWorker<Post> postWorker = new PostsWorker(api);

            var distinctUsers = userWorker.State
                .Distinct(u => u.Id);
            distinctUsers.Subscribe(u => Console.WriteLine("A wild {0} appears!", u.Name));

            var distinctPosts = postWorker.State
                .Distinct(p => p.Id);

            distinctPosts
                .Subscribe(p =>
                    distinctUsers
                        .Where(u => u.Id == p.UserId)
                        .Subscribe(u => Console.WriteLine("Post {0} belongs to {1}", p.Title, u.Name)));
            Console.WriteLine($"{DateTimeOffset.UtcNow.ToLocalTime()} -> Main Thread is alive");
            postWorker.Run();
            userWorker.Run();

            Console.WriteLine("Press a key to end");
            Console.ReadLine();

        }
    }
}
