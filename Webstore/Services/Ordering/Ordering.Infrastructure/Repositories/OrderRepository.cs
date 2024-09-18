using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Aggregates;
using Ordering.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUsername(string username)
        {
            return await _dbContext.Orders
                .Where(o => o.BuyerUsername == username)
                .Include(o => o.OrderItems)
                .ToListAsync();
            // bez Include ne bi radilo, tu ukljucujemo i OrderItems sa left join
        }
    }
}
// Ovo je repozitorijum za Order, samo smo mu dodali jos jednu metodu pored RepositoryBase
// koja je za uzimanje Ordera za jedan Username
// fora u DDD je da imamo sto vise baznih stvari koje koristimo svuda da ne bi ponavljali kod
// zato kad dodajes nesto novo i implementiras neki feature vrlo se lagano doda