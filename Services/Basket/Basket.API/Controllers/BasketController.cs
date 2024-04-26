using System.Net;
using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Common.Logging.Correlation;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    public class BasketController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<BasketController> _logger;
        private readonly ICorrelationIdGenerator _correlationIdGenerator;

        public BasketController(IMediator mediator, IPublishEndpoint publishEndpoint,ILogger<BasketController> logger,ICorrelationIdGenerator correlationIdGenerator)
        {
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
            _correlationIdGenerator = correlationIdGenerator;
            _logger.LogInformation("CorrelationId {correlationId} :",correlationIdGenerator.Get());
        }

        [HttpGet]
        [Route("[action]/{userName}", Name = "GetBasketByUserName")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<ShoppingCartResponse>> GetBasket(string userName)
        {
            var query = new GetBasketByUserNameQuery(userName);

            var basket = await _mediator.Send(query);

            return Ok(basket);
        }

        [HttpDelete]
        [Route("[action]/{userName}", Name = "DeleteBasketByUserName")]
        [ProducesResponseType((int)HttpStatusCode.OK)]

        public async Task<ActionResult<ShoppingCartResponse>> DeleteBasketByUserName(string userName)
        {
            var query = new DeleteBasketByUserNameCommand(userName);
            return Ok(await _mediator.Send(query));
        }

        [HttpPost("CreateBasket")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> UpdateBasket(
            [FromBody] CreateShoppingCartCommand createShoppingCartCommand)
        {

            var basket = await _mediator.Send(createShoppingCartCommand);
            return Ok(basket);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public async Task<ActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            //Get existing basket with username
            var query = new GetBasketByUserNameQuery(basketCheckout.UserName);

            var basket = await _mediator.Send(query);

            if (basket == null)
            {
                return BadRequest();
            }

            var eventMsg = BasketMapper.Mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMsg.TotalPrice = basketCheckout.TotalPrice;


            await _publishEndpoint.Publish(eventMsg);

            // remove the basket

            var deleteQuery = new DeleteBasketByUserNameCommand(basketCheckout.UserName);
            await _mediator.Send(deleteQuery);

            return Accepted();


        }


    }
}
