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
    // TODO HIGH refactor 
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
            _logger?.LogInformation("Endpoint {pingableEndpoint} is not accessible on {ip}", pingableEndpoint, _connectionData.BaseURL);
        }
        finally
        {
            StatusEventHandler?.Invoke(this, _status);
            _pingTimer?.Change(_pingInterval, Timeout.Infinite);
        }
    }

    public async Task<T?> GetAsync<T>(string uri)
    {
        HttpResponseMessage response = await _client.GetAsync(uri);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<T>();
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            _logger?.LogInformation("[GET-EX] " + error);
            throw new InvalidOperationException(error);
        }
    }

    public async Task<object?> PostAsync<T>(string uri, T item)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync(uri, item);

        if (response.IsSuccessStatusCode)
        {
            // Returns the Id of the created object
            object? id = await response.Content.ReadFromJsonAsync<object?>();
            return id;
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            _logger?.LogInformation("[POST-EX] " + error);
            throw new InvalidOperationException(error);
        }
    }

    public async Task DeleteAsync(string uri)
    {
        HttpResponseMessage response = await _client.DeleteAsync(uri);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            _logger?.LogInformation("[DELETE-EX] " + error);
            throw new InvalidOperationException(error);
        }
    }

    public async Task PutAsync<T>(string uri, T item)
    {
        HttpResponseMessage response = await _client.PutAsJsonAsync(uri, item);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            _logger?.LogInformation("[PUT-EX] " + error);
            throw new InvalidOperationException(error);
        }
    }
}
