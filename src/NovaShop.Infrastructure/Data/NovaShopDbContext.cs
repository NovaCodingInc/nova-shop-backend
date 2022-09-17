namespace NovaShop.Infrastructure.Data;

// Add-Migration AddCustomer -Context NovaShopDbContext -o "Migrations"
// Update-Database -Context NovaShopDbContext
public class NovaShopDbContext : DbContext
{
    #region constrcutor

    private readonly IDomainEventDispatcher? _dispatcher;

    public NovaShopDbContext(DbContextOptions<NovaShopDbContext> options,
        IDomainEventDispatcher? dispatcher) : base(options)
    {
        _dispatcher = dispatcher;
    }

    #endregion

    #region catalog

    public DbSet<CatalogItem> CatalogItems => Set<CatalogItem>();
    public DbSet<CatalogBrand> CatalogBrands => Set<CatalogBrand>();
    public DbSet<CatalogCategory> CatalogCategories => Set<CatalogCategory>();
    public DbSet<CatalogGallery> CatalogGalleries => Set<CatalogGallery>();

    #endregion

    #region customer

    public DbSet<Customer> Customers => Set<Customer>();

    #endregion

    #region order

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();

    #endregion

    #region on model creating

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    #endregion

    #region save changes

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // ignore events if no dispatcher provided
        if (_dispatcher == null) return result;

        // dispatch events only if save was successful
        var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToArray();

        await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

        return result;
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }

    #endregion
}