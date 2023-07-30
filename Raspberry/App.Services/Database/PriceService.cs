using Microsoft.EntityFrameworkCore;
using Raspberry.App.Database;
using Raspberry.App.Model.Database;
using Raspberry.App.Services.Interfaces;

namespace Raspberry.App.Services.Database
{
    public class PriceService : IDatabaseService<Price>, IQueryAllByItem<Price>
    {
        private readonly DatabaseContext context;

        public PriceService(DatabaseContext context)
        {
            this.context = context;
        }

        public long Add(Price entity)
        {
            context.Prices.Add(entity);
            context.SaveChanges();
            return entity.Id;
        }

        public Price? Get(long id)
        {
            return context.Prices.SingleOrDefault(p => p.Id == id);
        }

        public IQueryable<Price> GetAllByItem(long id)
        {
            return context.Prices
                .Include(p => p.Item)
                .Where(p => p.Item.Id == id)
                .OrderByDescending(p => p.Timestamp);
        }

        public IList<Price> GetAll()
        {
            return context.Prices.ToList();
        }

        public long Update(Price entity)
        {
            context.Prices.Update(entity);
            context.SaveChanges();
            return entity.Id;
        }
    }
}
