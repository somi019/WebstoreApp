using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Orders.Commands.CreateOrder;

namespace Ordering.API.EventBusConsumers
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        // Iz MediatR
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<BasketCheckoutConsumer> _logger;

        public BasketCheckoutConsumer(IMediator mediator, IMapper mapper, ILogger<BasketCheckoutConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            // Pretvaramo iz BasketCheckoutEvent u CreateOrderCommand
            var command = _mapper.Map<CreateOrderCommand>(context.Message);

            // CreateOrderCommand nasledjuje IRequest<int>, zato moze da se salje
            var id = await _mediator.Send(command);

            _logger.LogInformation($" {typeof(BasketCheckoutEvent).Name} consumed successfully. Created order id : {id}");

        }
    }
}
