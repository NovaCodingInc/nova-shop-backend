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
            Items = order.OrderDetails.Select(o => new BasketItemDTO
            {
                CatalogItemId = o.CatalogItemId,
                CatalogItemName = o.CatalogItem.Name,
                Count = o.Count,
                PictureUri = o.CatalogItem.UpdatePictureUri(_catalogSettings.CatalogPictureBaseUri),
                Price = o.CatalogItem.Price,
                TotalPrice = (o.CatalogItem.Price * o.Count)
            }).ToList()
        };
        return Ok(basket);
    }

    #endregion

    #region add to basket

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

    #region update basket

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [SwaggerOperation(
        Summary = "Update basket (Set item count)",
        Description = "Update basket",
        OperationId = "Basket.UpdateBasket",
        Tags = new[] { "Basket" })
    ]
    public async Task<IActionResult> UpdateBasket([FromBody] UpdateBasketDTO basket)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var userId = User.GetUserId();
        var updateCommand = new UpdateOrderDetailCommand(userId, basket.CatalogItemId, basket.Count);
        var result = await _mediator.Send(updateCommand);
        if (result)
            return Ok();

        return Forbid();
    }

    #endregion

    #region delete basket item

    [HttpDelete("{catalogItemId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerOperation(
        Summary = "Delete basket item",
        Description = "Delete basket item",
        OperationId = "Basket.DeleteBasketItem",
        Tags = new[] { "Basket" })
    ]
    public async Task<IActionResult> DeleteBasketItem(int catalogItemId)
    {
        var userId = User.GetUserId();
        var deleteCommand = new DeleteOrderDetailCommand(userId, catalogItemId);
        await _mediator.Send(deleteCommand);
        return Ok();
    }

    #endregion
}