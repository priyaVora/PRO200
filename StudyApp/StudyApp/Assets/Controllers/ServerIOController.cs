using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
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

    public class ServerIOController
    {
        public static readonly string IP = "104.42.173.109";
        private HttpClient client;

        public ServerIOController() {
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
            client = new HttpClient();
        }
        

        #region Send

        #region User
        
        public void CreateUser(UserAccount user, string password)
        {

            string json = JsonConvert.SerializeObject(user);
            PassToServer("user", "CreateAccount", $"password={password}", json);
        }
        public void AddPoints(string username, int pointsToAdd)
        {
            PassToServer("user", "AddPoints", $"username={username}&pointsToAdd={pointsToAdd}");
        }
        #endregion

        #region File
        public void DeleteFile(string guid)
        {
            PassToServer("file", "DeleteFile", $"guid={guid}");
        }
        public void UploadFile(File file)
        {
            string json = JsonConvert.SerializeObject(file);
            PassToServer("file", "UploadFile", json: json);
        }
        public void ShareFile(string guid, Dictionary<string, Permission> shareWith)
        {
            string json = JsonConvert.SerializeObject(shareWith);
            PassToServer("file", "ShareFile", $"guid={guid}", json);
        }

        #endregion

        #region Note
        public void DeleteNote(string guid)
        {
            PassToServer("note", "DeleteNote", $"guid={guid}");
        }

        public void CreateNote(Note note, string username)
        {
            string json = JsonConvert.SerializeObject(note);

            PassToServer("note", "CreateNote", $"username={username}", json);
        }

        public void UpdateNote(Note note)
        {
            string json = JsonConvert.SerializeObject(note);
            PassToServer("note", "UpdateNote", json: json);
        }

        #endregion

        #region Goal
        //public void CreateGoal(Goal goal, string username)
        //{
        //    string json = JsonConvert.SerializeObject(goal);
        //    PassToServer("goal", "CreateGoal", $"username={username}", json);
        //}

        public void CreateNonRecurringGoal(NonRecurringGoal goal, string username) {
            PassToServer("goal", "CreateNonRecurringGoal", $"username={username}", JsonConvert.SerializeObject(goal));
        }

        public void CreateRecurringGoal(RecurringGoal goal, string username) {
            PassToServer("goal", "CreateRecurringGoal", $"username={username}", JsonConvert.SerializeObject(goal));
        }

        public void MarkGoalAsCompleted(string guid, string username)
        {
            PassToServer("goal", "CompleteGoal", $"username={username}&goalGuid={guid}");
        }
        #endregion

        private void PassToServer(string modelType, string controller, string urlContent="", string json="")
        {
            
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
        
        #region Receive

        #region User
        public bool AuthenticateUser(string username, string password)
        {
            string url = $"https://{IP}/user/AuthenticateUser?username={username}&password={password}";
            
            HttpResponseMessage response = client.GetAsync(url).Result;
            Task<string> result = response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<bool>(result.Result);
        }
        public bool DoesUserExist(string username)
        {
            string url = $"https://{IP}/user/doesUserExist?username={username}";
            
            HttpResponseMessage response = client.GetAsync(url).Result;
            Task<string> result = response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<bool>(result.Result);
        }

        public UserAccount GetUser(string username)
        {
            string url = $"https://{IP}/user/GetUser?username={username}";
            
            HttpResponseMessage response = client.GetAsync(url).Result;
            Task<string> result = response.Content.ReadAsStringAsync();

            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.Objects
            };

            return JsonConvert.DeserializeObject<UserAccount>(result.Result, settings);

        }
        #endregion

        #region Goal
        public Goal GetGoal(string guid, string username)
        {
            string url = $"https://{IP}/user/GetUser?guid={guid}&username={username}";
            
            HttpResponseMessage response = client.GetAsync(url).Result;
            Task<string> result = response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Goal>(result.Result);

        }

        public List<Goal> GetUpcomingGoals(string username)
        {

            string url = $"https://{IP}/goal/GetUpcomingRecurringGoals?username={username}&dateString={DateTime.Now.ToShortDateString()}";
            
            HttpResponseMessage response = client.GetAsync(url).Result;
            Task<string> result = response.Content.ReadAsStringAsync();
            try
            {


                List<Goal> goals = JsonConvert.DeserializeObject<List<Goal>>(result.Result);

                url = $"https://{IP}/goal/GetUpcomingNonRecurringGoals?username={username}&dateString={DateTime.Now.ToShortDateString()}";
                response = client.GetAsync(url).Result;
                result = response.Content.ReadAsStringAsync();
                goals.AddRange(JsonConvert.DeserializeObject<List<Goal>>(result.Result));
                return goals;
            } 
            catch
            {

            }
            return new List<Goal>();
        }
        public List<Goal> GetOverdueGoals(string username)
        {
            string url = $"https://{IP}/goal/GetOverdueGoals?username={username}&dateString={DateTime.Now.ToShortDateString()}";
            
            HttpResponseMessage response = client.GetAsync(url).Result;
            Task<string> result = response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Goal>>(result.Result);

        }
        #endregion

        #region File
        public List<FileMini> GetFilePreviews(string username)
        {
            string url = $"https://{IP}/file/GetFilePreviews?username={username}";
            
            HttpResponseMessage response = client.GetAsync(url).Result;
            Task<string> result = response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<FileMini>>(result.Result);

        }
        public File DownloadFile(string guid)
        {
            string url = $"https://{IP}/file/DownloadFile?guid={guid}";
            
            HttpResponseMessage response = client.GetAsync(url).Result;
            Task<string> result = response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<File>(result.Result);

        }
        #endregion

        #region Note
        public Note GetNote(string guid, string username)
        {
            string url = $"https://{IP}/user/GetUser?guid={guid}&username={username}";
            
            HttpResponseMessage response = client.GetAsync(url).Result;
            Task<string> result = response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Note>(result.Result);

        }
        public List<NoteMini> GetNotePreviews(string username)
        {
            string url = $"https://{IP}/note/GetNotePreviews?username={username}";
            
            HttpResponseMessage response = client.GetAsync(url).Result;
            Task<string> result = response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<NoteMini>>(result.Result);

        }
        #endregion

        #region Caldendar
        public Month GetMonth(string username, int monthOfYear)
        {
            string url = $"https://{IP}/file/DownloadFile?username={username}&monthOfYear={monthOfYear}";
            
            HttpResponseMessage response = client.GetAsync(url).Result;
            Task<string> result = response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Month>(result.Result);

        }
        #endregion

        #endregion

    }
}