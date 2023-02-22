using invest.Model;
using invest.Steam.Data;
using System.Net;
using Cookie = System.Net.Cookie;

namespace invest.Steam
{
    public class SteamService
    {
        private readonly ILogger<SteamService> logger;
        private readonly SteamAuthService steamAuth;
        private readonly DatabaseContext context;
        private readonly HttpClient client;

        public SteamService(IServiceProvider serviceProvider)
        {
            IServiceProvider provider = serviceProvider.CreateScope().ServiceProvider;
            logger = provider.GetRequiredService<ILogger<SteamService>>();
            steamAuth = provider.GetRequiredService<SteamAuthService>();
            context = provider.GetRequiredService<DatabaseContext>();

            Uri uri = new("https://steamcommunity.com");
            CookieContainer cookies = new();
            foreach(Model.Cookie cookie in context.Cookies.Where(q => q.Expires > DateTime.UtcNow))
            {
                cookies.Add(uri, new Cookie(cookie.Name, cookie.Value));
            }
            HttpClientHandler handler = new() { CookieContainer = cookies };
            client = new HttpClient(handler) { BaseAddress = uri };
        }

        public PriceHistory GetPriceHistory(string hash)
        {
            string url = string.Format("/market/pricehistory?appid=730&country=PL&currency={0}&market_hash_name={1}", (int)Currency.PLN, hash);
            return Get<PriceHistory>(url);
        }

        public string GetIconUrl(string hash)
        {
            string url = string.Format("/market/search/render/?query={0}&norender=1&count=1&search_descriptions=0appid=730", hash);
            Search search = Get<Search>(url);
            return string.Format("https://steamcommunity-a.akamaihd.net/economy/image/{0}", search.Results.First().AssetDescription.IconUrl);
        }

        private T Get<T>(string url)
        {
            HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();
            do
            {
                if (!response.IsSuccessStatusCode) 
                {
                    logger.LogInformation("Cookies expired");
                    steamAuth.Relogin();
                    logger.LogInformation("Retrying");
                    response = client.GetAsync(url).GetAwaiter().GetResult();
                }
            } while (!response.IsSuccessStatusCode);
            return response.Content.ReadFromJsonAsync<T>().GetAwaiter().GetResult();
        }
    }
}
