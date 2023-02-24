﻿using System.Text.Json.Serialization;

namespace invest.Steam.Data
{
    public class Price
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("lowest_price")]
        public string LowestPrice { get; set; }
        [JsonPropertyName("volume")]
        public string Volume { get; set; }
        [JsonPropertyName("median_price")]
        public string MedianPrice { get; set; }
    }
}
