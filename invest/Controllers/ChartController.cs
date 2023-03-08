using invest.Data.Response;
using invest.Data.Types;
using invest.Model.Steam;
using invest.Services;
using Microsoft.AspNetCore.Mvc;

namespace invest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly ChartService service;

        public ChartController(ChartService service)
        {
            this.service = service;
        }

        [HttpGet]
        public DataResponse<UserItem> GetChart(Guid id, ChartType type)
        {

            return new DataResponse<UserItem>().Processed(service.Get(id, type));
        }
    }
}
