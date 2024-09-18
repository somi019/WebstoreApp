using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Aggregates;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistance.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistance
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions options) : base(options) 
        {
        }
        public DbSet<Order> Orders { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>()) 
            {
                // za sve koji nasledjuju EntityBase(entiteti i agregati)
                switch (entry.State) 
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "rs2";
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = "rs2";
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            } 
            return base.SaveChangesAsync(cancellationToken);
            //treba da se ostavi uvek base poziv
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
// svaka baza treba da ima 1 kontekst
