using invest.Model;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System;
using Cookie = invest.Model.Cookie;

namespace invest.Steam
{
    public class SteamAuth
    {
        private ILogger<SteamAuth> logger;
        private DatabaseContext context;
        private IConfigurationSection section;

        private Uri Uri { get; set; }

        public SteamAuth(ILogger<SteamAuth> logger, IConfiguration configuration, DatabaseContext context)
        {
            this.logger = logger;
            this.context = context;
            section = configuration.GetSection("Steam");
            Uri = new Uri("https://steamcommunity.com");
        }

        private bool Test()
        {
            CookieContainer cookies = new();
            foreach (Cookie cookie in context.Cookies.Where(q => q.Expires > DateTime.UtcNow))
            {
                cookies.Add(Uri, new System.Net.Cookie(cookie.Name, cookie.Value));
            }
            using HttpClientHandler handler = new() { CookieContainer = cookies };
            using HttpClient client = new HttpClient(handler) { BaseAddress = Uri };
            string response = client.GetStringAsync("my").GetAwaiter().GetResult();
            return true;
        }

        public void Login()
        {
            if (!context.Cookies.Any(q => q.Expires > DateTime.UtcNow) || Test())
            {
                RemoveAllCookies();
                string user = section.GetValue<string>("User");
                string pass = section.GetValue<string>("Pass");
                Login(user, pass).GetAwaiter().GetResult();
            }
        }

        private async Task Login(string user, string pass)
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = Uri
            };
            client.DefaultRequestHeaders.Add("Referer", "https://steamcommunity.com/login");

            FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", user)
            });
            HttpResponseMessage rsaResponse = await client.PostAsync("/login/getrsakey", content);
            logger.LogInformation(await rsaResponse.Content.ReadAsStringAsync());
            GetRsaKey rsaObject = await rsaResponse.Content.ReadFromJsonAsync<GetRsaKey>();

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            RSAParameters rsaParameters = new RSAParameters
            {
                Exponent = StringToByteArray(rsaObject.publickey_exp),
                Modulus = StringToByteArray(rsaObject.publickey_mod)
            };

            rsa.ImportParameters(rsaParameters);

            byte[] bytePassword = Encoding.ASCII.GetBytes(pass);
            byte[] encodedPassword = rsa.Encrypt(bytePassword, false);
            string encryptedBase64Password = Convert.ToBase64String(encodedPassword);

            logger.LogInformation("Logging into Steam Community...");

            SteamResult jsonObject = null;

            do
            {
                string time = Uri.EscapeDataString(rsaObject.timestamp);
                var unixTimestamp = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;

                string captchagid = string.Empty;
                string captchatext = string.Empty;
                if (jsonObject != null && jsonObject.captcha_needed)
                {
                    captchagid = jsonObject.captcha_gid.ToString();
                    captchatext = Console.ReadLine();
                }

                content = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("captcha_text", captchatext),
                    new KeyValuePair<string, string>("captchagid", captchagid),
                    new KeyValuePair<string, string>("password", encryptedBase64Password),
                    new KeyValuePair<string, string>("remember_login", "true"),
                    new KeyValuePair<string, string>("rsatimestamp", time),
                    new KeyValuePair<string, string>("username", user),
                    new KeyValuePair<string, string>("donotcache", unixTimestamp.ToString())
                });

                HttpResponseMessage loginResponse = await client.PostAsync("/login/dologin", content);
                if (loginResponse.IsSuccessStatusCode)
                {
                    jsonObject = await loginResponse.Content.ReadFromJsonAsync<SteamResult>();
                    if (!jsonObject.success)
                    {
                        if (jsonObject.captcha_needed)
                        {
                            logger.LogInformation("Provide captcha: {link}", "https://steamcommunity.com/login/rendercaptcha?gid=" + jsonObject.captcha_gid);
                        }
                        else
                        {
                            logger.LogError("Login failed: {msg}", jsonObject.message);
                        }
                    }
                    else
                    {
                        loginResponse.Headers.TryGetValues("Set-Cookie", out var cookies);
                        List<Cookie> _cookies = new List<Cookie>();
                        foreach (var cookie in cookies)
                        {
                            string[] temp = cookie.Split("; ")[0].Split("=");
                            string name = temp[0];
                            string value = temp[1];
                            _cookies.Add(new Cookie() { Name = name, Value = value, Expires = DateTime.UtcNow.AddDays(6) });
                        }
                        _cookies.Add(new Cookie() { Name = "sessionid", Value = GenerateSessionId(), Expires = DateTime.UtcNow.AddDays(6) });
                        context.Cookies.AddRange(_cookies);
                        context.SaveChanges();
                    }
                }
                else
                {
                    logger.LogError("Login failed: {msg}", loginResponse.StatusCode);
                    throw new Exception("Login failed");
                }
            } while (jsonObject == null || !jsonObject.success);
        }

        private byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        private string ByteArrayToString(byte[] array)
        {
            StringBuilder hex = new StringBuilder(array.Length * 2);
            foreach (byte b in array)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private string GenerateSessionId()
        {
            Random rnd = new Random();
            byte[] b = new byte[12];
            rnd.NextBytes(b);
            return ByteArrayToString(b);
        }

        private void RemoveAllCookies()
        {
            context.Database.ExecuteSqlRaw("TRUNCATE ONLY public.\"Cookies\" RESTART IDENTITY");
            context.SaveChanges();
        }
    }

    public class GetRsaKey
    {
        public bool success { get; set; }
        public string publickey_mod { get; set; }
        public string publickey_exp { get; set; }
        public string timestamp { get; set; }
    }

    public class SteamResult
    {
        public bool success { get; set; }
        public string message { get; set; }
        public bool requires_twofactor { get; set; }
        public bool captcha_needed { get; set; }
        public object captcha_gid { get; set; }
    }
}