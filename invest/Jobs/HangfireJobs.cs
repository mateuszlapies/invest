using Hangfire;
using invest.Model;
using invest.Steam;
using Microsoft.EntityFrameworkCore;

namespace invest.Jobs
{
    public class HangfireJobs : IHostedService
    {
        private IServiceProvider provider;
        private DatabaseContext context;
        private ILogger<HangfireJobs> logger;
        private List<string> jobIds = new List<string>();

        public HangfireJobs(IServiceProvider serviceProvider)
        {
            provider = serviceProvider.CreateScope().ServiceProvider;
            context = provider.GetRequiredService<DatabaseContext>();
            logger = provider.GetRequiredService<ILogger<HangfireJobs>>();
            var steamAuthProvider = provider.CreateScope().ServiceProvider;
            var steamAuthLogger = steamAuthProvider.GetRequiredService<ILogger<SteamAuth>>();
            var steamAuthConfiguration = steamAuthProvider.GetRequiredService<IConfiguration>();
            var steamAuthContext = steamAuthProvider.GetRequiredService<DatabaseContext>();
            var steamAuth = new SteamAuth(steamAuthLogger, steamAuthConfiguration, steamAuthContext);
            steamAuth.Login();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                string erId = "Exchange Rates Update";
                RecurringJob.AddOrUpdate<ExchangeRateJob>(erId, x => x.Run(), Cron.Daily);
                jobIds.Add(erId);
                logger.LogInformation("Scheduled a ExchangeRateJob");

                context.Items.ForEachAsync(i =>
                {
                    if (i.Url == null)
                    {
                        BackgroundJob.Enqueue<ItemDetailsJob>(x => x.Run(i));
                        logger.LogInformation("Scheduled a ItemDetailsJob for item {item}", i.Name);
                    }

                    string mbmId = string.Format("Minute by minute price for {0}", i.Name);
                    RecurringJob.AddOrUpdate<PriceDailyJob>(mbmId, x => x.Run(i), Cron.Minutely);
                    jobIds.Add(mbmId);
                    logger.LogInformation("Scheduled a recurring daily PriceHistoryJob for item {item}", i.Name);

                    string phId = string.Format("Price history for {0}", i.Name);
                    RecurringJob.AddOrUpdate<PriceHistoryJob>(phId, x => x.Run(i), Cron.Daily);
                    jobIds.Add(phId);
                    logger.LogInformation("Scheduled a recurring daily PriceHistoryJob for item {item}", i.Name);
                });
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                jobIds.ForEach(e => RecurringJob.RemoveIfExists(e));
            });
        }
    }
}
