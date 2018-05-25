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

using Newtonsoft.Json;

using StudyApp.Assets.Controllers;

namespace StudyApp.Assets.Views {

    [Activity(Label = "CreateAccountActivity")]
    public class CreateAccountActivity : Activity {

        private UserController userController;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateAccount);
            userController = JsonConvert.DeserializeObject<UserController>(Intent.GetStringExtra("UserController"));

            Button createButton = FindViewById<Button>(Resource.Id.CreateButton);

            createButton.Click += CreateClick;
        }

        public void CreateClick(object sender, EventArgs args) {
            EditText userNameField = FindViewById<EditText>(Resource.Id.CreateAccount_UsernameField);
            EditText passwordField = FindViewById<EditText>(Resource.Id.CreateAccount_PasswordField);
            EditText emailField = FindViewById<EditText>(Resource.Id.CreateAccount_EmailField);
            EditText phoneNumberField = FindViewById<EditText>(Resource.Id.CreateAccount_PhoneNumberField);

            List<string> errors = new List<string>();

            
        }
    }
}