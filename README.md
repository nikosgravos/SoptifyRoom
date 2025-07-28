# Roomiify - Spotify Room

Roomiify is a web app that turns your Spotify music taste into a personalized virtual room using your stats and listening habits, complete with themed visuals and a pet companion.  
Built with the **ASP.NET Core** framework, this project was developed as part of a university thesis at the University of Piraeus. The thesis paper and a demo video can be found on the following link.
[Repository Dione of University of Piraeus](https://dione.lib.unipi.gr/xmlui/handle/unipi/17920)

You can improve and modify this project in any way. A room template is provided so you can also create new 3D rooms in **Blender**.


## 🚀 Features

- Connect your Spotify account
- Explore and interact with music-themed digital rooms
- Meet a genre-inspired pet companion
- Discover your listening statistics


## 🛠️ Getting Started

1. **Create a Spotify App**  
   - Go to the [Spotify Developer Dashboard](https://developer.spotify.com/dashboard) 
   - Create a new app and set the redirect URI to: `https://localhost:5000/callback` 
   - Make sure to enable the following APIs: Web API, Web Playback SDK, Android, Ads API, iOS  

2. **Clone the Project**  
   - Clone the repository to your local machine.
   - Open the project folder using Visual Studio or your preferred IDE.

3. **Configure Your Credentials**  
   - Open the `appsettings.json` file.
   - Replace the placeholder values for `ClientId` and `ClientSecret` with your Spotify app credentials from the Developer Dashboard.

4. **Run the App**  
   - Open the `.csproj file` in your IDE.
   - Run the project.
   

## 📄 License

- Source code: [MIT License](LICENSE)  
- Visual assets: [CC BY-NC 4.0](ASSETS_LICENSE) – Non-commercial use only
