using Blazored.LocalStorage;
using Interfaces;
using System.Net.Http.Json;

namespace Service;
public class SyncService : ISyncService
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _http;

    private const string QueueKey = "offline-queue";

    public SyncService(ILocalStorageService localStorage, HttpClient http)
    {
        _localStorage = localStorage;
        _http = http;
    }

    public async Task EnqueueAsync<T>(T item)
    {
        var queue = await _localStorage.GetItemAsync<List<T>>(QueueKey) ?? new List<T>();
        queue.Add(item);
        await _localStorage.SetItemAsync(QueueKey, queue);
    }

    public async Task SyncAsync<T>(string endpoint)
    {
        var queue = await _localStorage.GetItemAsync<List<T>>(QueueKey);
        if (queue == null || queue.Count == 0)
            return;

        foreach (var item in queue.ToList())
        {
            try
            {
                var response = await _http.PostAsJsonAsync(endpoint, item);
                if (response.IsSuccessStatusCode)
                {
                    queue.Remove(item);
                }
            }
            catch
            {
                // Stay silent on network errors — retry later
                break;
            }
        }

        await _localStorage.SetItemAsync(QueueKey, queue);
    }

    public async Task<bool> IsOnlineAsync()
    {
        try
        {
            var result = await _http.GetAsync("api/health");
            return result.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public Task PushChangesAsync()
    {
        throw new NotImplementedException();
    }

    public Task PullUpdatesAsync()
    {
        throw new NotImplementedException();
    }
}
