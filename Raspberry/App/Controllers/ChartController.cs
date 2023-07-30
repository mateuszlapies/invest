using Microsoft.AspNetCore.Mvc;
using Raspberry.App.Model.Services.Chart;
using Raspberry.App.Services.Interfaces;

namespace Raspberry.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChartController : ControllerBase
    {
        private readonly IChartService chartService;

        public ChartController(IChartService chartService)
        {
            this.chartService = chartService;
        }

        [HttpGet]
        public ChartOptions Get(ChartType chartType, long itemId)
        {
            switch (chartType)
            {
                case ChartType.MAX:
                    return chartService.GetMax(itemId);

                case ChartType.YEAR:
                    return chartService.GetMax(itemId);

                case ChartType.MONTH:
                    return chartService.GetMax(itemId);

                default:
                case ChartType.DAY:
                    return chartService.GetMax(itemId);
            }
        }
    }
}
