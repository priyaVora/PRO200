using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Icu.Util;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Newtonsoft.Json;

using StudyApp.Assets.Views.PopUps;
using StudyApp.Assets.Controllers;

namespace StudyApp.Assets.Views {

    [Activity]
    public class CalendarActivity : CommonActivity {

        //private LayoutDialogFragment goalCreationPopup = LayoutDialogFragment.CreateInstance(Resource.Layout.GoalCreationPage);

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            PageSetup();
        }

        private void PageSetup() {
            SetUpPage();
            FrameLayout frame = FindViewById<FrameLayout>(Resource.Id.Common_FrameLayout);
            View calendarPage = LayoutInflater.Inflate(Resource.Layout.CalendarPage, null); 
            frame.AddView(calendarPage.FindViewById<LinearLayout>(Resource.Id.Calendar_Layout));
            SetUpNavBar();

            CalendarView calendar = FindViewById<CalendarView>(Resource.Id.calendarView1);
            calendar.Click += (sender, args) => { };

            Button createGoalButton = FindViewById<Button>(Resource.Id.AddGoalButton);
            createGoalButton.Click += Add;

            Button homeButton = FindViewById<Button>(Resource.Id.NavBar_HomeButton);
            Button calendarButton = FindViewById<Button>(Resource.Id.NavBar_CalendarButton);
            Button filesButton = FindViewById<Button>(Resource.Id.NavBar_FilesButton);
            Button notesButton = FindViewById<Button>(Resource.Id.NavBar_NotesButton);

            homeButton.Click += (sender, args) => GoToActivity(typeof(HomeActivity), true);
            calendarButton.Click += (sender, args) => GoToActivity(typeof(CalendarActivity), false);
            filesButton.Click += (sender, args) => GoToActivity(typeof(FileViewActivity), false);
            notesButton.Click += (sender, args) => GoToActivity(typeof(NoteActivity), false);
        }

        private void Add(object sender, EventArgs args) {
            //goalCreationPopup.Show(FragmentManager, "");
            GoalCreationPageSetup();
        }

        #region Create goal events
        private void GoalCreationPageSetup() {
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
            EditText taskName = FindViewById<EditText>(Resource.Id.NonRecurring_TaskNameField);
            EditText taskDescription = FindViewById<EditText>(Resource.Id.NonRecurring_DescriptionField);

            Switch goalSwitch = FindViewById<Switch>(Resource.Id.GoalCreation_GoalSwitch);

            DismissPopUp();
        }

        private void DismissPopUp() {
            PageSetup();
        }

        private void ShowGoals(object sender, EventArgs args) {
            SetContentView(Resource.Layout.DailyGoals);
        }
        #endregion
    }
}