using invest.Data.Response;
using invest.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace invest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        [HttpGet]
        public DataResponse<string[]> Currency()
        {
            return new DataResponse<string[]>().Processed(Enum.GetNames(typeof(Currency)));
        }
    }
}
