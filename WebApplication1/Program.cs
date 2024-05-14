var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", (httpContext) =>
//{
//    var ctx = httpContext.Request.HttpContext;

//    return "Hello world";
//});

app.Map("/", async ( HttpContext context) =>
{
    // This is hardcoded to a single backend, but that's just for the demo


    return "Hello World";
});

app.Run();
