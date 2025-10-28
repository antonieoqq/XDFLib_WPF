using Newtonsoft.Json;
using System;
using System.Numerics;

namespace XDFLib_WPF.Json
{
    public class Vector2Converter : JsonConverter<Vector2>
    {
        public override Vector2 ReadJson(
            JsonReader reader, Type objectType, Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string s = (string)reader.Value;
            return Parse2Vec2(s);
        }

        public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
        {
            var str = $"{value.X},{value.Y}";
            writer.WriteValue(str);
        }

        public static Vector2 Parse2Vec2(string str)
        {
            var s = str.Split(',');
            return new Vector2(float.Parse(s[0]), float.Parse(s[1]));
        }
    }
}
