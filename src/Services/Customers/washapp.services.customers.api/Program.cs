using System.Security.Claims;
using washapp.services.customers.application;
using washapp.services.customers.infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using washapp.services.customers.api.Grpc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Text.Json.Serialization;
using Spectre.Console;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment.EnvironmentName;
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddGrpc();
builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));


AnsiConsole.Write(
    new FigletText("WashApp v1")
        .LeftAligned()
        .Color(Color.Aqua));
AnsiConsole.Write(
    new FigletText("Customers")
        .LeftAligned()
        .Color(Color.Aqua));

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5020, ListenOptions => ListenOptions.Protocols = HttpProtocols.Http1);
    options.ListenAnyIP(5021, ListenOptions => ListenOptions.Protocols = HttpProtocols.Http2);
});

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "Customers Service",
        Description = "WashApp customers service"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(opt =>
        {
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
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("WashAppFrontendApp", policy =>
        {
            policy
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod();
        });
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

    app.AddApplicationBuilder();

    if (app.Environment.IsDevelopment() || environment == "Development" || environment == "Testing")
    {
    app.UseSwagger(options =>
    {
        options.PreSerializeFilters.Add((swagger, httpReq) =>
        {
            if (httpReq.Headers.ContainsKey("X-Forwarded-Host"))
            {
                var basePath = "customers";
                var serverUrl = $"{httpReq.Scheme}://{httpReq.Headers["X-Forwarded-Host"]}/{basePath}";
                swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = serverUrl } };
            }
        });
    });
        app.UseSwaggerUI();
    }
    app.ApplyMigrations();
    app.UseInfrastructure();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseEndpoints(enpoints =>
    {
         enpoints.MapGrpcService<CustomersGrpcController>();
    });
    app.UseCors("WashAppFrontendApp");
    app.UseHttpsRedirection(); 
    app.MapControllers();
    app.Run();

