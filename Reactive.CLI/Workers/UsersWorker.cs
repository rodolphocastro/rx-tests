using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;

using Reactive.CLI.Connectors;

namespace Reactive.CLI.Workers
{
    public class UsersWorker : BaseWorker, IWorker
    {
        private ReplaySubject<User> _user = new ReplaySubject<User>();

        public UsersWorker(ITodoApi api) : base(api)
        {
        }

        public IObservable<User> User => _user;
        public IObservable<IEnumerable<User>> Users => _user.ToList();

        public async Task DoWork(CancellationToken cancellationToken = default)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Checking API for users");

                Api
                    .ListUsers()
                    .ToObservable()
                    .Subscribe(us =>
                    {
                        foreach (var user in us)
                        {
                            _user.OnNext(user);
                        }
                    });


                await Wait();
            }
        }
    }
}
