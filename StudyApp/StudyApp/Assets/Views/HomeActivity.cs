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
using StudyApp.Assets.Models;

namespace StudyApp.Assets.Views {

    [Activity(Label = "HomeActivity")]
    public class HomeActivity : Activity {

        private UserController userController;
        private GoalController goalController;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            /*
             * The UserController shouldn't have to be instantiated here, since that would mean that we would lose
             * the currently logged in user. Thus, it should always be in the intent coming into this activity.
             */
            userController = JsonConvert.DeserializeObject<UserController>(Intent.GetStringExtra("UserController"));

            try {
                goalController = JsonConvert.DeserializeObject<GoalController>(Intent.GetStringExtra("GoalController"));
            } catch (ArgumentNullException) {
                goalController = new GoalController();
            }

            //List<Goal> overdue = goalController.GetOverdueGoals(userController.CurrentUser.UserName);
            //List<NonRecurringGoal> upcomingNonRecurring = goalController.GetUpcomingNonRecurringGoals(userController.CurrentUser.UserName);
            //List<RecurringGoal> upcomingRecurring = goalController.GetUpcomingRecurringGoals(userController.CurrentUser.UserName);
            // TODO: populate view with the previous lists of goals
        }

        public void LogOut() {
            userController.LogOut();
            Intent intent = new Intent(this, typeof(LoginActivity));
            StartActivity(intent);
            Finish();
        }

        public void UsernameClick(object sender, EventArgs args) {

        }

        public void LongPressOverDueGoals(object sender, EventArgs args) {

        }
    }
}