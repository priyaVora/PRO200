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
using static Android.Widget.CalendarView;

using Newtonsoft.Json;

using StudyApp.Assets.Views.PopUps;
using StudyApp.Assets.Controllers;
using Android.Graphics;

namespace StudyApp.Assets.Views {

    [Activity]
    public class CalendarActivity : CommonActivity, IOnDateChangeListener {

        private CalendarView calendar;

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

            calendar = FindViewById<CalendarView>(Resource.Id.calendarView1);
            calendar.SetOnDateChangeListener(this);

            Button createGoalButton = FindViewById<Button>(Resource.Id.AddGoalButton);
            createGoalButton.Click += Add;
        }

        private void Add(object sender, EventArgs args) {
            GoToActivity(typeof(GoalCreationActivity), false);
        }

        public void OnSelectedDayChange(CalendarView view, int year, int month, int dayOfMonth) {
            GoToActivity(typeof(DailyGoalActivity), false, new KeyValuePair<string, string>("Date", JsonConvert.SerializeObject(new DateTime(view.Date))));
        }
    }
}