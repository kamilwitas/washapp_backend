using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("yarp"));

builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));


builder.Services.AddCors(options=>{
   options.AddPolicy("WashAppFrontendApp", policy =>{
        policy.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseRouting();
app.MapGet("/", ()=>"WashApp Api Gateway");
app.UseCors("WashAppFrontendApp");
app.MapReverseProxy();


app.Run();

