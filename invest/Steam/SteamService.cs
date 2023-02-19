using invest.Steam.Data;
using System.Diagnostics.Metrics;

namespace invest.Steam
{
    public class SteamService
    {
        private HttpClient client;

        public SteamService()
        {
            client = new HttpClient();
        }

        public Task<PriceHistory> GetPriceHistory(string hash, Currency currency = Currency.PLN)
        {
            string request = string.Format("https://steamcommunity.com/market/pricehistory?appid=730&country=PL&currency={0}&market_hash_name={1}", (int)currency, hash);
            return client.GetFromJsonAsync<PriceHistory>(request);
        }
    }
}
