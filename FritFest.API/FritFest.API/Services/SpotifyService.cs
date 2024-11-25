using System.Net.Http.Headers;
using System.Text.Json;

namespace FritFest.API.Services
{
    public class SpotifyService
    {
        private readonly string _clientId = "605ddd30f52f44cc95ad47cc0e1c9242";
        private readonly string _clientSecret = "5e57dc96c4404f0da55ca3dca9acd9f9";

        public async Task<string> GetSpotifyArtistDetails(string apiCode)
        {
            var accessToken = await GetSpotifyAccessToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new Exception("Unable to retrieve Spotify access token");
            }

            var apiUrl = $"https://api.spotify.com/v1/artists/{apiCode}";

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        private async Task<string> GetSpotifyAccessToken()
        {
            using var httpClient = new HttpClient();

            var tokenUrl = "https://accounts.spotify.com/api/token";
            var requestBody = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("grant_type", "client_credentials")
            });

            var authenticationString = $"{_clientId}:{_clientSecret}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(authenticationString));

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);

            var response = await httpClient.PostAsync(tokenUrl, requestBody);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var responseJson = JsonSerializer.Deserialize<JsonElement>(responseBody);

            return responseJson.GetProperty("access_token").GetString();
        }
    }
}
