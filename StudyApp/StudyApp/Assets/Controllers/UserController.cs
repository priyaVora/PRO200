using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Security;
using StudyApp.Assets.Models;

namespace StudyApp.Assets.Controllers
{
    public class UserController
    {
        public UserAccount CurrentUser { get; set; }

        public UserAccount LogIn(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void LogOut()
        {
            throw new NotImplementedException();
        }

        public UserAccount CreateAccount(string username, string password, string email, string phoneNum = null)
        {
            UserAccount account = new UserAccount(userName: username, email: email, phoneNumber: phoneNum);
            ServerIOController serverIo = new ServerIOController();
            serverIo.CreateUser(account, password);
            return account;
        }

        public bool DoesUserExist(string username)
        {   
            ServerIOController serverIo = new ServerIOController();
            return serverIo.DoesUserExist(username);
        }

        public UserAccount GetUser(string username)
        {
            ServerIOController serverIo = new ServerIOController();
            return serverIo.GetUser(username);
        }

        public void AddPoints(string username, int pointsToAdd)
        {
            ServerIOController serverIo = new ServerIOController();
            serverIo.AddPoints(username,pointsToAdd);
        }
    }
}