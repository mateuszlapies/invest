using Microsoft.AspNetCore.Mvc;
using Raspberry.App.Database.Model;
using Raspberry.App.Services.Interfaces;

namespace Raspberry.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IQueryByItem<Order> service;

        public OrderController(IQueryByItem<Order> service)
        {
            this.service = service;
        }

        [HttpGet]
        public IList<Order> Get(long id)
        {
            return service.GetAllByItem(id);
        }
    }
}
