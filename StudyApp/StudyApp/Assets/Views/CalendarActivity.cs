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

using StudyApp.Assets.Views.PopUps;
using StudyApp.Assets.Controllers;

namespace StudyApp.Assets.Views {

    [Activity]
    public class CalendarActivity : Activity {

        private LayoutDialogFragment goalCreationPopup = LayoutDialogFragment.CreateInstance(Resource.Layout.GoalCreationPage);
        private UserController userController;
        private GoalController goalController;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CalendarPage);

            userController = JsonConvert.DeserializeObject<UserController>(Intent.GetStringExtra("UserController"));

            try {
                goalController = JsonConvert.DeserializeObject<GoalController>(Intent.GetStringExtra("GoalController"));
            } catch (ArgumentNullException) {
                goalController = new GoalController();
            }
            PageSetup();
        }

        private void PageSetup() {
            Button createGoalButton = FindViewById<Button>(Resource.Id.AddGoalButton);
            createGoalButton.Click += Add;
        }

        private void Add(object sender, EventArgs args) {
            //goalCreationPopup.Show(FragmentManager, "");
            SetContentView(Resource.Layout.GoalCreationPage);

            Button cancelButton = FindViewById<Button>(Resource.Id.GoalCreation_CancelButton);
            Button saveButton = FindViewById<Button>(Resource.Id.GoalCreation_SaveButton);

            cancelButton.Click += CancelPopup_Click;
            saveButton.Click += SavePopup_Click;
        }

        private void CancelPopup_Click(object sender, EventArgs args) {
            DismissPopUp();
        }

        private void SavePopup_Click(object sender, EventArgs args) {
            // TODO: Parse goal from fields and send it through the goalController
            DismissPopUp();
        }

        private void DismissPopUp() {
            SetContentView(Resource.Layout.CalendarPage);
            PageSetup();
        }

        private void ShowGoals(object sender, EventArgs args) {
            SetContentView(Resource.Layout.DailyGoals);
        }
    }
}