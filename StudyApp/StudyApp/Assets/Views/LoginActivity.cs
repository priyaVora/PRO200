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

namespace StudyApp.Assets.Views {
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginPage);

            Button createAccountBtn = FindViewById<Button>(Resource.Id.CreateAccountButton);
            Button LoginBtn = FindViewById<Button>(Resource.Id.LoginButton);

            createAccountBtn.Click += CreateAccountClick;
            LoginBtn.Click += LoginClick;

            // Create your application here
        }

        public void LoginClick(object sender, EventArgs args) {

        }

        public void CreateAccountClick(object sender, EventArgs args) {

        }
    }
}