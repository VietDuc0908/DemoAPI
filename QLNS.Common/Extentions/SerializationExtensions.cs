using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNS.Common.Extentions
{
    public static class SerializationExtensions
    {
        internal static Newtonsoft.Json.JsonSerializer Serializer;

        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Populate,
            TypeNameHandling = TypeNameHandling.Auto,
            ContractResolver = new LowercaseContractResolver()
        };

        static SerializationExtensions()
        {
            Serializer = Newtonsoft.Json.JsonSerializer.Create(JsonSerializerSettings);
        }

        public static T Convert<T>(this object obj)
        {
            if (obj == null)
            {
                return default;
            }
            return obj.JsonSerialize().JsonDeserialize<T>();
        }

        public static string JsonSerialize<T>(this T obj)
        {
            if (obj == null)
            {
                return null;
            }
            return JsonConvert.SerializeObject(obj, JsonSerializerSettings);
        }

        public static string JsonSerialize<T>(this T obj, JsonSerializerSettings setting)
        {
            if (obj == null)
            {
                return null;
            }

            return JsonConvert.SerializeObject(obj, setting);
        }

        public static T JsonDeserialize<T>(this string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(json, JsonSerializerSettings);
        }

        public static object JsonDeserialize(this string json, Type type)
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            return JsonConvert.DeserializeObject(json, type, JsonSerializerSettings);
        }

        public static T JsonDeserialize<T>(this Stream stream)
        {
            using var textReader = new StreamReader(stream);
            using var jsonReader = new JsonTextReader(textReader);
            return Serializer.Deserialize<T>(jsonReader);
        }
    }
    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName) => propertyName.ToLowerInvariant();
    }

    public class JsonLowercaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name.ToLowerInvariant();
    }
}

