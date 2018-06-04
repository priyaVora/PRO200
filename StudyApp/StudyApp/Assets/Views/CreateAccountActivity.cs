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
using StudyApp.Assets.Views.PopUps;
using StudyApp.Assets.Models;

namespace StudyApp.Assets.Views {

    [Activity(Label = "CreateAccountActivity")]
    public class CreateAccountActivity : Activity {

        private UserController userController;
        private EditText userNameField;
        private EditText passwordField;
        private EditText emailField;
        private EditText phoneNumberField;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateAccount);
            userController = JsonConvert.DeserializeObject<UserController>(Intent.GetStringExtra("UserController"));

            Button createButton = FindViewById<Button>(Resource.Id.CreateButton);

            userNameField = FindViewById<EditText>(Resource.Id.CreateAccount_UsernameField);
            passwordField = FindViewById<EditText>(Resource.Id.CreateAccount_PasswordField);
            emailField = FindViewById<EditText>(Resource.Id.CreateAccount_EmailField);
            phoneNumberField = FindViewById<EditText>(Resource.Id.CreateAccount_PhoneNumberField);

            createButton.Click += CreateClick;
        }

        public void CreateClick(object sender, EventArgs args) {
            Dictionary<string, IEnumerable<string>> errors = new Dictionary<string, IEnumerable<string>> {
                { "Username errors:", UsernameChecks(userNameField.Text) },
                { "Password errors:", PasswordChecks(passwordField.Text) },
                { "Email errors:", EmailChecks(emailField.Text) },
                { "Phone number errors:", PhoneNumberChecks(phoneNumberField.Text) }
            };

            Dictionary<string, IEnumerable<string>> errorsCaught =
                errors.Where(p => p.Value.Any()).ToDictionary(p => p.Key, p => p.Value);

            if (errorsCaught.Count > 0) {
                string errorMessage = String.Empty;
                foreach (KeyValuePair<string, IEnumerable<string>> errorPair in errorsCaught) {
                    errorMessage += errorPair.Key;
                    foreach (string message in errorPair.Value) {
                        errorMessage += $"\n\t\t{message}";
                    }

                    errorMessage += "\n\n";
                }

                StringMessageDialogFragment dialog = StringMessageDialogFragment.CreateInstance(errorMessage);
                dialog.Show(FragmentManager, "Invalid information");

                userNameField.Text = String.Empty;
                passwordField.Text = String.Empty;
                emailField.Text = String.Empty;
                phoneNumberField.Text = String.Empty;
            } else {
                UserAccount user = userController.CreateAccount(userNameField.Text, passwordField.Text, emailField.Text, phoneNumberField.Text);
                userController.LogIn(user.UserName, passwordField.Text);
                Intent intent = new Intent(this, typeof(HomeActivity));
                intent.PutExtra("UserController", JsonConvert.SerializeObject(userController));
                StartActivity(intent);
                Finish();
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
                errors.Add("Invalid email format.");
            }

            return errors;
        }

        private IEnumerable<string> PhoneNumberChecks(string phoneNum) {
            List<string> errors = new List<string>();
            if (!String.IsNullOrWhiteSpace(phoneNum)) {
                Regex phoneRegex = new Regex(@"(?:\d{3}-{0,1}){2}\d{4}");
                if (phoneRegex.IsMatch(phoneNum)) {
                    errors.Add("Invalid phone number format.");
                }
            }

            return errors;
        }
        #endregion
    }
}