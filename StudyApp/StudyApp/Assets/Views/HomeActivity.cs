﻿using System;
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

        RecyclerView mRecyclerViewOverDueGoals;
        RecyclerView.LayoutManager mLayoutManager;
        GoalAdapter mAdapterOverDueGoals;
        GoalAlbum mGoalAlbum;


        RecyclerView mRecyclerViewRecurringGoals;
        RecyclerView.LayoutManager mLayoutManagerRecurringGoal;
        GoalAdapter mAdapterRecurringGoal;
        GoalAlbum mGoalAlbumRecurringGoal;


        RecyclerView mRecyclerViewOneTimeGoals;
        RecyclerView.LayoutManager mLayoutManagerTimeGoal;
        GoalAdapter mAdapterOneTimeGoal;
        GoalAlbum mGoalAlbumOneTimeGoal;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();

            SetGoalData();
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
            BindRecyclerViews();
            BindActivityWithLayoutManager();
            SetLayoutOnRecyclerViews();
            SetAdaptersForRecyclerViews();
            BindAdapterToRecyclerView();
            //List<Goal> overdue = goalController.GetOverdueGoals(userController.CurrentUser.UserName);
            //List<NonRecurringGoal> upcomingNonRecurring = goalController.GetUpcomingNonRecurringGoals(userController.CurrentUser.UserName);
            //List<RecurringGoal> upcomingRecurring = goalController.GetUpcomingRecurringGoals(userController.CurrentUser.UserName);
            // TODO: populate view with the previous lists of goals
        }




        public void SetGoalData()
        {
            mGoalAlbum = new GoalAlbum();
            mGoalAlbumRecurringGoal = new GoalAlbum();
            mGoalAlbumOneTimeGoal = new GoalAlbum();
        }

        public void BindRecyclerViews()
        {
            mRecyclerViewOverDueGoals = FindViewById<RecyclerView>(Resource.Id.recyclerViewOverDueGoals);
            mRecyclerViewRecurringGoals = FindViewById<RecyclerView>(Resource.Id.recyclerViewRecurringGoals);
            mRecyclerViewOneTimeGoals = FindViewById<RecyclerView>(Resource.Id.recyclerViewOneTimeGoal);
        }



        public void BindActivityWithLayoutManager()
        {
            mLayoutManager = new LinearLayoutManager(this);
            mLayoutManagerRecurringGoal = new LinearLayoutManager(this);
            mLayoutManagerTimeGoal = new LinearLayoutManager(this);
        }

        public void SetLayoutOnRecyclerViews()
        {
            mRecyclerViewOverDueGoals.SetLayoutManager(mLayoutManager);
            mRecyclerViewRecurringGoals.SetLayoutManager(mLayoutManagerRecurringGoal);
            mRecyclerViewOneTimeGoals.SetLayoutManager(mLayoutManagerTimeGoal);
        }
        public void SetAdaptersForRecyclerViews()
        {
            mAdapterOverDueGoals = new GoalAdapter(mGoalAlbum);
            mAdapterRecurringGoal = new GoalAdapter(mGoalAlbumRecurringGoal);
            mAdapterOneTimeGoal = new GoalAdapter(mGoalAlbumOneTimeGoal);
        }

        public void BindAdapterToRecyclerView()
        {
            mRecyclerViewOverDueGoals.SetAdapter(mAdapterOverDueGoals);
            mRecyclerViewRecurringGoals.SetAdapter(mAdapterRecurringGoal);
            mRecyclerViewOneTimeGoals.SetAdapter(mAdapterOneTimeGoal);
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