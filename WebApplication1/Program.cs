var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.UseTunnelTransport("http://localhost:5244/connect-h2?host=backend1.app", options =>
//{
//    options.Transport = TransportType.HTTP2;
//});
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
