using Hangfire;
using Hangfire.PostgreSql;

namespace Server.App.Integrations.Hangfire
{
    public class Hangfire
    {
        public Hangfire()
        {
            GlobalConfiguration.Configuration.UsePostgreSqlStorage("server=postgres;database=hangfire;user id=postgres;password=postgres");


        }
    }
}
