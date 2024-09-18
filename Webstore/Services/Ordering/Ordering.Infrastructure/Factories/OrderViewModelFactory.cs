using Ordering.Application.Contracts.Factories;
using Ordering.Application.Features.Orders.Queries.ViewModels;
using Ordering.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Factories
{
    public class OrderViewModelFactory : IOrderViewModelFactory
    {
        public OrderViewModel CreateViewModel(Order order)
        {
            var orderVM = new OrderViewModel();
            orderVM.Id = order.Id;
            orderVM.BuyerId = order.BuyerId;
            orderVM.BuyerUsername = order.BuyerUsername;
            orderVM.Street = order.Address.Street;
            orderVM.City = order.Address.City;
            orderVM.State = order.Address.State;
            orderVM.Country = order.Address.Country;
            orderVM.EmailAdress = order.Address.EmailAddress;
            orderVM.ZipCode = order.Address.ZipCode;

            var orderItems = new List<OrderItemViewModel>();
            foreach (var item in order.OrderItems)
            {
                var orderItem = new OrderItemViewModel();
                orderItem.Id = item.Id;
                orderItem.ProductName = item.ProductName;
                orderItem.ProductId = item.ProductId;
                orderItem.PictureUrl = item.PictureUrl;
                orderItem.Price = item.Price;
                orderItem.Units = item.Units;

                orderItems.Add(orderItem);
            }

            orderVM.OrderItems = orderItems;

            return orderVM;
        }
    }
}
// ViewModel su klase podaci, u njih samo prebacis podatke iz Order klase i to je to