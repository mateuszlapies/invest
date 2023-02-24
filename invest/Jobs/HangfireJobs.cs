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
            new SteamService(serviceProvider);
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
