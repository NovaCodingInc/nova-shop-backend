namespace NovaShop.Infrastructure.Data;

public class EFRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EFRepository(NovaShopDbContext dbContext) : base(dbContext) { }
}