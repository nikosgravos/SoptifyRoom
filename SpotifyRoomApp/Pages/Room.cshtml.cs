using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace SpotifyRoomApp.Pages
{
    public class RoomModel : PageModel
    {
        public List<Track> TopTracks { get; set; }
        public string ErrorMessage { get; set; }
        public List<GenreCount> TrackGenreList { get; set; }
        public string UserNickname { get; set; }
        public string UserRoom { get; set; }
        public string UserPet { get; set; }
        public string PetName { get; set; }
        public string PetDescription { get; set; }
        public ArtistDetail TopArtist { get; set; }
        public string Username { get; set; }
        public List<RoomInfo> UserRoomInfos { get; private set; } = new();

        private Dictionary<string, (string nickname, string room, string pet, string petName, string petDescription)> genreData =
            new Dictionary<string, (string nickname, string room, string pet, string petName, string petDescription)>
            {
                // POP & Subgenres
                { "pop", ("Main Character Listener", "PopRoomFinal2_4K", "PopRoomPet", "Sunny", "Sunshine in fur form — Sunny lives for sparkles, high notes, and Instagrammable moments.") },
                { "pov: indie", ("First-Person Feels", "PopRoomFinal2_4K", "PopRoomPet", "Sunny", "Sunshine in fur form — Sunny lives for sparkles, high notes, and Instagrammable moments.") },
                { "uk pop", ("Brit­Beat Barista", "PopRoomFinal2_4K", "PopRoomPet", "Sunny", "Sunshine in fur form — Sunny lives for sparkles, high notes, and Instagrammable moments.") },
                { "art pop", ("Canvas & Chorus", "PopRoomFinal2_4K", "PopRoomPet", "Sunny", "Sunshine in fur form — Sunny lives for sparkles, high notes, and Instagrammable moments.") },
                { "singer-songwriter", ("Lyric Diary DJ", "PopRoomFinal2_4K", "PopRoomPet", "Sunny", "Sunshine in fur form — Sunny lives for sparkles, high notes, and Instagrammable moments.") },
                { "alt z", ("Gen Z Zeitgeist", "PopRoomFinal2_4K", "PopRoomPet", "Sunny", "Sunshine in fur form — Sunny lives for sparkles, high notes, and Instagrammable moments.") },
                { "singer-songwriter pop", ("Acoustic Anthemeer", "PopRoomFinal2_4K", "PopRoomPet", "Sunny", "Sunshine in fur form — Sunny lives for sparkles, high notes, and Instagrammable moments.") },
                { "new wave pop", ("Retro-Wave Raver", "PopRoomFinal2_4K", "PopRoomPet", "Sunny", "Sunshine in fur form — Sunny lives for sparkles, high notes, and Instagrammable moments.") },
                { "italian pop", ("Dolce Vibe Maestro", "PopRoomFinal2_4K", "PopRoomPet", "Sunny", "Sunshine in fur form — Sunny lives for sparkles, high notes, and Instagrammable moments.") },
                { "indie pop", ("Bedroom Beat Dreamer", "PopRoomFinal2_4K", "PopRoomPet", "Sunny", "Sunshine in fur form — Sunny lives for sparkles, high notes, and Instagrammable moments.") },

                // RAP & Subgenres
                { "rap", ("Knows Every Lyric", "RapRoomFinal1_4K", "RapRoomPet", "Savage", "Gritty growls and no skips — Savage is a real one, stomping through verses like it's nothing.") },
                { "pop urbaine", ("City Slick Cypher", "RapRoomFinal1_4K", "RapRoomPet", "Savage", "Gritty growls and no skips — Savage is a real one, stomping through verses like it's nothing.") },

                // ROCK & Subgenres
                { "rock", ("Still in My Alt Era", "RockRoomFinal3_4K", "RockRoomPet", "Axel", "Loud, classic, and ready to smash a stage monitor — Axel is your mosh-pit bulldog hero.") },
                { "classic rock", ("Vinyl Road Warrior", "RockRoomFinal3_4K", "RockRoomPet", "Axel", "Loud, classic, and ready to smash a stage monitor — Axel is your mosh-pit bulldog hero.") },
                { "permanent wave", ("Eternal Riff Rider", "RockRoomFinal3_4K", "RockRoomPet", "Axel", "Loud, classic, and ready to smash a stage monitor — Axel is your mosh-pit bulldog hero.") },
                { "album rock", ("LP Lockdown", "RockRoomFinal3_4K", "RockRoomPet", "Axel", "Loud, classic, and ready to smash a stage monitor — Axel is your mosh-pit bulldog hero.") },
                { "soft rock", ("Candlelight Chordsmith", "RockRoomFinal3_4K", "RockRoomPet", "Axel", "Loud, classic, and ready to smash a stage monitor — Axel is your mosh-pit bulldog hero.") },
                { "hard rock", ("Amp Breaker", "RockRoomFinal3_4K", "RockRoomPet", "Axel", "Loud, classic, and ready to smash a stage monitor — Axel is your mosh-pit bulldog hero.") },
                { "mellow gold", ("Sunset String Savior", "RockRoomFinal3_4K", "RockRoomPet", "Axel", "Loud, classic, and ready to smash a stage monitor — Axel is your mosh-pit bulldog hero.") },
                { "alternative rock", ("Nonconformist Noise", "RockRoomFinal3_4K", "RockRoomPet", "Axel", "Loud, classic, and ready to smash a stage monitor — Axel is your mosh-pit bulldog hero.") },
                { "rock en espanol", ("Grito Guitarist", "RockRoomFinal3_4K", "RockRoomPet", "Axel", "Loud, classic, and ready to smash a stage monitor — Axel is your mosh-pit bulldog hero.") },
                { "latin alternative", ("Bilingual Banger", "RockRoomFinal3_4K", "RockRoomPet", "Axel", "Loud, classic, and ready to smash a stage monitor — Axel is your mosh-pit bulldog hero.") },
                { "heartland rock", ("Backroad Balladeer", "RockRoomFinal3_4K", "RockRoomPet", "Axel", "Loud, classic, and ready to smash a stage monitor — Axel is your mosh-pit bulldog hero.") },
                { "pop rock", ("Hooksmith Supreme", "RockRoomFinal3_4K", "RockRoomPet", "Axel", "Loud, classic, and ready to smash a stage monitor — Axel is your mosh-pit bulldog hero.") },
                { "latin rock", ("Ritmo Rocker", "RockRoomFinal3_4K", "RockRoomPet", "Axel", "Loud, classic, and ready to smash a stage monitor — Axel is your mosh-pit bulldog hero.") },
                { "indie rock", ("Basement Breakout", "RockRoomFinal3_4K", "RockRoomPet", "Axel", "Loud, classic, and ready to smash a stage monitor — Axel is your mosh-pit bulldog hero.") },
                { "folk rock", ("Campfire Chord Crafter", "RockRoomFinal3_4K", "RockRoomPet", "Axel", "Loud, classic, and ready to smash a stage monitor — Axel is your mosh-pit bulldog hero.") },

                // URBANO LATINO & Subgenres
                { "urbano latino", ("Bad Bunny Disciple", "UrbanoLatinoRoomFinal1_4K", "UrbanoLatinoRoomPet", "Jowell & Randy", "Always chirping reggaetón classics and switching flows mid-flight — never not vibing.") },
                { "musica mexicana", ("Mariachi Mood Maker", "UrbanoLatinoRoomFinal1_4K", "UrbanoLatinoRoomPet", "Jowell & Randy", "Always chirping reggaetón classics and switching flows mid-flight — never not vibing.") },
                { "latin pop", ("Tecla Trendsetter", "UrbanoLatinoRoomFinal1_4K", "UrbanoLatinoRoomPet", "Jowell & Randy", "Always chirping reggaetón classics and switching flows mid-flight — never not vibing.") },
                { "corrido", ("Ballad Bandito", "UrbanoLatinoRoomFinal1_4K", "UrbanoLatinoRoomPet", "Jowell & Randy", "Always chirping reggaetón classics and switching flows mid-flight — never not vibing.") },
                { "norteno", ("Accordion Outlaw", "UrbanoLatinoRoomFinal1_4K", "UrbanoLatinoRoomPet", "Jowell & Randy", "Always chirping reggaetón classics and switching flows mid-flight — never not vibing.") },
                { "sierreno", ("Sierra Soul Shaker", "UrbanoLatinoRoomFinal1_4K", "UrbanoLatinoRoomPet", "Jowell & Randy", "Always chirping reggaetón classics and switching flows mid-flight — never not vibing.") },
                { "sad sierreno", ("Tear-Stained Trompa", "UrbanoLatinoRoomFinal1_4K", "UrbanoLatinoRoomPet", "Jowell & Randy", "Always chirping reggaetón classics and switching flows mid-flight — never not vibing.") },
                { "banda", ("Brass Boss", "UrbanoLatinoRoomFinal1_4K", "UrbanoLatinoRoomPet", "Jowell & Randy", "Always chirping reggaetón classics and switching flows mid-flight — never not vibing.") },
                { "corridos tumbados", ("Graveyard Groover", "UrbanoLatinoRoomFinal1_4K", "UrbanoLatinoRoomPet", "Jowell & Randy", "Always chirping reggaetón classics and switching flows mid-flight — never not vibing.") },
                { "sertanejo universitario", ("Campus Cowboy", "UrbanoLatinoRoomFinal1_4K", "UrbanoLatinoRoomPet", "Jowell & Randy", "Always chirping reggaetón classics and switching flows mid-flight — never not vibing.") },
                { "sertanejo", ("Country Corazón", "UrbanoLatinoRoomFinal1_4K", "UrbanoLatinoRoomPet", "Jowell & Randy", "Always chirping reggaetón classics and switching flows mid-flight — never not vibing.") },
                { "arrocha", ("Heartache Hustler", "UrbanoLatinoRoomFinal1_4K", "UrbanoLatinoRoomPet", "Jowell & Randy", "Always chirping reggaetón classics and switching flows mid-flight — never not vibing.") },
                { "colombian pop", ("Barranquilla Beat Baron", "UrbanoLatinoRoomFinal1_4K", "UrbanoLatinoRoomPet", "Jowell & Randy", "Always chirping reggaetón classics and switching flows mid-flight — never not vibing.") },
                { "mexican pop", ("Aztec Anthemist", "UrbanoLatinoRoomFinal1_4K", "UrbanoLatinoRoomPet", "Jowell & Randy", "Always chirping reggaetón classics and switching flows mid-flight — never not vibing.") },
                { "ranchera", ("Rancho Romantico", "UrbanoLatinoRoomFinal1_4K", "UrbanoLatinoRoomPet", "Jowell & Randy", "Always chirping reggaetón classics and switching flows mid-flight — never not vibing.") },
                { "latin arena pop", ("Estadio Star", "UrbanoLatinoRoomFinal1_4K", "UrbanoLatinoRoomPet", "Jowell & Randy", "Always chirping reggaetón classics and switching flows mid-flight — never not vibing.") },
                { "agronejo", ("Farmhouse Flowmaster", "UrbanoLatinoRoomFinal1_4K", "UrbanoLatinoRoomPet", "Jowell & Randy", "Always chirping reggaetón classics and switching flows mid-flight — never not vibing.") },
    
                // HIP HOP & Subgenres
                { "hip hop", ("Beats Over Everything", "HipHopRoomFinal4_4K", "HipHopRoomPet", "Remy", "From underground kitchens to rooftop cyphers, Remy’s got bars and hustle in his DNA.") },
                { "southern hip hop", ("Bayou Beat Boss", "HipHopRoomFinal4_4K", "HipHopRoomPet", "Remy", "From underground kitchens to rooftop cyphers, Remy’s got bars and hustle in his DNA.") },
                { "french hip hop", ("Parisian Punchline", "HipHopRoomFinal4_4K", "HipHopRoomPet", "Remy", "From underground kitchens to rooftop cyphers, Remy’s got bars and hustle in his DNA.") },
                { "german hip hop", ("Berlin Bass Baron", "HipHopRoomFinal4_4K", "HipHopRoomPet", "Remy", " From underground kitchens to rooftop cyphers, Remy’s got bars and hustle in his DNA.") },
                { "gangster rap", ("Street Code Conductor", "HipHopRoomFinal4_4K", "HipHopRoomPet", "Remy", "From underground kitchens to rooftop cyphers, Remy’s got bars and hustle in his DNA.") },
                { "conscious hip hop", ("Mind M.C.", "HipHopRoomFinal4_4K", "HipHopRoomPet", "Remy", "From underground kitchens to rooftop cyphers, Remy’s got bars and hustle in his DNA.") },
                { "east coast hip hop", ("Harbor Hard-Hit", "HipHopRoomFinal4_4K", "HipHopRoomPet", "Remy", "From underground kitchens to rooftop cyphers, Remy’s got bars and hustle in his DNA.") },

                // TRAP LATINO & Subgenres
                { "trap latino", ("Perreo & Hustle", "TrapLatinoRoomFinal1_4K", "TrapLatinoRoomPet", "Bad Froggy", "Flashy, unpredictable, and oddly adorable — Bad Froggy’s got street croaks and stage presence.") },
                { "latin hip hop", ("Barrio Beatmaker", "TrapLatinoRoomFinal1_4K", "TrapLatinoRoomPet", "Bad Froggy", "Flashy, unpredictable, and oddly adorable — Bad Froggy’s got street croaks and stage presence.") },
                { "trap argentino", ("Buenos Beat Brigadier", "TrapLatinoRoomFinal1_4K", "TrapLatinoRoomPet", "Bad Froggy", "Flashy, unpredictable, and oddly adorable — Bad Froggy’s got street croaks and stage presence.") },

                // REGGAETON & Subgenres
                { "reggaeton", ("Party Playlist CEO", "ReggaetonRoomFinal1_4k", "ReggaetonRoomPet", "Perreko", "A red and blue parrot with attitude. He mimics reggaeton hooks, bobs to every beat, and brings nonstop perreo vibes wherever he lands.") },
                { "reggaeton colombiano", ("Cartagena Club Captain", "ReggaetonRoomFinal1_4k", "ReggaetonRoomPet", "Perreko", "A red and blue parrot with attitude. He mimics reggaeton hooks, bobs to every beat, and brings nonstop perreo vibes wherever he lands.") },

                // DANCE POP & Subgenres
                { "dance pop", ("Festival Core", "DancePopRoomFinal2_4K", "DancePopRoomPet", "Luma", "Luma lives for bass drops and strobe lights — a nonstop dancer with moves as slick as a festival set.") },
                { "pop dance", ("Floor Frenzy Founder", "DancePopRoomFinal2_4K", "DancePopRoomPet", "Luma", "Luma lives for bass drops and strobe lights — a nonstop dancer with moves as slick as a festival set.") },

                // POP RAP & Subgenres
                { "pop rap", ("Hooks & Hits", "PopRapRoomFinal5_4K", "PopRapRoomPet", "Drizzy", "An owl who knows every hook before it trends — smooth, stylish, and just a little toxic.") },
                { "canadian pop", ("Maple Melody Mixer", "PopRapRoomFinal5_4K", "PopRapRoomPet", "Drizzy", "An owl who knows every hook before it trends — smooth, stylish, and just a little toxic.") },
                { "canadian hip hop", ("North Flow Navigator", "PopRapRoomFinal5_4K", "PopRapRoomPet", "Drizzy", "An owl who knows every hook before it trends — smooth, stylish, and just a little toxic.") },
                { "canadian contemporary r&b", ("Hockey Harmonics", "PopRapRoomFinal5_4K", "PopRapRoomPet", "Drizzy", "An owl who knows every hook before it trends — smooth, stylish, and just a little toxic.") },

                // MODERN ROCK & Subgenres
                { "modern rock", ("Alt Kid Forever", "PresetRoomFinal1_4K", "BetaRoomPet", "Demo", "Demo") },
                { "new romantic", ("Synth Swoonmaker", "PresetRoomFinal1_4K", "BetaRoomPet", "Demo", "Demo") },
                { "new wave", ("Neon Noise Navigator", "PresetRoomFinal1_4K", "BetaRoomPet", "Demo", "Demo") },
                { "neo mellow", ("Chillwave Champion", "PresetRoomFinal1_4K", "BetaRoomPet", "Demo", "Demo") },
                { "modern alternative rock", ("Future Feedback Freak", "PresetRoomFinal1_4K", "BetaRoomPet", "Demo", "Demo") },

                // TRAP & Subgenres
                { "trap", ("Bassline Addict", "TrapRoomFinal2_4K", "TrapRoomPet", "Slime", "Icy slow but heavy in presence, Slime rolls through with lean energy.") },
                { "atl hip hop", ("Peachtree Pounder", "TrapRoomFinal2_4K", "TrapRoomPet", "Slime", "Icy slow but heavy in presence, Slime rolls through with lean energy.") },
                { "chicago rap", ("Windy City Wordsmith", "TrapRoomFinal2_4K", "TrapRoomPet", "Slime", "Icy slow but heavy in presence, Slime rolls through with lean energy.") },

                // ALTERNATIVE METAL & Subgenres
                { "alternative metal", ("Heavy & Hyped", "AlternativeMetalRoomFinal1_4K", "AlternativeMetalRoomPet", "Hades", "Piercing eyes and a scream like thunder — Hades rules the alt-metal skies with silent menace.") },
                { "post-grunge", ("Rust Belt Rumbler", "AlternativeMetalRoomFinal1_4K", "AlternativeMetalRoomPet", "Hades", "Piercing eyes and a scream like thunder — Hades rules the alt-metal skies with silent menace.") },
                { "nu metal", ("Rapcore Riot", "AlternativeMetalRoomFinal1_4K", "AlternativeMetalRoomPet", "Hades", "Piercing eyes and a scream like thunder — Hades rules the alt-metal skies with silent menace.") },
                { "pop punk", ("Skatepark Shouter", "AlternativeMetalRoomFinal1_4K", "AlternativeMetalRoomPet", "Hades", "Piercing eyes and a scream like thunder — Hades rules the alt-metal skies with silent menace.") },
                { "rap metal", ("Rhymes & Riffs Renegade", "AlternativeMetalRoomFinal1_4K", "AlternativeMetalRoomPet", "Hades", "Piercing eyes and a scream like thunder — Hades rules the alt-metal skies with silent menace.") },
                { "metal", ("Sonic Steelheart", "AlternativeMetalRoomFinal1_4K", "AlternativeMetalRoomPet", "Hades", "Piercing eyes and a scream like thunder — Hades rules the alt-metal skies with silent menace.") },

                // K-POP & Subgenres
                { "k-pop", ("Trendsetter Certified", "KPopRoomFinal1_4K", "KPopRoomPet", "Jungkookie", "Glittery, energetic, and dangerously cute — Jungkookie hops in formation with flawless choreography.") },
                { "j-pop", ("Tokyo Tune Titan", "KPopRoomFinal1_4K", "KPopRoomPet", "Jungkookie", "Glittery, energetic, and dangerously cute — Jungkookie hops in formation with flawless choreography.") },
                { "indonesian pop", ("Jakarta Jam Curator", "KPopRoomFinal1_4K", "KPopRoomPet", "Jungkookie", "Glittery, energetic, and dangerously cute — Jungkookie hops in formation with flawless choreography.") },
                { "k-pop boy group", ("Bias Boss", "KPopRoomFinal1_4K", "KPopRoomPet", "Jungkookie", "Glittery, energetic, and dangerously cute — Jungkookie hops in formation with flawless choreography.") },
                { "k-pop girl group", ("Idol Icon", "KPopRoomFinal1_4K", "KPopRoomPet", "Jungkookie", "Glittery, energetic, and dangerously cute — Jungkookie hops in formation with flawless choreography.") },

                // R&B & Subgenres
                { "r&b", ("Vibe Curator", "RnBRoomFinal3_4K", "RnBRoomPet", "Nova", "Velvet-soft with galaxy eyes — Nova floats through silky harmonies and after-hours vibes.") },
                { "urban contemporary", ("City Soul Surgeon", "RnBRoomFinal3_4K", "RnBRoomPet", "Nova", "Velvet-soft with galaxy eyes — Nova floats through silky harmonies and after-hours vibes.") },
                { "soul", ("Velvet Vibe Virtuoso", "RnBRoomFinal3_4K", "RnBRoomPet", "Nova", "Velvet-soft with galaxy eyes — Nova floats through silky harmonies and after-hours vibes.") },

                // EDM & Subgenres
                { "edm", ("Drop Enthusiast", "EDMRoomFinal1_4K", "EDMRoomPet", "Serpz", "Sleek and silent, Serpz flickers with neon patterns and coils to the beat.") },
                { "indietronica", ("Lo-Fi Lantern Lighter", "EDMRoomFinal1_4K", "EDMRoomPet", "Serpz", "Sleek and silent, Serpz flickers with neon patterns and coils to the beat.") },
                { "electro house", ("Bass Bin Baron", "EDMRoomFinal1_4K", "EDMRoomPet", "Serpz", "Sleek and silent, Serpz flickers with neon patterns and coils to the beat.") },
                { "uk dance", ("Brit Beat Brigadier", "EDMRoomFinal1_4K", "EDMRoomPet", "Serpz", "Sleek and silent, Serpz flickers with neon patterns and coils to the beat.") },
                { "slap house", ("Drop & Slap Commander", "EDMRoomFinal1_4K", "EDMRoomPet", "Serpz", "Sleek and silent, Serpz flickers with neon patterns and coils to the beat.") },
                { "house", ("4/4 Floor Filler", "EDMRoomFinal1_4K", "EDMRoomPet", "Serpz", "Sleek and silent, Serpz flickers with neon patterns and coils to the beat.") },

                // MELODIC RAP
                { "melodic rap", ("Auto-Tuned Emotions", "MelodicRapRoomFinal2_4K", "MelodicRapRoomPet", "Uzi", "Icy vocals with emotional depth — Uzi howls over spacey beats and late-night feelings.") },

                // COUNTRY & Subgenres
                { "country", ("Indie Cowboy Energy", "CountryRoomFinal1_4K", "CountryRoomPet", "Walker", "Dust on his boots, melody in his stride — Walker’s a lone rider with a heart of gold and twang to spare.") },
                { "contemporary country", ("Main Street Minstrel", "CountryRoomFinal1_4K", "CountryRoomPet", "Walker", "Dust on his boots, melody in his stride — Walker’s a lone rider with a heart of gold and twang to spare.") },
                { "country road", ("Backroad Balladeer", "CountryRoomFinal1_4K", "CountryRoomPet", "Walker", "Dust on his boots, melody in his stride — Walker’s a lone rider with a heart of gold and twang to spare.") },
                { "modern country pop", ("Boots & Beats Boss", "CountryRoomFinal1_4K", "CountryRoomPet", "Walker", "Dust on his boots, melody in his stride — Walker’s a lone rider with a heart of gold and twang to spare.") },
                { "modern country rock", ("Steel–String Stallion", "CountryRoomFinal1_4K", "CountryRoomPet", "Walker", "Dust on his boots, melody in his stride — Walker’s a lone rider with a heart of gold and twang to spare.") },

            };



        public async Task<IActionResult> OnGetAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                ErrorMessage = "Token is missing or invalid.";
                return Page();
            }

            TopTracks = await GetTopTracks(token);
            TrackGenreList = await GetTrackGenres(token);

            bool matched = false;
            foreach (var genreCount in TrackGenreList)
            {
                var genreKey = genreCount.Genre.ToLower();
                if (genreData.TryGetValue(genreKey, out var data))
                {
                    UserNickname = data.nickname;
                    UserRoom = data.room;
                    UserPet = data.pet;
                    PetName = data.petName;
                    PetDescription = data.petDescription;
                    matched = true;
                    break;
                }
            }

            if (!matched)
            {
                UserNickname = "The Music Explorer";
                UserRoom = "MusicExplorerRoomFinal1_4K";
                UserPet = "MusicExplorerRoomPet";
                PetName = "Scarab";
                PetDescription = "A rare beetle that feeds on hidden sounds. It appears only to true music explorers.";
            }

            UserRoomInfos = TrackGenreList
                .Select(tc => tc.Genre.ToLower())
                .Where(g => genreData.ContainsKey(g))
                .Select(g => {
                    var d = genreData[g];
                    return new RoomInfo
                    {
                        Room = d.room,
                        Nickname = d.nickname,
                        Pet = d.pet,
                        PetName = d.petName,
                        PetDescription = d.petDescription
                    };
                })
                .DistinctBy(r => r.Room)
                .ToList();

            if (!UserRoomInfos.Any())
            {
                var demo = new RoomInfo
                {
                    Room = "MusicExplorerRoomFinal1_4K",
                    Nickname = "The Music Explorer",
                    Pet = "MusicExplorerRoomPet",
                    PetName = "Scarab",
                    PetDescription = "A rare beetle that feeds on hidden sounds. It appears only to true music explorers."
                };
                UserRoomInfos.Add(demo);
            }

            TopArtist = await GetTopArtist(token);
            Username = await GetUsername(token);

            return Page();
        }


        private async Task<List<Track>> GetTopTracks(string token)
        {
            var allTracks = new List<Track>();
            var selectedTracks = new List<Track>();
            var spotifyService = new SpotifyService();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync("https://api.spotify.com/v1/me/top/tracks?limit=15&time_range=long_term");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var topTracksResponse = JsonConvert.DeserializeObject<TopTracksResponse>(content);

                    if (topTracksResponse?.Items != null)
                    {
                        allTracks = topTracksResponse.Items;

                        var random = new Random();
                        selectedTracks = allTracks.OrderBy(x => random.Next()).Take(9).ToList();

                        
                        foreach (var track in selectedTracks)
                        {
                            track.FetchedPreviewUrl = await spotifyService.GetSpotifyPreviewUrlAsync(track.Id);
                        }
                    }
                    else
                    {
                        ErrorMessage = "No top tracks found.";
                    }
                }
                else
                {
                    ErrorMessage = $"Failed to fetch top tracks: {response.StatusCode}";
                }
            }

            return selectedTracks;
        }




        private async Task<ArtistDetail> GetTopArtist(string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync("https://api.spotify.com/v1/me/top/artists?limit=3&time_range=long_term");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var topArtistsResponse = JsonConvert.DeserializeObject<TopArtistsResponse>(content);

                    if (topArtistsResponse.Items != null && topArtistsResponse.Items.Count > 0)
                    {
                        var random = new Random();
                        int randomIndex = random.Next(topArtistsResponse.Items.Count);
                        return topArtistsResponse.Items[randomIndex];
                    }
                }
            }

            return null;
        }


        private async Task<List<GenreCount>> GetTrackGenres(string token)
        {
            var genreCounts = new Dictionary<string, int>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync("https://api.spotify.com/v1/me/top/tracks?limit=15&time_range=long_term");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var topTracksResponse = JsonConvert.DeserializeObject<TopTracksResponse>(content);

                    foreach (var track in topTracksResponse.Items)
                    {
                        foreach (var artist in track.Artists)
                        {
                            var artistResponse = await client.GetAsync($"https://api.spotify.com/v1/artists/{artist.Id}");
                            if (artistResponse.IsSuccessStatusCode)
                            {
                                var artistContent = await artistResponse.Content.ReadAsStringAsync();
                                var artistData = JsonConvert.DeserializeObject<Artist>(artistContent);
                                foreach (var genre in artistData.Genres)
                                {
                                    if (genreCounts.ContainsKey(genre))
                                        genreCounts[genre]++;
                                    else
                                        genreCounts[genre] = 1;
                                }
                            }
                        }
                    }
                }
            }

            return genreCounts.Select(g => new GenreCount { Genre = g.Key, Count = g.Value })
                              .OrderByDescending(g => g.Count)
                              .ToList();
        }

        private async Task<string> GetUsername(string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync("https://api.spotify.com/v1/me");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var profile = JsonConvert.DeserializeObject<UserProfile>(content);
                    return profile.DisplayName ?? "Your";
                }
            }

            return "Your";
        }





    }

    public class SpotifyService
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<string> GetSpotifyPreviewUrlAsync(string spotifyTrackId)
        {
            try
            {
                var embedUrl = $"https://open.spotify.com/embed/track/{spotifyTrackId}";
                var response = await _httpClient.GetStringAsync(embedUrl);

                var match = Regex.Match(response, @"""audioPreview"":\s*{\s*""url"":\s*""([^""]+)""");

                if (match.Success)
                {
                    return match.Groups[1].Value;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to fetch Spotify preview URL: {ex.Message}");
            }

            return null;
        }
    }


    public class TopTracksResponse
    {
        [JsonProperty("items")]
        public List<Track> Items { get; set; }
    }

    public class TopArtistsResponse
    {
        [JsonProperty("items")]
        public List<ArtistDetail> Items { get; set; }
    }

    public class ArtistDetail
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("images")]
        public List<Image> Images { get; set; }
    }

    public class Track
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("album")]
        public Album Album { get; set; }

        [JsonProperty("artists")]
        public List<ArtistReference> Artists { get; set; }

        public string FetchedPreviewUrl { get; set; }

    }

    public class ArtistReference
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Artist
    {
        [JsonProperty("genres")]
        public List<string> Genres { get; set; }
    }

    public class Album
    {
        [JsonProperty("images")]
        public List<Image> Images { get; set; }
    }

    public class Image
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class GenreCount
    {
        public string Genre { get; set; }
        public int Count { get; set; }
    }

    public class UserProfile
    {
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
    }

    public class RoomInfo
    {
        public string Room { get; set; }
        public string Nickname { get; set; }
        public string Pet { get; set; }
        public string PetName { get; set; }
        public string PetDescription { get; set; }
    }


}


