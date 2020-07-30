using System;
using System.Threading.Tasks;

using Reactive.CLI.Connectors;

namespace Reactive.CLI.Workers
{
    public abstract class BaseWorker
    {
        protected ITodoApi Api { get; private set; }

        protected BaseWorker(ITodoApi api)
        {
            Api = api ?? throw new ArgumentNullException(nameof(api));
        }

        protected Task Wait() => Task.Delay(1000 * 60);
    }
}