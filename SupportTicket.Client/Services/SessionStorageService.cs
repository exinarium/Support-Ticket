using Microsoft.JSInterop;

namespace SupportTicket.Client.Services;

public interface ISessionStorageService
{
    Task<T?> GetItemAsync<T>(string key);
    Task SetItemAsync<T>(string key, T value);
    Task RemoveItemAsync(string key);
    Task ClearAsync();
}

public class SessionStorageService : ISessionStorageService
{
    private readonly IJSRuntime _jsRuntime;

    public SessionStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<T?> GetItemAsync<T>(string key)
    {
        var json = await _jsRuntime.InvokeAsync<string?>("sessionStorage.getItem", key);
        return json == null ? default : System.Text.Json.JsonSerializer.Deserialize<T>(json);
    }

    public async Task SetItemAsync<T>(string key, T value)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(value);
        await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", key, json);
    }

    public async Task RemoveItemAsync(string key)
    {
        await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", key);
    }

    public async Task ClearAsync()
    {
        await _jsRuntime.InvokeVoidAsync("sessionStorage.clear");
    }
}