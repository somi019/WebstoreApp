using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistance.EntityConfigurations
{
    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            // napravi tabelu OrderItems

            builder.HasKey(o => o.Id);
            // polje koje se zove Id je primarni kljuc ove tabele

            builder.Property(o => o.Id).UseHiLo("orderitemseq");
            // koristi HiLo algoritam za davanje Id-a

            builder.Property<string>("ProductId")
                .HasColumnType("VARCHAR(24)")
                .HasColumnName("ProductId")
                .IsRequired();
            // dodajemo kolonu, koja ima tip VARCHAR(24)
            // ima ime "ProductId"(mogli smo bilo koje drugo)
            // i required je, obavezna

            // uradi do kraja za sve Property-e
           


        }
    }
}
