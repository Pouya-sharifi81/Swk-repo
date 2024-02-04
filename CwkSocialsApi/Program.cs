
using CwkSocialsApi.Extention;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterServices(typeof(Program));
var app = builder.Build();

app.RegisterPipeLineComponent(typeof(Program));

app.Run();
