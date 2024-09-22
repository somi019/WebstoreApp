using AutoMapper;

namespace Basket.API.Mapper
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<Entities.BasketCheckout,EventBus.Messages.Events.BasketCheckoutEvent>().ReverseMap();
            CreateMap<Entities.BasketItem, EventBus.Messages.Events.BasketItem>().ReverseMap();
        }

    }
}
// Drugi nacin za utvrdjivanje pravila za mapiranje umesto da sve pises u Program.cs
// Samo da klasa nasledi Profile iz Automappera i u konstruktoru postavis
// sta se mapira u sta