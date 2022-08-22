var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    Args = args,
    WebRootPath = "pictures",
    ContentRootPath = Directory.GetCurrentDirectory()
});

// Create pictures directory
if (!Directory.Exists(".\\pictures"))
    Directory.CreateDirectory(".\\pictures");

if (!Directory.Exists(".\\pictures\\gallery"))
    Directory.CreateDirectory(".\\pictures\\gallery");

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(cors => cors.AddPolicy("blazor-upload-picture-cors", policy =>
{
    policy.WithOrigins(builder.Environment.IsDevelopment() ? "localhost:5001" : "nova-shop.admin.mehdimst.com")
        .WithMethods("POST")
        .AllowAnyHeader()
        .AllowCredentials();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();