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
    class UserController
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
            UserAccount account = new UserAccount(userName: username );
        }

        public bool DoesUserExist(string username)
        {
            throw new NotImplementedException();
        }

        public UserAccount GetUser(string username)
        {
            throw new NotImplementedException();
        }

        public void AddPoints(string username, int pointsToAdd)
        {
            throw new NotImplementedException();
        }

        private string EncryptPassword(string password)
        {
            throw new NotImplementedException();
        }
    }
}