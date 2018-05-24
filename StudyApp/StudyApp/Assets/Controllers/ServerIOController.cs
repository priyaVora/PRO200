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
          _____                      _     _____            __         
         / ____|                    | |   |_   _|          / _|        
        | (___     ___   _ __     __| |     | |    _ __   | |_    ___  
         \___ \   / _ \ | '_ \   / _` |     | |   | '_ \  |  _|  / _ \ 
         ____) | |  __/ | | | | | (_| |    _| |_  | | | | | |   | (_) |
        |_____/   \___| |_| |_|  \__,_|   |_____| |_| |_| |_|    \___/
        Does not expect returns
        */

        #region Send

        public void CreateUser(UserAccount user, string password)
        {

            string json = "";
            JsonSerializer serializer = JsonSerializer.Create();
            serializer.Serialize(new StringWriter(new StringBuilder(json)), user);

            PassToServer("user", "CreateAccount", $"password={password}", json);
        }

        public void DeleteFile(string guid)
        {
            PassToServer("file", "DeleteFile", $"guid={guid}");

        }
        public void DeleteNote(string guid)
        {
            PassToServer("note", "DeleteNote", $"guid={guid}");
        }

        public void CreateNote(Note note, string username)
        {
            string json = "";
            JsonSerializer serializer = JsonSerializer.Create();
            serializer.Serialize(new StringWriter(new StringBuilder(json)),note);

            PassToServer("note", "CreateNote", $"username={username}", json);
        }

        public void UploadFile(File file)
        {
            string json = "";
            JsonSerializer serializer = JsonSerializer.Create();
            serializer.Serialize(new StringWriter(new StringBuilder(json)), file);

            PassToServer("file", "UploadFile", json: json);
        }

        public void CreateGoal(Goal goal)
        {
            string json = "";
            JsonSerializer serializer = JsonSerializer.Create();
            serializer.Serialize(new StringWriter(new StringBuilder(json)), goal);

            PassToServer("goal", "CreateGoal", json: json);
        }

        public void MarkGoalAsCompleted(string guid, string username)
        {
            PassToServer("goal", "CompleteGoal", $"username={username}&goalGuid={guid}");
        }

        public void ShareFile(string guid, Dictionary<string, Permission> shareWith)
        {
            string json = "";
            JsonSerializer serializer = JsonSerializer.Create();
            serializer.Serialize(new StringWriter(new StringBuilder(json)), shareWith);

            PassToServer("file", "ShareFile", $"guid={guid}", json);
        }

        private static void PassToServer(string modelType, string controller, string urlContent="", string json="")
        {
            HttpClient client = new HttpClient();
            string url = $"https://{IP}/{modelType}/{controller}{(string.IsNullOrWhiteSpace(urlContent) ? "" : $"?{urlContent}")}";

            if (!string.IsNullOrWhiteSpace(json))
            {
                StringContent htmlContent = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage jsonSendResult = client.PostAsync(url, htmlContent).Result;
            }
            else
            {
                HttpResponseMessage result = client.PostAsync(url, null).Result;
            }
        }
        #endregion

        /*
         _____                         _                    _____            __         
        |  __ \                       (_)                  |_   _|          / _|        
        | |__) |   ___    ___    ___   _  __   __   ___      | |    _ __   | |_    ___  
        |  _  /   / _ \  / __|  / _ \ | | \ \ / /  / _ \     | |   | '_ \  |  _|  / _ \ 
        | | \ \  |  __/ | (__  |  __/ | |  \ V /  |  __/    _| |_  | | | | | |   | (_) |
        |_|  \_\  \___|  \___|  \___| |_|   \_/    \___|   |_____| |_| |_| |_|    \___/
        Expects returns
        */

        #region Receive

        public async Task<bool> AuthenticateUser(string username, string password)
        {
            string url = $"https://{IP}/user/AuthenticateUser?username={username}&password={password}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            Task<string> result = response.Content.ReadAsStringAsync();
            Newtonsoft.Json.JsonReader reader = new JsonTextReader(new StringReader(result.Result));
            return reader.ReadAsBoolean() ?? false;
        }

        public async Task<UserAccount> GetUser(string username)
        {
            string url = $"https://{IP}/user/GetUser?username={username}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
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
            HttpResponseMessage response = await client.GetAsync(url);
            Task<string> result = response.Content.ReadAsStringAsync();
            Newtonsoft.Json.JsonReader reader = new JsonTextReader(new StringReader(result.Result));


            JsonSerializer serializer = JsonSerializer.Create();
            List<Goal> goals = serializer.Deserialize<List<Goal>>(reader);
            
            url = $"https://{IP}/goal/GetUpcomingNonRecurringGoals?username={username}&curDate={DateTime.Now}";
            response = await client.GetAsync(url);
            result = response.Content.ReadAsStringAsync();
            reader = new JsonTextReader(new StringReader(result.Result));

            goals.AddRange(serializer.Deserialize<List<Goal>>(reader));
            
            return goals;
        }

        public async Task<List<FileMini>> GetFilePreviews(string username)
        {
            string url = $"https://{IP}/file/GetFilePreviews?username={username}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            Task<string> result = response.Content.ReadAsStringAsync();
            Newtonsoft.Json.JsonReader reader = new JsonTextReader(new StringReader(result.Result));

            JsonSerializer serializer = JsonSerializer.Create();
            List<FileMini> minis = serializer.Deserialize<List<FileMini>>(reader);

            return minis;
        }

        #endregion
        
    }
}