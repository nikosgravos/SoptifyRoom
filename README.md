# Roomiify - Spotify Room App

Roomiify is a web app that transforms your music taste into a personalized virtual room. 
By connecting your Spotify account, Roomiify analyzes your top tracks and genres to generate a themed digital space — complete with custom visuals and a matching pet companion.
Roomiify is developed using the **ASP.NET Core Web App framework** in **Visual Studio**.

This app was created as part of a university thesis project.  
The thesis paper and a demo video are available online at the University of Piraeus repository:  
https://dione.lib.unipi.gr/xmlui/handle/unipi/17920

## 🚀 Features

- 🎧 Connect with Spotify to fetch your top songs and genres  
- 🏠 Generate a unique room based on your music preferences  
- 🐸 Meet a genre-themed pet companion  
- 🔁 Refresh and explore new room variations  

## 🛠️ Getting Started

To run **Roomiify** locally, follow the steps below:

---

### 1. Create a Spotify Developer Account

1. Go to the [Spotify Developer Dashboard](https://developer.spotify.com/dashboard/)
2. Log in with your Spotify account and click **"Create an App"**
3. Set the **Redirect URI** for your app to:  
   ```
   https://localhost:5000/callback
   ```

---

### 2. Clone the Repository

Clone this project using Git or open it directly in **Visual Studio** or your preferred IDE.

```bash
git clone https://github.com/your-username/roomiify.git
```

---

### 3. Configure Your Spotify Credentials

In the `appsettings.json` file of the project, replace the placeholder values with the credentials from your Spotify Developer app:

```json
"ClientId": "your-client-id-here",
"ClientSecret": "your-client-secret-here"
```

---

### 4. Run the Project

1. Open the `.csproj` file in Visual Studio (or your IDE of choice).
2. Run the application.

The app will launch at `https://localhost:5000` and redirect to Spotify for login when needed.

---

## 📄 License

- The **source code** is licensed under the [MIT License](LICENSE).  
- The **images and other visual assets** are licensed under the [Creative Commons Attribution-NonCommercial 4.0 International License](ASSETS_LICENSE).  
  You may not use them for commercial purposes.
