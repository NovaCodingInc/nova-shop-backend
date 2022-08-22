using Microsoft.EntityFrameworkCore;
using NovaShop.ApplicationCore.CatalogAggregate;

namespace NovaShop.Web;

public class SeedData
{
    public static readonly CatalogBrand TestCatalogBrand = new ("Nova Coding Brand");

    public static CatalogItem CatalogItem1 = new(TestCatalogBrand.Id, "IPhone X", "Mobile",
        "Apple IPhone X is a expensive phone in world", 
        "Apple IPhone X is a expensive phone in world, Apple IPhone X is a expensive phone in world, Apple IPhone X is a expensive phone in world", 
        "1.png", 3, 31000);

    public static readonly CatalogItem CatalogItem2 = new(TestCatalogBrand.Id, "Samsung S20 Plus", "Mobile",
        "Samsung S20 Plus is a expensive phone in world",
        "Samsung S20 Plus is a expensive phone in world, Samsung S20 Plus is a expensive phone in world, Samsung S20 Plus is a expensive phone in world",
        "2.png", 10, 26500);

    public static readonly CatalogItem CatalogItem3 = new(TestCatalogBrand.Id, "Laptop FX510 ASUS", "Laptop",
        "Laptop FX510 ASUS is a expensive laptop in world",
        "Laptop FX510 ASUS is a expensive laptop in world, Laptop FX510 ASUS is a expensive laptop in world, Laptop FX510 ASUS is a expensive laptop in world",
        "3.png", 8, 43200);

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var dbContext = new NovaShopDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<NovaShopDbContext>>(), null);
        if (dbContext.CatalogItems.Any())
        {
            return; // DB has been seeded
        }

        PopulateTestData(dbContext);
    }

    public static void PopulateTestData(NovaShopDbContext dbContext)
    {
        foreach (var item in dbContext.CatalogBrands)
        {
            dbContext.Remove(item);
        }
        foreach (var item in dbContext.CatalogGalleries)
        {
            dbContext.Remove(item);
        }
        foreach (var item in dbContext.CatalogItems)
        {
            dbContext.Remove(item);
        }
        dbContext.SaveChanges();

        var brandId = dbContext.CatalogBrands.Add(TestCatalogBrand).Entity.Id;
        dbContext.SaveChanges();

        // Set brand id
        CatalogItem1.UpdateBrand(brandId);
        CatalogItem2.UpdateBrand(brandId);
        CatalogItem3.UpdateBrand(brandId);

        dbContext.CatalogItems.Add(CatalogItem1);
        dbContext.CatalogItems.Add(CatalogItem2);
        dbContext.CatalogItems.Add(CatalogItem3);

        dbContext.SaveChanges();
    }
}