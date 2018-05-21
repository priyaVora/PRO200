using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;

namespace StudyApp.Assets.Controllers
{
    class ServerIOController
    {
        public static readonly string IP = "104.42.173.109";
        //TODO: I DON'T KNOW IF THIS WILL WORK. PLEASE TEST.
        public async Task<bool> AuthenticateUser(string username, string password)
        {
            string url = $"https://{IP}/AuthenticateUser/username={username}&password={password}";
            HttpClient client = new HttpClient();
            var response = await client.PostAsync(url, null);
            Task<string> thing = response.Content.ReadAsStringAsync();
            JsonReader reader = new JsonReader(new StringReader(thing.Result));
            bool val = false;
            while (reader.HasNext)
            {
                val = reader.NextBoolean();
            }
            return val;
        }
    }
}