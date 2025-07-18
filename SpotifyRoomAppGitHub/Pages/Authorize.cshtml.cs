using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using SpotifyRoomAppGitHub;

namespace SpotifyRoomApp.Pages
{
    public class AuthorizeModel : PageModel
    {
        private readonly SpotifyConfig _spotifyConfig;

        public AuthorizeModel(IOptions<SpotifyConfig> spotifyConfig)
        {
            _spotifyConfig = spotifyConfig.Value;
        }

        public IActionResult OnGet()
        {
            string scope = "user-top-read user-modify-playback-state streaming";
            string authUrl = $"https://accounts.spotify.com/authorize?response_type=code&client_id={_spotifyConfig.ClientId}&redirect_uri={Uri.EscapeDataString(_spotifyConfig.RedirectUri)}&scope={Uri.EscapeDataString(scope)}";

            return Redirect(authUrl);
        }
    }
}