using System;
using System.Collections.Generic;
using System.IO;
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
using Newtonsoft.Json;
using StudyApp.Assets.Models;
using File = StudyApp.Assets.Models.File;

namespace StudyApp.Assets.Controllers
{
    class ServerIOController
    {
        public static readonly string IP = "104.42.173.109";

        public void DeleteFile(string guid)
        {
            PassToServer(guid, "file", "DeleteFile");

        }
        public void DeleteNote(string guid)
        {
            PassToServer(guid, "note", "DeleteNote");
        }

        public void CreateNote(Note note)
        {
            string json = "";
            JsonSerializer serializer = JsonSerializer.Create();
            serializer.Serialize(new StringWriter(new StringBuilder(json)),note);

            PassToServer(json, "note", "CreateNote");
        }

        public void UploadFile(File file)
        {
            string json = "";
            JsonSerializer serializer = JsonSerializer.Create();
            serializer.Serialize(new StringWriter(new StringBuilder(json)), file);

            PassToServer(json, "file", "UploadFile");
        }

        public void CreateGoal(Goal goal)
        {
            string json = "";
            JsonSerializer serializer = JsonSerializer.Create();
            serializer.Serialize(new StringWriter(new StringBuilder(json)), goal);

            PassToServer(json, "goal", "CreateGoal");
        }

        private static async void PassToServer(string content, string modelType, string controller)
        {
            string url = $"https://{IP}/{modelType}/{controller}/{content}";
            HttpClient client = new HttpClient();
            await client.PostAsync(url, null);
        }



        //TODO: I DON'T KNOW IF THIS WILL WORK. PLEASE TEST.
        public async Task<bool> AuthenticateUser(string username, string password)
        {
            string url = $"https://{IP}/user/AuthenticateUser/username={username}&password={password}";
            HttpClient client = new HttpClient();
            var response = await client.PostAsync(url, null);
            Task<string> thing = response.Content.ReadAsStringAsync();
            Newtonsoft.Json.JsonReader reader = new JsonTextReader(new StringReader(thing.Result));
            return reader.ReadAsBoolean() ?? false;
        }
    }
}