var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/transform", () => "Hello World!");

app.Run();
