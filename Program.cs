var builder = WebApplication.CreateBuilder(args);
builder.RegisterAppServices();
var app = builder.Build();
app.RegisterEndPoints();
app.MapGet("/", () => "Hello World!");

app.Run();
