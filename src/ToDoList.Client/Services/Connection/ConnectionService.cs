using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ToDoList.Client.Services.Connection;

public class ConnectionService
{
    private readonly ConnectionData _connectionData;
    protected readonly ILogger<ConnectionService>? _logger;

    protected static readonly HttpClient _client = new HttpClient();
    private const int _pingInterval = 2000;
    private Timer _pingTimer;
    private bool _status = false;

    public event EventHandler<bool> StatusEventHandler = null!;

    public ConnectionService(ConnectionData connectionData, ILogger<ConnectionService>? logger)
    {
        _connectionData = connectionData;
        _logger = logger;

        _client.BaseAddress = new Uri(_connectionData.BaseURL);
        _client.DefaultRequestHeaders.Accept.Clear();

        _client.DefaultRequestHeaders.Accept
            .Add(new MediaTypeWithQualityHeaderValue("application/json"));

        _pingTimer = new Timer(async x => await Ping(_connectionData.PingableEndpoint), null, _pingInterval, Timeout.Infinite);
    }

    private async Task Ping(string pingableEndpoint)
    {
        try
        {
            var response = await _client.GetAsync(pingableEndpoint);

            if (response.IsSuccessStatusCode)
                _status = true;
            else
                _status = false;
        }
        catch (Exception)
        {
            _status = false;
            _logger?.LogInformation("Endpoint {pingableEndpoint} is not accessible, ping failed", pingableEndpoint);
        }
        finally
        {
            StatusEventHandler?.Invoke(this, _status);
            _pingTimer?.Change(_pingInterval, Timeout.Infinite);
        }
    }

    public async Task<List<T>> GetAsync<T>(string endpoint)
    {
        List<T>? items;
        HttpResponseMessage response = await _client.GetAsync(endpoint);

        if (response.IsSuccessStatusCode)
        {
            items = await response.Content.ReadFromJsonAsync<List<T>>();
        }
        else
        {
            var error = await response.Content.ReadFromJsonAsync<Exception>();
            throw new ArgumentException(error?.Message);
        }

        return items!;
    }

    public async Task<string> GetAsJsonAsync<T>(string endpoint)
    {
        string? content;
        HttpResponseMessage response = await _client.GetAsync(endpoint);

        if (response.IsSuccessStatusCode)
        {
            content = await response.Content.ReadAsStringAsync();
        }
        else
        {
            var error = await response.Content.ReadFromJsonAsync<Exception>();
            throw new ArgumentException(error?.Message);
        }

        return content!;
    }

    public async Task<T> GetSingleAsync<T>(string endpoint)
    {
        T? item;
        HttpResponseMessage response = await _client.GetAsync(endpoint);

        if (response.IsSuccessStatusCode)
        {
            item = await response.Content.ReadFromJsonAsync<T>();
        }
        else
        {
            var error = await response.Content.ReadFromJsonAsync<Exception>();
            throw new ArgumentException(error?.Message);
        }

        return item!;
    }

    public async Task<T> GetAsync<T>(int id, string endpoint)
    {
        T? item;
        HttpResponseMessage response = await _client.GetAsync(endpoint + "/" + id.ToString());

        if (response.IsSuccessStatusCode)
        {
            item = await response.Content.ReadFromJsonAsync<T>();
        }
        else
        {
            var error = await response.Content.ReadFromJsonAsync<Exception>();
            throw new ArgumentException(error?.Message);
        }

        return item!;
    }

    public async Task PostAsync<T>(T item, string endpoint)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync(endpoint, item);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<Exception>();
            _logger?.LogInformation("[POST ERROR] " + error?.Message);

            throw new ArgumentException(error?.Message);
        }
    }

    /// <summary>
    /// Returns the Json data as a string.
    /// </summary>
    /// <param name="item">JSON serialized input</param>
    /// <param name="endpoint">Endpoint</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<string> PostAsJsonAsync(string item, string endpoint)
    {
        string? content;
        HttpResponseMessage response = await _client.PostAsJsonAsync(endpoint, item);

        if (response.IsSuccessStatusCode)
        {
            content = await response.Content.ReadAsStringAsync();
        }
        else
        {
            var error = await response.Content.ReadFromJsonAsync<Exception>();
            throw new ArgumentException(error?.Message);
        }

        return content!;
    }

    public async Task DeleteAsync(int id, string endpoint)
    {
        HttpResponseMessage response = await _client.DeleteAsync(endpoint + "/" + id.ToString());

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<Exception>();
            throw new ArgumentException(error?.Message);
        }
    }

    public async Task PutAsync<T>(T item, string endpoint)
    {
        HttpResponseMessage response = await _client.PutAsJsonAsync(endpoint, item);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<Exception>();
            throw new ArgumentException(error?.Message);
        }
    }
}
