using Hangfire;
using invest.Model;
using Microsoft.EntityFrameworkCore;

namespace invest.Jobs
{
    public class HangfireJobs : IHostedService
    {
        private DatabaseContext context;
        public HangfireJobs(IServiceProvider serviceProvider)
        {
            this.context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<DatabaseContext>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                context.Items.ForEachAsync(i =>
                {
                    RecurringJob.AddOrUpdate(() => PriceHistoryJob.Run(i), Cron.Daily);
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
