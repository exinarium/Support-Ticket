using Microsoft.JSInterop;

namespace SupportTicket.Client.Services;

public interface ISessionStorageService
{
    Task<T?> GetItemAsync<T>(string key);
    Task SetItemAsync<T>(string key, T value);
    Task RemoveItemAsync(string key);
    Task ClearAsync();
}

public class SessionStorageService(IJSRuntime jsRuntime) : ISessionStorageService
{
    public async Task<T?> GetItemAsync<T>(string key)
    {
        var json = await jsRuntime.InvokeAsync<string?>("sessionStorage.getItem", key);
        return json == null ? default : System.Text.Json.JsonSerializer.Deserialize<T>(json);
    }

    public async Task SetItemAsync<T>(string key, T value)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(value);
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", key, json);
    }

    public async Task RemoveItemAsync(string key)
    {
        await jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", key);
    }

    public async Task ClearAsync()
    {
        await jsRuntime.InvokeVoidAsync("sessionStorage.clear");
    }
}