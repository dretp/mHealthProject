using mHealthProject.Middleware;

var SetAllowSpecificOrigins = "_setAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// configure cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: SetAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5280",
                "http://localhost:8080",
                "http://*:1025");
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.SetIsOriginAllowed((host) => true);
        });
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();
//add registry service for app settings and services
builder.Services.MPathStartupServices();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(SetAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseMHealthMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
