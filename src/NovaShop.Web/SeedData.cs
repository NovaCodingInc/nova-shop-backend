using Microsoft.EntityFrameworkCore;

namespace NovaShop.Web;

public class SeedData
{
    public static readonly CatalogBrand TestCatalogBrand = new ("Nova Coding Brand");
    public static readonly CatalogCategory TestMobileCategory = new ("Mobile");
    public static readonly CatalogCategory TestLaptopCategory = new ("Laptop");

    public static CatalogItem CatalogItem1 = new(TestCatalogBrand.Id, TestMobileCategory.Id, "IPhone X",
        "Apple IPhone X is a expensive phone in world", 
        "Apple IPhone X is a expensive phone in world, Apple IPhone X is a expensive phone in world, Apple IPhone X is a expensive phone in world",
        "1.jpg", 3, 31000);

    public static readonly CatalogItem CatalogItem2 = new(TestCatalogBrand.Id, TestMobileCategory.Id, "Samsung S20 Plus", 
        "Samsung S20 Plus is a expensive phone in world",
        "Samsung S20 Plus is a expensive phone in world, Samsung S20 Plus is a expensive phone in world, Samsung S20 Plus is a expensive phone in world",
        "2.jpg", 10, 26500);

    public static readonly CatalogItem CatalogItem3 = new(TestCatalogBrand.Id, TestLaptopCategory.Id, "Laptop FX510 ASUS", 
        "Laptop FX510 ASUS is a expensive laptop in world",
        "Laptop FX510 ASUS is a expensive laptop in world, Laptop FX510 ASUS is a expensive laptop in world, Laptop FX510 ASUS is a expensive laptop in world",
        "3.jpg", 8, 43200);

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
        foreach (var item in dbContext.CatalogGalleries)
        {
            dbContext.Remove(item);
        }
        foreach (var item in dbContext.CatalogItems)
        {
            dbContext.Remove(item);
        }
        foreach (var item in dbContext.CatalogBrands)
        {
            dbContext.Remove(item);
        }
        foreach (var item in dbContext.CatalogCategories)
        {
            dbContext.Remove(item);
        }
        dbContext.SaveChanges();

        var brandEntity = dbContext.CatalogBrands.Add(TestCatalogBrand);
        var mobileEntity = dbContext.CatalogCategories.Add(TestMobileCategory);
        var laptopEntity = dbContext.CatalogCategories.Add(TestLaptopCategory);
        dbContext.SaveChanges();

        // Set brand id
        CatalogItem1.UpdateBrand(brandEntity.Entity.Id);
        CatalogItem2.UpdateBrand(brandEntity.Entity.Id);
        CatalogItem3.UpdateBrand(brandEntity.Entity.Id);

        // Set category id
        CatalogItem1.UpdateCategory(mobileEntity.Entity.Id);
        CatalogItem2.UpdateCategory(mobileEntity.Entity.Id);
        CatalogItem3.UpdateCategory(laptopEntity.Entity.Id);

        dbContext.CatalogItems.Add(CatalogItem1);
        dbContext.CatalogItems.Add(CatalogItem2);
        dbContext.CatalogItems.Add(CatalogItem3);

        dbContext.SaveChanges();
    }
}