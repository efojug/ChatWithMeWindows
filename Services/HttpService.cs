using ChatWithMeWindows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatWithMeWindows.Services
{
    internal class HttpService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public HttpService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(new { username, password }),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/login", content);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(jsonResponse);
        }


    public async Task<User> RegisterAsync(string username, string password)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(new { username, password }),
                Encoding.UTF8,
                "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/register", content);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(jsonResponse);
        }
    }
}