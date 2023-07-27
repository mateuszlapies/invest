using Microsoft.EntityFrameworkCore;
using Raspberry.App.Database;
using Raspberry.App.Database.Model;
using Raspberry.App.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry.App.Services.Database
{
    public class PriceService : IDatabaseService<Price>, IQueryByItem<Price>
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

        public IList<Price> GetAllByItem(long id)
        {
            return context.Prices
                .Include(p => p.Item)
                .Where(p => p.Item.Id == id)
                .ToList();
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
