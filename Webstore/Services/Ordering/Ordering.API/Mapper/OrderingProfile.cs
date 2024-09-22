using AutoMapper;
using Ordering.Application.Features.Orders.Commands.CreateOrder;
using Ordering.Application.Features.Orders.Commands.DTOs;

namespace Ordering.API.Mapper
{
    public class OrderingProfile : Profile
    {
        public OrderingProfile()
        {
            CreateMap<OrderItemDTO, EventBus.Messages.Events.BasketItem>().ReverseMap();
            CreateMap<CreateOrderCommand, EventBus.Messages.Events.BasketCheckoutEvent>().ReverseMap();
            //CreateMap<Entities.BasketCheckout,EventBus.Messages.Events.BasketCheckoutEvent>().ReverseMap();
            
        }
    }
}
