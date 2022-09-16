using NovaShop.ApplicationCore.OrderAggregate.Queries.GetCustomerBasket;

namespace NovaShop.Web.Controllers;

[Authorize]
public class BasketController : ApiBaseController
{
    #region constrcutor

    private readonly IMediator _mediator;
    private readonly CatalogSettings _catalogSettings;

    public BasketController(IMediator mediator, IOptions<CatalogSettings> catalogOptions)
    {
        _mediator = mediator;
        _catalogSettings = catalogOptions.Value;
    }

    #endregion

    #region get customer basket

    [HttpGet]
    [ProducesResponseType(typeof(GetCustomerBasketDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerOperation(
        Summary = "Get customer basket",
        Description = "Get customer basket",
        OperationId = "Basket.GetCustomerBasket",
        Tags = new[] { "Basket" })
    ]
    public async Task<ActionResult<GetCustomerBasketDTO>> GetCustomerBasket()
    {
        var userId = User.GetUserId();
        var query = new GetCustomerBasketQuery(userId);
        var order = await _mediator.Send(query);
        var basket = new GetCustomerBasketDTO
        {
            IsPaid = false,
            CustomerId = order.CustomerId,
            Items = order.OrderDetails.Select(o => new BasketItemDTO
            {
                CatalogItemId = o.CatalogItemId,
                CatalogItemName = o.CatalogItem.Name,
                Count = o.Count,
                PictureUri = o.CatalogItem.UpdatePictureUri(_catalogSettings.CatalogPictureBaseUri),
                Price = o.CatalogItem.Price
            }).ToList()
        };
        return Ok(basket);
    }

    #endregion

    #region add to basket

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
        Summary = "Add to basket",
        Description = "Add item to basket",
        OperationId = "Basket.AddToBasket",
        Tags = new[] { "Basket" })
    ]
    public async Task<IActionResult> AddToBasket([FromBody] AddToOrderDTO addToOrder)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var userId = User.GetUserId();
        var command = new AddToOrderCommand(userId, addToOrder.CatalogItemId, addToOrder.Count);
        await _mediator.Send(command);
        return Ok();
    }

    #endregion

    #region delete basket item



    #endregion
}