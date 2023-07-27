using Microsoft.EntityFrameworkCore;
using Raspberry.App.Database;
using Raspberry.App.Database.Model;
using Raspberry.App.Services.Interfaces;

namespace Raspberry.App.Services.Database
{
    public class OrderService : IDatabaseService<Order>, IQueryByItem<Order>
    {
        private readonly DatabaseContext context;

        public OrderService(DatabaseContext context)
        {
            this.context = context;
        }

        public long Add(Order entity)
        {
            context.Orders.Add(entity);
            context.SaveChanges();
            return entity.Id;
        }

        public Order? Get(long id)
        {
            return context.Orders.SingleOrDefault(o => o.Id == id);
        }

        public IList<Order> GetAllByItem(long id)
        {
            return context.Orders
                .Include(p => p.Item)
                .Where(p => p.Item.Id == id)
                .ToList();
        }

        public IList<Order> GetAll()
        {
            return context.Orders.ToList();
        }

        public long Update(Order entity)
        {
            context.Orders.Update(entity);
            context.SaveChanges();
            return entity.Id;
        }
    }
}
