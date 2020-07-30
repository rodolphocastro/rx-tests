using System.Threading;
using System.Threading.Tasks;

namespace Reactive.CLI.Workers
{
    public interface IWorker
    {
        Task DoWork(CancellationToken cancellationToken = default);
    }
}
