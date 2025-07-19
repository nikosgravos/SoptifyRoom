# Roomiify - Spotify Room App

Roomiify is a web app that turns your Spotify music taste into a personalized virtual room, complete with themed visuals and a pet companion.  
Built with **ASP.NET Core** in **Visual Studio**, this project was developed as part of a university thesis at the University of Piraeus.  
📄 [Thesis & Demo](https://dione.lib.unipi.gr/xmlui/handle/unipi/17920)

You can improve and modify this project in any way. A room template is provided so you can also create new rooms in Blender.

## 🚀 Features

- Connect your Spotify account  
- Explore music-themed digital rooms  
- Meet a genre-inspired pet companion  


## 🛠️ Getting Started

1. **Create a Spotify App**  
   - Go to the [Spotify Developer Dashboard](https://developer.spotify.com/dashboard)  
   - Create a new app and set the redirect URI to: `https://localhost:5000/callback` 
   - Make sure to enable the following APIs: Web API, Web Playback SDK, Android, Ads API, iOS  

2. **Clone the Project**  
   Open the repo in Visual Studio or any IDE of your choice.

3. **Add Your Credentials**  
   In `appsettings.json`, replace the placeholder `ClientId` and `ClientSecret` with your Spotify app credentials.

4. **Run the App**  
   Launch the `.csproj` in Visual Studio and start the project.  
   The app will open at: `https://localhost:5000`


## 📄 License

- Source code: [MIT License](LICENSE)  
- Visual assets: [CC BY-NC 4.0](ASSETS_LICENSE) – Non-commercial use only
