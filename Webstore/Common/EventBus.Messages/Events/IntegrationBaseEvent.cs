using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class IntegrationBaseEvent
    {
        public Guid Id { get; private set; }
        public DateTime CreationDate { get; private set; }
        public IntegrationBaseEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }

        public IntegrationBaseEvent(Guid id, DateTime creationDate)
        {
            Id = id;
            CreationDate = creationDate;
        }
    }
}
// Sa redom poruka zelimo:
// - kada korisnik zavrsi sa kupovinom, klijent u BasketAPI-ju ce poslati http zahtev
// koji kaze zavrsio si kupovinu, odabrao si te i te proizvode, ovo je njihova cena
// i ostale stvari za order
// - prvo da proveri da li ima nesto u svojoj korpi, ako ima onda bi trebalo da posalje
// jedan asinhroni dogadjaj koji ce uhvatiti orderingAPI, i taj zahtev ce biti za
// checkoutBasket-a, poslacemo informacije o tome sta je odabrao korisnik
// ode to na red poruka
// basketAPI izbrise korpu na pocetno stanje, i salje poruku klijentu da je uradio
// sve sta treba
// i kad stigne ta poruka u orderingAPI mi cemo dodati tu porudzbinu u nasu bazu.