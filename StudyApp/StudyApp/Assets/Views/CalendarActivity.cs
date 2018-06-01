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
            calendar.Click += ShowGoals;

            Button createGoalButton = FindViewById<Button>(Resource.Id.AddGoalButton);
            createGoalButton.Click += Add;
        }

        private void Add(object sender, EventArgs args) {
            GoToActivity(typeof(GoalCreationActivity), false);
        }

        private void ShowGoals(object sender, EventArgs args) {
            GoToActivity(typeof(DailyGoalActivity), false);
        }
    }
}