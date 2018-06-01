using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using StudyApp.RecyclerView_Resources;

namespace StudyApp.Assets.Views {

    /// <summary>
    /// Instead of inheriting from Activity, this activity inherits from CommonActivity, which is a special activity that has the navbar incorporated into it
    /// </summary>
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : CommonActivity {

        RecyclerView mRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        GoalAdapter mAdapter;
        GoalAlbum mGoalAlbum;





        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();
            mGoalAlbum = new GoalAlbum();

            /*
             * This code is how to replace the placeholder layout that's part of the CommonLayout.
             */
            FrameLayout frame = FindViewById<FrameLayout>(Resource.Id.Common_FrameLayout);
            View home = LayoutInflater.Inflate(Resource.Layout.HomePage, null); // Replace the inside of this method call with your desired layout
            frame.AddView(home.FindViewById<LinearLayout>(Resource.Id.Home_Layout));

            /*
             * This method needs to be called on the OnCreate method for any activities inheriting from CommonActivity,
             * since this is what initializes the navbar
             */
            SetUpNavBar();

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerViewOverDueGoals);

            // Plug in the linear layout manager:
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);

            // Plug in my adapter:
            mAdapter = new GoalAdapter(mGoalAlbum);
            mRecyclerView.SetAdapter(mAdapter);

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

        #region Unimplemented events
        public void UsernameClick(object sender, EventArgs args) {

        }

        public void LongPressOverDueGoals(object sender, EventArgs args) {

        }
        #endregion
    }
}