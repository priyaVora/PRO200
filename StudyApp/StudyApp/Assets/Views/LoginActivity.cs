using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using StudyApp.Assets.Controllers;
using StudyApp.Assets.Models;

namespace StudyApp.Assets.Views {

    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity {

        private UserController userController = new UserController();

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginPage);

            Button createAccountBtn = FindViewById<Button>(Resource.Id.CreateAccountButton);
            Button LoginBtn = FindViewById<Button>(Resource.Id.LoginButton);

            createAccountBtn.Click += CreateAccountClick;
            LoginBtn.Click += LoginClick;
        }

        public void LoginClick(object sender, EventArgs args) {
            string username = FindViewById<EditText>(Resource.Id.UserNameField).Text;
            string password = FindViewById<EditText>(Resource.Id.PasswordField).Text;
            UserAccount user = userController.LogIn(username, password);
            if (user == null) {

            } else {

            }
        }

        public void CreateAccountClick(object sender, EventArgs args) {

        }
    }
}