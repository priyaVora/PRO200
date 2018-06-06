using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using StudyApp.Assets.Models;
using StudyApp.RecyclerView_Resources;

namespace StudyApp.Assets.Views {
    [Activity(Label = "DailyGoalActivity")]
    public class DailyGoalActivity : CommonActivity
    {
        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManagerRecycler;
        private GoalAdapter mAdapter;
        private GoalAlbum mGoalAlbum;
        private DateTime date;
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            FrameLayout frame = FindViewById<FrameLayout>(Resource.Id.Common_FrameLayout);
            View calendarPage = LayoutInflater.Inflate(Resource.Layout.DailyGoals, null);
          
            frame.AddView(calendarPage.FindViewById<LinearLayout>(Resource.Id.DailyGoals_Layout));
            SetUpNavBar();

            // Create your application here
            date = JsonConvert.DeserializeObject<DateTime>(Intent.GetStringExtra("Date"));
            TextView dayLabel = FindViewById<TextView>(Resource.Id.DailyGoals_DayLabel);
            dayLabel.Text = date.Day.ToString();
            TextView dayofWeekLabel = FindViewById<TextView>(Resource.Id.DailyGoals_DayOfWeekLabel);
            dayofWeekLabel.Text = date.DayOfWeek.ToString();
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.DailyGoals_RecyclerView);
            mLayoutManagerRecycler = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManagerRecycler);
            SetGoalData();
            mAdapter = new GoalAdapter(mGoalAlbum, this);
            mRecyclerView.SetAdapter(mAdapter);

        }

        private void SetGoalData()
        {
            List<Goal> temp = new List<Goal>();

            goalController.GetUpcomingRecurringGoals(userController.CurrentUser.UserName).Where(g => g.Deadline == date).ToList().ForEach(temp.Add);
            goalController.GetUpcomingNonRecurringGoals(userController.CurrentUser.UserName).Where(g => g.Deadline == date).ToList().ForEach(temp.Add);

            mGoalAlbum = new GoalAlbum(temp);
        }

        public void OnTouchEvent(object sender, EventArgs args) {

        }
    }
}