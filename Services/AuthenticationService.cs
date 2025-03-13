using ChatWithMeWindows.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithMeWindows.Services
{
    public class AuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://yourserver.com/");
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            var credentials = new { username, password };
            var content = new StringContent(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/login", content);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(jsonResponse);
            return user;
        }

        public async Task<bool> RegisterAsync(string username, string password)
        {
            var newUser = new { username, password };
            var content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/register", content);
            return response.IsSuccessStatusCode;
        }
    }
}
