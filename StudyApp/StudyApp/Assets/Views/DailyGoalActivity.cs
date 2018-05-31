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
using StudyApp.Assets.Models;

namespace StudyApp.Assets.Views {
    [Activity(Label = "DailyGoalActivity")]
    public class DailyGoalActivity : CommonActivity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Create your application here

            List<RecurringGoal> recurringGoals = goalController
                .GetUpcomingRecurringGoals(userController.CurrentUser.UserName).Where(g => g.Deadline == DateTime.Today)
                .ToList();
            List<NonRecurringGoal> nonRecurringGoals = goalController.GetUpcomingNonRecurringGoals(userController.CurrentUser.UserName).Where(g => g.Deadline == DateTime.Today)
                .ToList();
            RecurringGoal goal = new RecurringGoal();
            goal.

        }

        public void OnTouchEvent(object sender, EventArgs args) {

        }
    }
}