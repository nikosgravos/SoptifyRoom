using SpotifyRoomAppGitHub;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Configure Spotify settings
builder.Services.Configure<SpotifyConfig>(builder.Configuration.GetSection("Spotify"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Redirect root URL to the Authorize page
app.MapGet("/", () => Results.Redirect("/Connect"));

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();