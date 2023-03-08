using Hangfire;
using Hangfire.Storage;
using invest.Model;
using invest.Model.Steam;
using invest.Steam.Data;
using System.Net;
using System.Web;
using Cookie = System.Net.Cookie;

namespace invest.Services
{
    public class SteamService
    {
        private readonly ILogger<SteamService> logger;
        private readonly HttpClient client;

        public SteamService(ILogger<SteamService> logger, DatabaseContext context)
        {
            this.logger = logger;

            Uri uri = new("https://steamcommunity.com");
            CookieContainer cookies = new();
            foreach (Model.Steam.Cookie cookie in context.Cookies.Where(q => q.Expires > DateTime.UtcNow))
            {
                cookies.Add(uri, new Cookie(cookie.Name, cookie.Value));
            }
            HttpClientHandler handler = new() { CookieContainer = cookies };
            client = new HttpClient(handler) { BaseAddress = uri };
        }

        public Daily GetPrice(string hash)
        {
            string url = string.Format("/market/priceoverview?appid=730&market_hash_name={0}&currency={1}", hash, (int)Currency.USD);
            Price price = Get<Price>(url);
            return new Daily()
            {
                Volume = int.Parse(GetNumber(price.Volume)),
                Price = double.Parse(GetDecimalNumber(price.LowestPrice)),
                MedianPrice = double.Parse(GetDecimalNumber(price.MedianPrice))
            };
        }

        private string GetNumber(string text)
        {
            return string.Concat(text.Where(char.IsDigit));
        }

        private string GetDecimalNumber(string text)
        {
            return string.Concat(text.Where(c => char.IsDigit(c) || c == '.' || c == ','));
        }

        public PriceHistory GetPriceHistory(string hash)
        {
            string url = string.Format("/market/pricehistory?appid=730&country=PL&currency={0}&market_hash_name={1}", (int)Currency.USD, hash);
            return Get<PriceHistory>(url);
        }

        public string GetIconUrl(string hash)
        {
            string url = string.Format("/market/search/render/?query={0}&norender=1&count=1&search_descriptions=0&appid=730", hash);
            Search search = Get<Search>(url);
            return string.Format("https://steamcommunity-a.akamaihd.net/economy/image/{0}", search.Results.First().AssetDescription.IconUrl);
        }

        public Search SearchItems(string query)
        {
            string url = string.Format("/market/search/render/?query={0}&norender=1&search_descriptions=0appid=730", HttpUtility.UrlEncode(query));
            Search search = Get<Search>(url);
            return search;
        }

        private T Get<T>(string url)
        {
            HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();
            try
            {
                return response.Content.ReadFromJsonAsync<T>().GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                logger.LogError("Steam request failed with: {exception}", e);
                throw;
            }
        }
    }
}
