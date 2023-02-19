using invest.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace invest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private DatabaseContext dbContext;

        public ItemController(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public Item? GetItem(int id)
        {
            return dbContext.Items.FirstOrDefault(q => q.Id == id);
        }

        [HttpPost]
        public Item? UpdateItem(Item item)
        {
            dbContext.Items.Add(item);
            return item;
        }

        [HttpDelete]
        public Item? DeleteItem(int id)
        {
            Item item = dbContext.Items.FirstOrDefault(q => q.Id == id);
            dbContext.Items.Remove(item);
            return item;
        }
    }
}
