using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Refit;

namespace Reactive.CLI.Connectors
{
    /// <summary>
    /// Interface for https://jsonplaceholder.typicode.com/
    /// </summary>
    public interface ITodoApi
    {
        [Get("/users")]
        Task<IEnumerable<User>> ListUsers(CancellationToken cancellationToken = default);
        [Get("/posts")]
        Task<IEnumerable<Post>> ListPosts(CancellationToken cancellation = default);
    }
}
