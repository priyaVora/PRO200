using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using StudyApp.Assets.Controllers;
using StudyApp.Assets.Models;
using StudyApp.Assets.Views.PopUps;

namespace StudyApp.Assets.Views {

    [Activity(MainLauncher = true)]
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
            EditText usernameField = FindViewById<EditText>(Resource.Id.UserNameField);
            EditText passwordField = FindViewById<EditText>(Resource.Id.PasswordField);

            UserAccount user = userController.LogIn(usernameField.Text, passwordField.Text);
            if (user == null) {
                LoginFailedDialogFragment dialog = new LoginFailedDialogFragment();
                dialog.Show(FragmentManager, "Login Failed");
            } else {
                StartActivity(typeof(HomeActivity));
            }
        }

        public void CreateAccountClick(object sender, EventArgs args) {
            StartActivity(typeof(CreateAccountActivity));
        }
    }
}