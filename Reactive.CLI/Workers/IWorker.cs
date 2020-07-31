using System;

namespace Reactive.CLI.Workers
{
    public interface IWorker<T> where
        T : class
    {
        IObservable<T> State { get; }
        IDisposable Run();
    }
}
