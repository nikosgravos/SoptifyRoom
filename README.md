# Roomiify - Spotify Room <img width="25" height="25" alt="RoomLogo" src="https://github.com/user-attachments/assets/79bef26c-1ee8-4ddd-8b95-2c6f33f0ae60"/>

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

- **Requirements**
   1. [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) must be installed.

- **Create a Spotify App**  
   1. Go to the [Spotify Developer Dashboard](https://developer.spotify.com/dashboard) 
   2. Create a new app and set the redirect URI to: `https://localhost:5000/callback` 
   3. Make sure to enable the following APIs: Web API, Web Playback SDK, Android, Ads API, iOS  

- **Clone the Project**  
   1. Clone the repository to your local machine.

- **Configure Your Credentials**  
   1. Open the `appsettings.json` file.
   2. Replace the placeholder values for `ClientId` and `ClientSecret` with your Spotify app credentials from the Developer Dashboard.

- **Running the App**  

   1. **Option A:** Using Visual Studio
      1. Open the `.csproj` file.
      2. Run the project.
   1. **Option B:** Without Visual Studio (Command Line)
      1. Open a terminal or command prompt.
      2. Navigate to the project folder:
         ```bash
         cd SpotifyRoomApp
      3. Run the app:
         ```bash
         dotnet run --urls "https://localhost:5000"
      4. Open your browser and go to: https://localhost:5000
        
   

## 📄 License

- Source code: [MIT License](LICENSE)  
- Visual assets: [CC BY-NC 4.0](ASSETS_LICENSE) – Non-commercial use only
