var builder = WebApplication.CreateBuilder(args);
const string CORS_POLICY = "NovaShopCorsPolicy";

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

string connectionString = builder.Configuration.GetConnectionString("CatalogConnectionString");
builder.Services.AddDatabase(connectionString);

builder.Services.AddAutoMapperProfile();
builder.Services.AddIdentity(builder.Configuration);
builder.Services.AddControllers(options =>
    {
        options.Filters.Add(typeof(HttpGlobalExceptionFilter));
        options.Filters.Add(typeof(ValidateModelStateFilter));
    })
    .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = builder.Environment.IsDevelopment());
builder.Services.AddSwagger();
builder.Services.Configure<CatalogSettings>(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy(CORS_POLICY,
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
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
}

app.UseHttpsRedirection();

app.UseCors(CORS_POLICY);

app.UseAuthorization();
app.UseAuthentication();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nova Shop Api"));

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