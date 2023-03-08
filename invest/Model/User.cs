using invest.Model.Steam;

namespace invest.Model
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public Currency DefaultCurrency { get; set; }
        public List<UserItem> Items { get; set; }
    }
}
