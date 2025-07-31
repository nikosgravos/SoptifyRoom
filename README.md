# <img width="30" height="30" alt="RoomLogo" src="https://github.com/user-attachments/assets/79bef26c-1ee8-4ddd-8b95-2c6f33f0ae60"/> Roomiify - Spotify Room 

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

1. **Install the [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).**

2. **Create a Spotify App**  
   - Go to the [Spotify Developer Dashboard](https://developer.spotify.com/dashboard) 
   - Create a new app and set the redirect URI to: `http://[::1]:5000/callback` 
   - Make sure to enable the following APIs: Web API, Web Playback SDK, Android, Ads API, iOS  

3. **Clone the Project**  
   - Clone the repository to your local machine.

4. **Configure Your Credentials**  
   - Open the `appsettings.json` file.
   - Replace the placeholder values for `ClientId` and `ClientSecret` with your Spotify app credentials from the Developer Dashboard.

## 🏃‍♂️‍➡️ Running the App

1. **Using Visual Studio** 
   - Open the `.csproj` file.
   - Run the project.
2. **Without Visual Studio (Command Line)**
   - Open a terminal or command prompt.
   - Navigate to the project folder:
      ```bash
      cd SpotifyRoomApp
      ```
      or find the `SpotifyRoomApp` folder in File Explorer, right click and select Open in Terminal
   - Run the app:
      ```bash
      dotnet run --urls "http://[::1]:5000"
   - Open your browser and go to: http://[::1]:5000

## 🎵 Example Room

<img width="500" height="500" alt="my-music-room" src="https://github.com/user-attachments/assets/2b79db4f-38a5-40f2-a38e-f876e833df69" />

