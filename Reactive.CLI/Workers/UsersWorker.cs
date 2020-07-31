using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

using Reactive.CLI.Connectors;

namespace Reactive.CLI.Workers
{
    public class UsersWorker : BaseWorker, IWorker<User>
    {
        private ReplaySubject<User> _user = new ReplaySubject<User>();

        public UsersWorker(ITodoApi api) : base(api) { }

        public IObservable<User> State => _user;

        public IDisposable Run() => Observable
            .Interval(TimeSpan.FromSeconds(15))
            .Timestamp()
            .Subscribe(async l =>
            {
                Console.WriteLine("{0} Fetching users from API", l.Timestamp.ToLocalTime());
                var users = await Api.ListUsers();
                foreach (var user in users)
                {
                    _user.OnNext(user);
                }
            });
    }
}
