using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
const string corsPolicy = "NovaShopCorsPolicy";

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.UseSerilog((_, config) => 
    config.ReadFrom.Configuration(builder.Configuration));

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

string connectionString = builder.Configuration.GetConnectionString("CatalogConnectionString");

builder.Services.AddDatabase(connectionString);

builder.Services.AddAutoMapperProfile();

builder.Services.AddIdentity(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = builder.Environment.IsDevelopment());;
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nova Shop Api", Version = "v1" });
    c.EnableAnnotations();
});

builder.Services.Configure<CatalogSettings>(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy,
        policy => policy
            .WithOrigins(builder.Configuration.GetValue<string>("CorsPolicyOrigin"))
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
builder.Services.Configure<ServiceConfig>(config =>
{
    config.Services = new List<ServiceDescriptor>(builder.Services);
    config.Path = "/novashipservicelist";
});

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new DefaultCoreModule());
    containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.IsDevelopment()));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseShowAllServicesMiddleware();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nova Shop Api"));
}

app.UseHttpsRedirection();
app.UseCookiePolicy();

app.UseAuthorization();

app.UseCors(corsPolicy);

app.MapControllers();

// Seed Database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<NovaShopDbContext>();
        context.Database.EnsureCreated();
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
    }
}

app.Run();