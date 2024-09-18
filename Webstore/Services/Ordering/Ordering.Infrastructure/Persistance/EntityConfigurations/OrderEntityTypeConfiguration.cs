using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistance.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).UseHiLo("orderseq");
            // OrderItem se sastoji samo iz obicnih polja (stringovi,intovi,decimal,itd.)
            // Ali Order u sebi ima vrednosni objekat Address pored toga
            // treba reci : Order je vlasnik tog vrednosnog objekta
            builder.OwnsOne(o => o.Address, a =>
            {
                // adresa bi trebalo da ima neki strani kljuc da bi se znalo na koju se porudzbinu odnosi
                a.Property<int>("OrderId").UseHiLo("orderseq");
                // istu sekvencu "orderseq" koristim za generisanje privatnih kljuceva za order
                a.WithOwner();
            });

            var navigation = builder.Metadata.FindNavigation(nameof(Order.OrderItems));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
            //nadji public OrderItems i postavi da se pristupa polju _orderItems

        }
    }
}
