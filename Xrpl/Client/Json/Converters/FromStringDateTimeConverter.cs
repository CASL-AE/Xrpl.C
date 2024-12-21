using Newtonsoft.Json;

using System;
using System.Globalization;

namespace Xrpl.Client.Json.Converters;

public class FromStringDateTimeConverter : JsonConverter
{
    private static DateTime RippleStartTime = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(DateTime);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        switch (reader.TokenType)
        {
            case JsonToken.Null: return null;

            case JsonToken.Date:
                {
                    return (DateTime?)reader.Value;
                }
            case JsonToken.String:
                {
                    string dateTimeString = (string)reader.Value;
                    // Попробуем разобрать строку в DateTime
                    if (DateTime.TryParseExact(dateTimeString, "yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime dateTime))
                    {
                        return dateTime;
                    }

                    return null;
                }
            case JsonToken.Integer:
                {
                    double totalSeconds;

                    try
                    {
                        totalSeconds = Convert.ToDouble(reader.Value, CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        throw new Exception("Invalid double value.");
                    }

                    return RippleStartTime.AddSeconds(totalSeconds);
                }
            default: throw new Exception("Invalid token. Expected string");
        }

        throw new JsonSerializationException("Expected string value for DateTime.");
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is DateTime dateTime)
        {
            // Записываем в формате ISO 8601
            writer.WriteValue(dateTime.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture));
        }
        else
        {
            throw new JsonSerializationException("Expected DateTime value.");
        }
    }
}