using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace api.net5.Middleware
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string _dateFormat = "dd/MM/yyyy";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string dateString = reader.GetString();
            if (DateTime.TryParseExact(dateString, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                return date;
            }
            throw new JsonException($"Date in invalid format. Use the format {_dateFormat}.");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_dateFormat));
        }
    }
}
