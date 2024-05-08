using Microsoft.AspNetCore.Connections;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseWebSocketTunnel(o => o.MaxConnectionCount = 10);
builder.WebHost.ConfigureKestrel(o =>
{
    
    // Add the endpoint
    o.Listen(new UriEndPoint(new("https://88c2-70-126-4-249.ngrok-free.app/connect")));
});

var app = builder.Build();

app.MapGet("/", () =>
{
    return "Hello World!";
});

app.Run();
