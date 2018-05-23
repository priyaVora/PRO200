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
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StudyApp.Assets.Models;
using File = StudyApp.Assets.Models.File;

namespace StudyApp.Assets.Controllers
{
    class ServerIOController
    {
        public static readonly string IP = "104.42.173.109";

        /*
          _____                      _ 
         / ____|                    | |
        | (___     ___   _ __     __| |
         \___ \   / _ \ | '_ \   / _` |
         ____) | |  __/ | | | | | (_| |
        |_____/   \___| |_| |_|  \__,_|
        Does not expect returns
        */

        #region Send
        public void DeleteFile(string guid)
        {
            PassToServer($"guid={guid}", "file", "DeleteFile");

        }
        public void DeleteNote(string guid)
        {
            PassToServer($"guid={guid}", "note", "DeleteNote");
        }

        public void CreateNote(Note note, string username)
        {
            string json = "";
            JsonSerializer serializer = JsonSerializer.Create();
            serializer.Serialize(new StringWriter(new StringBuilder(json)),note);

            PassToServer($"note={json}&username={username}", "note", "CreateNote");
        }

        public void UploadFile(File file)
        {
            string json = "";
            JsonSerializer serializer = JsonSerializer.Create();
            serializer.Serialize(new StringWriter(new StringBuilder(json)), file);

            PassToServer($"f={json}", "file", "UploadFile");
        }

        public void CreateGoal(Goal goal)
        {
            string json = "";
            JsonSerializer serializer = JsonSerializer.Create();
            serializer.Serialize(new StringWriter(new StringBuilder(json)), goal);

            PassToServer($"content={json}", "goal", "CreateGoal");
        }

        public void MarkGoalAsCompleted(string guid, string username)
        {
            PassToServer($"username={username}&goalGuid={guid}", "goal", "CompleteGoal");
        }

        public void ShareFile(string guid, Dictionary<string, Permission> shareWith)
        {
            string json = "";
            JsonSerializer serializer = JsonSerializer.Create();
            serializer.Serialize(new StringWriter(new StringBuilder(json)), shareWith);

            PassToServer($"guid={guid}&shareWith={json}", "file", "ShareFile");
        }

        private static async void PassToServer(string content, string modelType, string controller)
        {
            string url = $"https://{IP}/{modelType}/{controller}?{content}";
            HttpClient client = new HttpClient();
            await client.PostAsync(url, null);
        }
        #endregion

        /*
         _____                         _                
        |  __ \                       (_)               
        | |__) |   ___    ___    ___   _  __   __   ___ 
        |  _  /   / _ \  / __|  / _ \ | | \ \ / /  / _ \
        | | \ \  |  __/ | (__  |  __/ | |  \ V /  |  __/
        |_|  \_\  \___|  \___|  \___| |_|   \_/    \___|
        Expects returns
        */

        #region Receive

        public async Task<bool> AuthenticateUser(string username, string password)
        {
            string url = $"https://{IP}/user/AuthenticateUser?username={username}&password={password}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(url, null);
            Task<string> result = response.Content.ReadAsStringAsync();
            Newtonsoft.Json.JsonReader reader = new JsonTextReader(new StringReader(result.Result));
            return reader.ReadAsBoolean() ?? false;
        }

        public async Task<UserAccount> GetUser(string username)
        {
            string url = $"https://{IP}/user/GetUser?username={username}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(url, null);
            Task<string> result = response.Content.ReadAsStringAsync();
            Newtonsoft.Json.JsonReader reader = new JsonTextReader(new StringReader(result.Result));

            JsonSerializer serializer = JsonSerializer.Create();
            UserAccount user = serializer.Deserialize<UserAccount>(reader);

            return user;
        }

        public async Task<List<Goal>> GetUpcomingGoals(string username)
        {

            string url = $"https://{IP}/goal/GetUpcomingRecurringGoals?username={username}&curDate={DateTime.Now}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(url, null);
            Task<string> result = response.Content.ReadAsStringAsync();
            Newtonsoft.Json.JsonReader reader = new JsonTextReader(new StringReader(result.Result));


            JsonSerializer serializer = JsonSerializer.Create();
            List<Goal> goals = serializer.Deserialize<List<Goal>>(reader);
            
            url = $"https://{IP}/goal/GetUpcomingNonRecurringGoals?username={username}&curDate={DateTime.Now}";
            response = await client.PostAsync(url, null);
            result = response.Content.ReadAsStringAsync();
            reader = new JsonTextReader(new StringReader(result.Result));

            goals.AddRange(serializer.Deserialize<List<Goal>>(reader));
            
            return goals;
        }

        public async Task<List<FileMini>> GetFilePreviews(string username)
        {
            string url = $"https://{IP}/file/GetFilePreviews?username={username}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(url, null);
            Task<string> result = response.Content.ReadAsStringAsync();
            Newtonsoft.Json.JsonReader reader = new JsonTextReader(new StringReader(result.Result));

            JsonSerializer serializer = JsonSerializer.Create();
            List<FileMini> minis = serializer.Deserialize<List<FileMini>>(reader);

            return minis;
        }

        #endregion
        
    }
}