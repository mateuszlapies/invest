﻿namespace invest.Model
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Hash { get; set; }
        public string? Url { get; set; }
        public double BuyPrice { get; set; }
        public int BuyAmount { get; set; }
        public List<Point> Points { get; set; }
    }
}
