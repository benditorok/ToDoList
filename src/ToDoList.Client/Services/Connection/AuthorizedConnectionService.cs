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
    private TokenInfo _userTokenInfo;
    private Timer? _refreshTimer;

    // TODO add logic
    public bool IsLoggedIn { get; private set; } = false;

    public string? UserName { get; private set; }

    public AuthorizedConnectionService(ConnectionData connectionData, ILogger<ConnectionService>? logger, TokenInfo tokenInfo) : base(connectionData, logger)
    {
        _userTokenInfo = tokenInfo;
    }

    /// <summary>
    /// Set the access token of the http header.
    /// </summary>
    private async Task UpdateHeaderToken()
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_userTokenInfo.TokenType!, _userTokenInfo.AccessToken!);

        var userName = await GetAsync<string?>("user/getuserinfo");
        UserName = userName != null ? userName : throw new ArgumentNullException(nameof(userName));
        IsLoggedIn = true;

        _logger?.LogInformation("Account bearer token updated.");
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
                refreshToken = _userTokenInfo.RefreshToken
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
                _userTokenInfo.TokenType = root.GetProperty("tokenType").GetString();
                _userTokenInfo.AccessToken = root.GetProperty("accessToken").GetString();
                _userTokenInfo.RefreshToken = root.GetProperty("refreshToken").GetString();
                _userTokenInfo.ExpiresIn = root.GetProperty("expiresIn").GetInt32() * 1000;

                // Update the token of the header
                await UpdateHeaderToken();
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, ex.Message);
        }
        finally
        {
            _refreshTimer?.Change(_userTokenInfo.ExpiresIn, Timeout.Infinite);
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
            _userTokenInfo.TokenType = root.GetProperty("tokenType").GetString();
            _userTokenInfo.AccessToken = root.GetProperty("accessToken").GetString();
            _userTokenInfo.RefreshToken = root.GetProperty("refreshToken").GetString();
            _userTokenInfo.ExpiresIn = root.GetProperty("expiresIn").GetInt32() * 1000;

            // Update the token of the header
            await UpdateHeaderToken();

            // Create a timer to refresh the access token
            _refreshTimer = new Timer(async x => await RefreshTokenAsync(), null, _userTokenInfo.ExpiresIn, Timeout.Infinite);
            _logger?.LogInformation("Account login successful.");
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new ConnectionServiceException(error);
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
            _logger?.LogInformation("Account registration successful.");
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new ConnectionServiceException(error);
        }
    }

    public void LogoutClearToken()
    {
        IsLoggedIn = false;
        UserName = null;
        _userTokenInfo = new();
        _client.DefaultRequestHeaders.Authorization = null;

        _logger?.LogInformation("Account logged out.");
    }
}
