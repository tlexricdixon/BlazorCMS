// =====================
// [Scaffolded Interfaces]
// =====================

namespace Interfaces;
public interface ILocalDatabase
{
    Task InitializeAsync();
    Task<List<T>> GetAllAsync<T>() where T : class;
    Task SaveAsync<T>(T item) where T : class;
}

public interface ISyncService
{
    Task<bool> IsOnlineAsync();
    Task PushChangesAsync();
    Task PullUpdatesAsync();
}
