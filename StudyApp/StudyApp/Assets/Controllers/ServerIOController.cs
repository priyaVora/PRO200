﻿using System;
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
          _____          _       _____            __         
         / ____|        | |     |_   _|          / _|        
        | |  __    ___  | |_      | |    _ __   | |_    ___  
        | | |_ |  / _ \ | __|     | |   | '_ \  |  _|  / _ \ 
        | |__| | |  __/ | |_     _| |_  | | | | | |   | (_) |
         \_____|  \___|  \__|   |_____| |_| |_| |_|    \___/
        Does not expect returns
        */

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
            string json = "";
            JsonSerializer serializer = JsonSerializer.Create();
            serializer.Serialize(new StringWriter(new StringBuilder(json)), note);

            PassToServer("note", "UpdateNote", json: json);
        }

        #endregion

        #region Goal
        public void CreateGoal(Goal goal, string username)
        {
            string json = JsonConvert.SerializeObject(goal);
            PassToServer("goal", "CreateGoal", $"username={username}", json);
        }

        public void MarkGoalAsCompleted(string guid, string username)
        {
            PassToServer("goal", "CompleteGoal", $"username={username}&goalGuid={guid}");
        }
        #endregion

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
         _____                  _       _____            __         
        |  __ \                | |     |_   _|          / _|        
        | |__) |   ___    ___  | |_      | |    _ __   | |_    ___  
        |  ___/   / _ \  / __| | __|     | |   | '_ \  |  _|  / _ \ 
        | |      | (_) | \__ \ | |_     _| |_  | | | | | |   | (_) |
        |_|       \___/  |___/  \__|   |_____| |_| |_| |_|    \___/
        Expects returns
        */

        #region Receive

        #region User
        public bool AuthenticateUser(string username, string password)
        {
            string url = $"https://{IP}/user/AuthenticateUser?username={username}&password={password}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            Task<string> result = response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<bool>(result.Result);
        }
        public bool DoesUserExist(string username)
        {
            string url = $"https://{IP}/user/doesUserExist?username={username}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            Task<string> result = response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<bool>(result.Result);
        }

        public UserAccount GetUser(string username)
        {
            string url = $"https://{IP}/user/GetUser?username={username}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            Task<string> result = response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserAccount>(result.Result);

        }
        #endregion

        #region Goal
        public Goal GetGoal(string guid, string username)
        {
            string url = $"https://{IP}/user/GetUser?guid={guid}&username={username}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            Task<string> result = response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Goal>(result.Result);

        }

        public List<Goal> GetUpcomingGoals(string username)
        {

            string url = $"https://{IP}/goal/GetUpcomingRecurringGoals?username={username}&dateString={DateTime.Now.ToShortDateString()}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            Task<string> result = response.Content.ReadAsStringAsync();
            List<Goal> goals = JsonConvert.DeserializeObject<List<Goal>>(result.Result);
            
            url = $"https://{IP}/goal/GetUpcomingNonRecurringGoals?username={username}&dateString={DateTime.Now.ToShortDateString()}";
            response = client.GetAsync(url).Result;
            result = response.Content.ReadAsStringAsync();
            goals.AddRange(JsonConvert.DeserializeObject<List<Goal>>(result.Result));
            
            return goals;
        }
        public List<Goal> GetOverdueGoals(string username)
        {
            string url = $"https://{IP}/goal/GetOverdieGoals?username={username}&dateString={DateTime.Now.ToShortDateString()}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            Task<string> result = response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Goal>>(result.Result);

        }
        #endregion

        #region File
        public List<FileMini> GetFilePreviews(string username)
        {
            string url = $"https://{IP}/file/GetFilePreviews?username={username}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            Task<string> result = response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<FileMini>>(result.Result);

        }
        public File DownloadFile(string guid)
        {
            string url = $"https://{IP}/file/DownloadFile?guid={guid}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            Task<string> result = response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<File>(result.Result);

        }
        #endregion

        #region Note
        public Note GetNote(string guid, string username)
        {
            string url = $"https://{IP}/user/GetUser?guid={guid}&username={username}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            Task<string> result = response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Note>(result.Result);

        }
        public List<NoteMini> GetNotePreviews(string username)
        {
            string url = $"https://{IP}/note/GetNotePreviews?username={username}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            Task<string> result = response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<NoteMini>>(result.Result);

        }
        #endregion

        #region Caldendar
        public Month GetMonth(string username, int monthOfYear)
        {
            string url = $"https://{IP}/file/DownloadFile?username={username}&monthOfYear={monthOfYear}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            Task<string> result = response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Month>(result.Result);

        }
        #endregion

        #endregion

    }
}