using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Factories;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderFactory _factory;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(IOrderRepository repository, IOrderFactory factory, IEmailService emailService, ILogger<CreateOrderCommandHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _factory.Create(request);
            var newOrder = await _repository.AddAsync(orderEntity);
            // novi order je sacuvan da bi mogli da posaljemo mejl sa tim informacijama
            // (napravis sablon mejla sa informacijama o samoj porudzbini i izlistas proizvode)

            _logger.LogInformation($"Order {newOrder.Id} is successfully created.");

            await SendMail(newOrder);

            return newOrder.Id;


            // po pravilu bi trebalo da imam po jednu fabriku za svaki argument
            // koja vraca podatke istog tipa (vracaju isto sve funkcije)
            // FABRIKA FABRIKUJE SVE U ORDER

            // svaki agregat bi trebalo da ima svoju fabriku
            // tako i svaka ostala klasa koju koristim u kodu
            
            // ali pitanje je kako fabrika poziva metodu Create koja nigde u kodu nije
            // definisana

        }

        private async Task SendMail(Order newOrder)
        {
            var email = new Email
            {
                To = newOrder.Address.EmailAddress,
                Subject = $"Order {newOrder.Id} is successfully created",
                Body = "You have placed a new order on Webstore."
            };

            try
            {
                await _emailService.SendEmail(email);
                _logger.LogInformation($"Sending email for order {newOrder.Id} was successful");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Sending email for order {newOrder.Id} failed due to error : {ex.Message}");

            }

        }
    }
}
// sta sve zelimo da se desi kada korisnik kreira novu porudzbinu?
// 1. zelimo da se sacuva u bazi => treba nam repozitorijum

