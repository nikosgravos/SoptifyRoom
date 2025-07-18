using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SpotifyRoomAppGitHub;

namespace SpotifyRoomApp.Pages
{
    public class CallbackModel : PageModel
    {
        private readonly SpotifyConfig _spotifyConfig;

        public CallbackModel(IOptions<SpotifyConfig> spotifyConfig)
        {
            _spotifyConfig = spotifyConfig.Value;
        }

        public async Task<IActionResult> OnGetAsync(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("Authorization code is missing.");
            }

            using (var client = new HttpClient())
            {
                var tokenRequest = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("code", code),
                    new KeyValuePair<string, string>("redirect_uri", _spotifyConfig.RedirectUri),
                    new KeyValuePair<string, string>("client_id", _spotifyConfig.ClientId),
                    new KeyValuePair<string, string>("client_secret", _spotifyConfig.ClientSecret),
                });

                var response = await client.PostAsync("https://accounts.spotify.com/api/token", tokenRequest);
                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

                if (tokenResponse != null)
                {
                    string accessToken = tokenResponse.AccessToken;
                    return RedirectToPage("/Room", new { token = accessToken });
                }
            }

            return BadRequest("Token exchange failed.");
        }
    }

    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}