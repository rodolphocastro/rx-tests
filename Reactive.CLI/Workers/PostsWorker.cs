using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

using Reactive.CLI.Connectors;

namespace Reactive.CLI.Workers
{
    public class PostsWorker : BaseWorker, IWorker<Post>
    {
        private BehaviorSubject<Post> _post = new BehaviorSubject<Post>(new Post { });

        public PostsWorker(ITodoApi api) : base(api)
        {
        }

        public IObservable<Post> State => _post;

        public IDisposable Run() => Observable
            .Interval(TimeSpan.FromSeconds(30))
            .Timestamp()
            .Subscribe(async l =>
            {
                Console.WriteLine("{0} Fetching posts from API", l.Timestamp.ToLocalTime());
                var posts = await Api.ListPosts();
                foreach (var post in posts)
                {
                    _post.OnNext(post);
                }
            });
    }
}
