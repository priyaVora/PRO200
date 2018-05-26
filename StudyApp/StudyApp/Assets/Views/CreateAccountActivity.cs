using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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

            Dictionary<string, IEnumerable<string>> errors = new Dictionary<string, IEnumerable<string>> {
                { "Username", UsernameChecks(userNameField.Text) },
                { "Password", PasswordChecks(passwordField.Text) },
                { "Email", EmailChecks(emailField.Text) },
                { "Phone Number", PhoneNumberChecks(phoneNumberField.Text) }
            };

            if (errors.Values.Any(i => i.Any())) {

            } else {
                // Go home
            }
        }

        #region Field error checking helpers
        private IEnumerable<string> UsernameChecks(string username) {
            List<string> errors = new List<string>();

            if (username.Length < 5 || username.Length > 32) {
                errors.Add("Must be between 5 and 32 characters long, inclusive.");
            } else if (userController.DoesUserExist(username)) { // Since a username is only valid if 5 <= length <= 32, this check only needs to happen if the username passes the length check
                errors.Add("Username is already taken.");
            }

            return errors;
        }

        private IEnumerable<string> PasswordChecks(string password) {
            List<string> errors = new List<string>();

            if (password.Length < 8 || password.Length > 32) {
                errors.Add("Must be between 8 and 32 characters long, inclusive.");
            }

            if (!password.Any(c => char.IsLetter(c))) {
                errors.Add("Must contain at least 1 letter.");
            }

            if (!password.Any(c => char.IsDigit(c))) {
                errors.Add("Must contain at least 1 number.");
            }

            if (!password.Any(c => char.IsUpper(c))) {
                errors.Add("Must contain at least 1 uppercase letter.");
            }

            if (password.Any(c => char.IsWhiteSpace(c))) {
                errors.Add("Cannot contain any whitespace.");
            }

            return errors;
        }

        private IEnumerable<string> EmailChecks(string email) {
            List<string> errors = new List<string>();

            Regex regex = new Regex(@".+?[@].+?\..+");
            if (!regex.IsMatch(email)) {
                errors.Add("Must be in the correct format (ex. test@provider.com)");
            }

            return errors;
        }

        private IEnumerable<string> PhoneNumberChecks(string phoneNum) {
            List<string> errors = new List<string>();
            if (!String.IsNullOrWhiteSpace(phoneNum)) {

            }

            return errors;
        }
        #endregion
    }
}