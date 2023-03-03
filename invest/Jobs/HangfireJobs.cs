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
                context.Items.ForEachAsync(i =>
                {
                    if (i.Url == null)
                    {
                        BackgroundJob.Enqueue<ItemDetailsJob>(x => x.Run(i));
                        logger.LogInformation("Scheduled a ItemDetailsJob for item {item}", i.Name);
                    }

                    RecurringJob.AddOrUpdate<PriceDailyJob>(x => x.Run(i), Cron.Minutely);
                    logger.LogInformation("Scheduled a recurring daily PriceHistoryJob for item {item}", i.Name);

                    RecurringJob.AddOrUpdate<PriceHistoryJob>(x => x.Run(i), Cron.Daily);
                    logger.LogInformation("Scheduled a recurring daily PriceHistoryJob for item {item}", i.Name);
                });
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {

            });
        }
    }
}
