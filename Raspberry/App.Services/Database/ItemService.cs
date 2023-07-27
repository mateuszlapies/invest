using Raspberry.App.Database;
using Raspberry.App.Database.Model;
using Raspberry.App.Services.Interfaces;

namespace Raspberry.App.Services.Database
{
    public class ItemService : IDatabaseService<Item>
    {
        private readonly DatabaseContext context;

        public ItemService(DatabaseContext context)
        {
            this.context = context;
        }

        public long Add(Item entity)
        {
            context.Items.Add(entity);
            context.SaveChanges();
            return entity.Id;
        }

        public Item? Get(long id)
        {
            return context.Items.SingleOrDefault(item => item.Id == id);
        }

        public IList<Item> GetAll()
        {
            return context.Items.ToList();
        }

        public long Update(Item entity)
        {
            context.Items.Update(entity);
            context.SaveChanges();
            return entity.Id;
        }
    }
}
