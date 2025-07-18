# Roomiify - Spotify Room App

Roomiify is a web app that transforms your music taste into a personalized virtual room. 
By connecting your Spotify account, Roomiify analyzes your top tracks and genres to generate a themed digital space — complete with custom visuals and a matching pet companion.
Roomiify is developed using the **ASP.NET Core Web App framework** in **Visual Studio**.

## 🚀 Features

- 🎧 Connect with Spotify to fetch your top songs and genres  
- 🏠 Generate a unique room based on your music preferences  
- 🐸 Meet a genre-themed pet companion  
- 🔁 Refresh and explore new room variations  

## 🛠️ Getting Started

To run Roomiify locally, follow these steps:

### 1. Create a Spotify Developer Account

- Go to the Spotify Developer Dashboard: https://developer.spotify.com/dashboard/  
- Log in and create a new app  
- Set the Redirect URI to:  
  https://localhost:5000/callback

### 2. Set Up Your Credentials

- In the `appsettings.json` file of the project, replace the following lines with your own values:

"ClientId": "your-client-id-here",
"ClientSecret": "your-client-secret-here"

## 📄 License

- The **source code** is licensed under the [MIT License](LICENSE).  
- The **images and other visual assets** are licensed under the [Creative Commons Attribution-NonCommercial 4.0 International License](ASSETS_LICENSE).  
  You may not use them for commercial purposes.
