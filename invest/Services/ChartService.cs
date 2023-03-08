using invest.Data.Types;
using invest.Model;
using invest.Model.Steam;
using Microsoft.EntityFrameworkCore;

namespace invest.Services
{
    public class ChartService
    {
        private readonly DatabaseContext context;

        public ChartService(DatabaseContext context)
        {
            this.context = context;
        }

        public UserItem Get(Guid id, ChartType type)
        {
            UserItem item = context.UserItems.Where(q => q.UserItemId == id).Include(i => i.Item).FirstOrDefault();
            switch (type)
            {
                case ChartType.Overall:
                    {
                        item.Item.Points = context.Points
                                .Where(q => q.ItemId == item.Item.ItemId)
                                .GroupBy(g => g.Date.Date)
                                .Select(s => new Point() { Date = s.Key, Value = Math.Round(s.Average(sel => sel.Value), 2), Volume = s.Sum(sel => sel.Volume) })
                                .OrderBy(o => o.Date)
                                .ToList();
                        break;
                    }

                case ChartType.Year:
                    {
                        item.Item.Points = context.Points
                                .Where(q => q.ItemId == item.Item.ItemId && q.Date > DateTime.UtcNow.AddYears(-1))
                                .GroupBy(g => g.Date.Date)
                                .Select(s => new Point() { Date = s.Key, Value = Math.Round(s.Average(sel => sel.Value), 2), Volume = s.Sum(sel => sel.Volume) })
                                .OrderBy(o => o.Date)
                                .ToList();
                        break;
                    }

                case ChartType.Month:
                    {
                        item.Item.Points = context.Points
                                .Where(q => q.ItemId == item.Item.ItemId && q.Date > DateTime.UtcNow.AddMonths(-1))
                                .GroupBy(g => g.Date.Date)
                                .Select(s => new Point() { Date = s.Key, Value = Math.Round(s.Average(sel => sel.Value), 2), Volume = s.Sum(sel => sel.Volume) })
                                .OrderBy(o => o.Date)
                                .ToList();
                        break;
                    }

                case ChartType.Week:
                    {
                        item.Item.Points = context.Points
                                .Where(q => q.ItemId == item.Item.ItemId && q.Date > DateTime.UtcNow.AddDays(-7))
                                .OrderBy(o => o.Date)
                                .ToList();
                        break;
                    }

                case ChartType.Day:
                    {
                        item.Item.Points = context.Points
                                .Where(q => q.ItemId == item.Item.ItemId && q.Date > DateTime.UtcNow.AddDays(-1))
                                .OrderBy(o => o.Date)
                                .ToList();
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
            return item;
        }
    }
}
