using Microsoft.AspNetCore.Mvc;
using Raspberry.App.Model.Database;
using Raspberry.App.Services.Interfaces;

namespace Raspberry.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IDatabaseService<Item> service;

        public ItemController(IDatabaseService<Item> service)
        {
            this.service = service;
        }

        [HttpGet]
        public IList<Item> GetAll()
        {
            return service.GetAll();
        }
    }
}
