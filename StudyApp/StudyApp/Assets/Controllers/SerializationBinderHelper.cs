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

    internal class SerializationBinderHelper : ISerializationBinder {

        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
            TypeNameHandling = TypeNameHandling.Objects,
            Formatting = Formatting.Indented,
            SerializationBinder = new SerializationBinderHelper()
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