using Microsoft.AspNetCore.Connections;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
       .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));



builder.WebHost.UseTunnelTransport(o =>
{
    o.MaxConnectionCount = 1;
    o.Transport = TransportType.HTTP2;
});

// This is the HTTP/2 endpoint to register this app as part of the cluster endpoint
var url = builder.Configuration["Tunnel:Url"]!;

builder.WebHost.ConfigureKestrel(o =>
{
    // WebSockets
    // o.Listen(new UriEndPoint(new("https://localhost:7244/connect-ws")));

    // H2
    o.Listen(new UriEndPoint(new(url)));
// o.Listen(new UriEndPoint(new(url)));
});

var app = builder.Build();

app.MapReverseProxy();

app.Run();
