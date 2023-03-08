using invest.Model;
using invest.Model.NBP;

namespace invest.Jobs
{
    public class ExchangeRateJob
    {
        private readonly ILogger<ExchangeRateJob> logger;
        private readonly DatabaseContext context;

        public ExchangeRateJob(ILogger<ExchangeRateJob> logger, DatabaseContext context) {
            this.logger = logger;
            this.context = context;
        }

        public void Run() {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    Exchange exchange = client.GetFromJsonAsync<Exchange>("https://api.nbp.pl/api/exchangerates/tables/A").GetAwaiter().GetResult();
                    context.Exchanges.Add(exchange);
                    context.SaveChanges();
                    logger.LogInformation("Update of exchange rates has been completed successfully: ExchangeId: {exchangeId}", exchange.ExchangeId);
                } catch (Exception ex)
                {
                    logger.LogError("Update of exchange rates has failed with error: {error}", ex);
                }
            }
        }
    }
}
