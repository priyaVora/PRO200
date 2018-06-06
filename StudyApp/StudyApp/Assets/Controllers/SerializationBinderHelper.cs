using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StudyApp.Assets.Controllers {

    internal class ByteArrayConverter : JsonConverter {

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            string base64String = Convert.ToBase64String((byte[]) value);

            serializer.Serialize(writer, base64String);
        }

        public override bool CanRead {
            get { return false; }
        }

        public override bool CanConvert(Type t) {
            return typeof(byte[]).IsAssignableFrom(t);
        }
    }

    internal class SerializationBinderHelper : ISerializationBinder {

        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
            TypeNameHandling = TypeNameHandling.Objects,
            Formatting = Formatting.Indented,
            SerializationBinder = new SerializationBinderHelper(),
            Converters = { new ByteArrayConverter() }
        };

        public IList<Type> KnownTypes { get; set; }

        public SerializationBinderHelper() {
            KnownTypes = Assembly.GetExecutingAssembly().GetTypes().ToList();
        }

        public Type BindToType(string assemblyName, string typeName) {
            if (typeName == "Dictionary`2") {
                return typeof(Dictionary<string, StudyApp.Assets.Models.Permission>);
            } 
            return KnownTypes.First(t => t.Name == typeName);
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName) {
            assemblyName = null;
            if (serializedType.Name == "Dictionary`2")
            {
                typeName = typeof(Dictionary<string, StudyApp.Assets.Models.Permission>).Name;
            } else {
                typeName = serializedType.Name;
            }
        }
    }
}