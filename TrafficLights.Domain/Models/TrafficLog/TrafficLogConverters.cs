using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TrafficLights.Domain.Models.TrafficLog;

public class TrafficLogConverters
{
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        private const string ModelFormat = "dd-MM-yyyy HH:mm:ss";

        public override DateTime Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
            DateTime.ParseExact(reader.GetString()!, ModelFormat, CultureInfo.InvariantCulture);

        public override void Write(
            Utf8JsonWriter writer,
            DateTime dateTimeValue,
            JsonSerializerOptions options) =>
            writer.WriteStringValue(dateTimeValue.ToString(ModelFormat, CultureInfo.InvariantCulture));
    }
}