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

using Newtonsoft.Json;

using StudyApp.Assets.Controllers;
using StudyApp.Assets.Models;
using StudyApp.Assets.Views.PopUps;

namespace StudyApp.Assets.Views {

    [Activity]
    public class LoginActivity : Activity {

        private UserController userController = new UserController();

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginPage);
            ActionBar.Hide();

            Button createAccountBtn = FindViewById<Button>(Resource.Id.Login_CreateAccountButton);
            Button LoginBtn = FindViewById<Button>(Resource.Id.LoginButton);

            createAccountBtn.Click += CreateAccountClick;
            LoginBtn.Click += LoginClick;
        }

        public void LoginClick(object sender, EventArgs args) {
            //EditText usernameField = FindViewById<EditText>(Resource.Id.Login_UsernameField);
            //EditText passwordField = FindViewById<EditText>(Resource.Id.Login_PasswordField);

            //UserAccount user = userController.LogIn(usernameField.Text, passwordField.Text);
            //if (user == null) {
            //    StringMessageDialogFragment dialog = StringMessageDialogFragment.CreateInstance("Invalid username or password.");
            //    dialog.Show(FragmentManager, "Login Failed");
            //} else {
                Intent homeIntent = new Intent(this, typeof(HomeActivity));
                homeIntent.PutExtra("UserController", JsonConvert.SerializeObject(userController));
                StartActivity(homeIntent);
                Finish();
            //}
        }

        public void CreateAccountClick(object sender, EventArgs args) {
            Intent intent = new Intent(this, typeof(CreateAccountActivity));
            intent.PutExtra("UserController", JsonConvert.SerializeObject(userController));
            StartActivity(intent);
        }
    }
}