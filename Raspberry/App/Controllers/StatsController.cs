using Microsoft.AspNetCore.Mvc;
using Raspberry.App.Model.Services.Stats;
using Raspberry.App.Services.Database;

namespace Raspberry.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly StatsService service;

        public StatsController(StatsService service)
        {
            this.service = service;
        }

        [HttpGet]
        public Stats Get(long id) {
            return service.GetByItem(id);
        }
    }
}
