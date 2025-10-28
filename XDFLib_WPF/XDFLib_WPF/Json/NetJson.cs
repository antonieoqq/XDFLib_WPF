using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Formatting = Newtonsoft.Json.Formatting;

namespace XDFLib_WPF.Json
{
    public static class NetJson
    {
        public static readonly JsonSerializerSettings Settings;

        static NetJson()
        {
            Settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented,
            };

            Settings.Converters.Add(new StringEnumConverter());
            Settings.Converters.Add(new Vector2Converter());
            Settings.Converters.Add(new Vector3Converter());
        }

        public static string ToJson<T>(T obj)
        {
            var json = JsonConvert.SerializeObject(obj, Settings);
            return json;
        }

        public static T FromJson<T>(string json)
        {
            var t = JsonConvert.DeserializeObject<T>(json, Settings);
            return t;
        }

        public static T Clone<T>(T obj)
        {
            var json = ToJson(obj);
            var result = FromJson<T>(json);
            return result;
        }
    }
}
