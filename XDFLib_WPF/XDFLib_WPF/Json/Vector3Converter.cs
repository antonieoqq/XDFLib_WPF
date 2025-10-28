using Newtonsoft.Json;
using System;
using System.Numerics;

namespace XDFLib_WPF.Json
{
    public class Vector3Converter : JsonConverter<Vector3>
    {
        public override Vector3 ReadJson(
            JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string s = (string)reader.Value;
            return Parse2Vec3(s);
        }

        public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
        {
            var str = $"{value.X},{value.Y},{value.Z}";
            writer.WriteValue(str);
        }

        public static Vector3 Parse2Vec3(string str)
        {
            var s = str.Split(',');
            return new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));
        }
    }
}
