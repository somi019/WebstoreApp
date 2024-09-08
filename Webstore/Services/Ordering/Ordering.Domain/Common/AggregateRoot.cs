using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Common
{
    public class AggregateRoot : EntityBase
    {

    }
}
// kad implementiramo jedan agregat on bi trebalo da ima samo jedan entitet za koren
// Kupac moze da ima vrednosne objekte Kontakt(brTel,brFaksa,eMail) i Adresu(ulica,grad,drzava)
// kad pristupamo agregatu treba da pristupamo korenu (Kupcu)
// koren agregata je ponovo entitet, zato nasledjuje EntityBase