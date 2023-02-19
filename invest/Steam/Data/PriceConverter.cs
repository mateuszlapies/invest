using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace invest.Steam.Data
{
    public class PriceConverter : JsonConverter
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(Price);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                return null;
            }

            JArray array = JArray.Load(reader);
            Price price = existingValue as Price ?? new Price();
            price.Date = DateTime.Parse(array[0].ToString());
            price.Value = double.Parse(array[1].ToString());
            price.Volume = int.Parse(array[2].ToString());
            return price;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Price price = (Price)value;
            serializer.Serialize(writer, new List<object>() { price.Date, price.Value, price.Volume });
        }
    }
}
