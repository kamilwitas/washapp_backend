using System.Reflection;
using System.Security.Claims;
using washapp.services.identity.application;
using washapp.services.identity.infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using washapp.services.identity.infrastructure.Auth.Settings;
using MediatR;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Spectre.Console;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("JWT").Bind(authenticationSettings);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5010, ListenOptions => ListenOptions.Protocols = HttpProtocols.Http1);
});

builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));

AnsiConsole.Write(
    new FigletText("WashApp v1")
        .LeftAligned()
        .Color(Color.Aqua));
AnsiConsole.Write(
    new FigletText("Identity")
        .LeftAligned()
        .Color(Color.Aqua));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt => {
        opt.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration,authenticationSettings);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer \"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddCors(o=> 
{
    o.AddPolicy("WashAppFrontEndApp", policy =>
    policy
    .AllowAnyHeader()
    .AllowAnyOrigin()
    .AllowAnyMethod()
    );
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Employee", builder =>
    {
        builder.RequireClaim(ClaimTypes.Role.ToString(), "User");
    });
    options.AddPolicy("Administrator", builder =>
    {
        builder.RequireClaim(ClaimTypes.Role.ToString(), "Admin");
    });
});

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();


if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName.Equals("Development") || app.Environment.EnvironmentName.Equals("Testing"))
{
    app.UseSwagger(options =>
    {
        options.PreSerializeFilters.Add((swagger, httpReq) =>
        {
            if (httpReq.Headers.ContainsKey("X-Forwarded-Host"))
            {
                var basePath = "identity";
                var serverUrl = $"{httpReq.Scheme}://{httpReq.Headers["X-Forwarded-Host"]}/{basePath}";
                swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = serverUrl } };
            }
        });
    })
.UseSwaggerUI();
}
app.UseInfrastructure();
app.UseCors("WashAppFrontEndApp");
app.UseApplication();
app.PrepDatabase();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
