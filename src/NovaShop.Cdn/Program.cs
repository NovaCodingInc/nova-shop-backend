var builder = WebApplication.CreateBuilder(args);

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

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();