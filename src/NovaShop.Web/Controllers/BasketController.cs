namespace NovaShop.Web.Controllers;

[Authorize]
public class BasketController : ApiBaseController
{
    #region constrcutor

    private readonly IMediator _mediator;

    public BasketController(IMediator mediator)
    {
        _mediator = mediator;
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
}