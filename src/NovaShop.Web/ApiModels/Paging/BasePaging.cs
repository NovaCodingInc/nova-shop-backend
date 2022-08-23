namespace NovaShop.Web.ApiModels.Paging;

public class BasePaging
{
    public BasePaging()
    {
        PageId = 1;
        HowManyShowPageAfterAndBefore = 3;
    }

    public int PageId { get; set; }

    public int PageCount { get; set; }

    public int AllEntitiesCount { get; set; }

    public int StartPage { get; set; }

    public int EndPage { get; set; }

    public int Take { get; set; }

    public int Skip { get; set; }

    public int HowManyShowPageAfterAndBefore { get; set; }

    public int GetLastPage()
    {
        return (int)Math.Ceiling(AllEntitiesCount / (double)Take);
    }

    public BasePaging GetCurrentPaging()
    {
        return this;
    }
}