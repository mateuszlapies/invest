using Microsoft.AspNetCore.Mvc;
using Raspberry.App.Database.Model;
using Raspberry.App.Services.Interfaces;

namespace Raspberry.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceController : ControllerBase
    {
        private readonly IQueryByItem<Price> service;

        public PriceController(IQueryByItem<Price> service)
        {
            this.service = service;
        }

        [HttpGet]
        public IList<Price> Get(long id)
        {
            return service.GetAllByItem(id);
        }
    }
}
