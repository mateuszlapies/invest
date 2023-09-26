using Microsoft.AspNetCore.Mvc;
using Raspberry.App.Integrations.Bluetooth.Interfaces;

namespace Raspberry.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetupController : ControllerBase
    {
        private readonly IBluetoothIntegration integration;

        public SetupController(IBluetoothIntegration integration)
        {
            this.integration = integration;
        }

        [HttpGet]
        public async Task<string> GetAddress()
        {
            integration.InitializeAdapter();
            return await integration.GetAdapterAddress();
        }
    }
}
