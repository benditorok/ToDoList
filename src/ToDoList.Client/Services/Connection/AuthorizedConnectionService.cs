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
public class AuthorizedConnectionService : ConnectionService
{
    private string? _tokenType;
    private string? _accessToken;
    private string? _refreshToken;
    private int _expiresIn = int.MaxValue;
    private Timer? _refreshTimer;

    public AuthorizedConnectionService(ConnectionData connectionData, ILogger<ConnectionService>? logger) : base(connectionData, logger)
    {
    }

    /// <summary>
    /// Set the access token of the http header.
    /// </summary>
    private void UpdateHeaderToken()
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_tokenType!, _accessToken);
        _logger?.LogInformation("[AUTH] Updated bearer token");
    }

    /// <summary>
    /// Refreshes the bearer token when it expires using the refresh token.
    /// </summary>
    /// <returns></returns>
    private async Task RefreshTokenAsync()
    {
        try
        {
            var requestData = new
            {
                refreshToken = _refreshToken
            };

            string jsonData = JsonSerializer.Serialize(requestData);
            var requestContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("refresh", requestContent);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                // Get the values from the response
                JsonDocument doc = JsonDocument.Parse(content);
                JsonElement root = doc.RootElement;

                // Set token data
                _tokenType = root.GetProperty("tokenType").GetString();
                _accessToken = root.GetProperty("accessToken").GetString();
                _refreshToken = root.GetProperty("refreshToken").GetString();
                _expiresIn = root.GetProperty("expiresIn").GetInt32() * 1000;

                // Update the token of the header
                UpdateHeaderToken();
            }
        }
        catch (Exception)
        {
            _logger?.LogInformation("[AUTH-EX] Refreshing bearer token failed");
        }
        finally
        {
            _refreshTimer?.Change(_expiresIn, Timeout.Infinite);
        }
    }

    /// <summary>
    /// Log in to the user account using an username/email and a password
    /// </summary>
    /// <param name="username">Username</param>
    /// <param name="password">Password</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task LoginAsync(string username, string password)
    {
        // Json type definition
        var requestData = new
        {
            email = username,
            password = password,
            twoFactorCode = "string",
            twoFactorRecoveryCode = "string"
        };

        // Convert request data to JSON
        string jsonData = JsonSerializer.Serialize(requestData);
        var requestContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PostAsync("login", requestContent);

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();

            // Get the values from the response
            JsonDocument doc = JsonDocument.Parse(content);
            JsonElement root = doc.RootElement;

            // Set token data
            _tokenType = root.GetProperty("tokenType").GetString();
            _accessToken = root.GetProperty("accessToken").GetString();
            _refreshToken = root.GetProperty("refreshToken").GetString();
            _expiresIn = root.GetProperty("expiresIn").GetInt32() * 1000;

            // Update the token of the header
            UpdateHeaderToken();

            // Create a timer to refresh the access token
            _refreshTimer = new Timer(async x => await RefreshTokenAsync(), null, _expiresIn, Timeout.Infinite);

            _logger?.LogInformation("[AUTH] Login successful");
        }
        else
        {
            var error = await response.Content.ReadFromJsonAsync<Exception>();
            _logger?.LogInformation("[AUTH-EX] Login failed: {msg}", error?.Message);

            throw new ArgumentException(error?.Message);
        }
    }
    public async Task RegisterAsync(string username, string password)
    {
        // Json type definition
        var requestData = new
        {
            email = username,
            password = password,
        };

        // Convert request data to JSON
        string jsonData = JsonSerializer.Serialize(requestData);
        var requestContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PostAsync("register", requestContent);

        if (response.IsSuccessStatusCode)
        {
            _logger?.LogInformation("[AUTH] Account registered");
        }
        else
        {
            var error = await response.Content.ReadFromJsonAsync<Exception>();
            _logger?.LogInformation("[AUTH-EX] Registration failed: {msg}", error?.Message);

            throw new ArgumentException(error?.Message);
        }
    }
}
