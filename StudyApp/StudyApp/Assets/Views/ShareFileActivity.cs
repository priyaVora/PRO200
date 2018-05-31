﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace StudyApp.Assets.Views {
    [Activity(Label = "ShareFileActivity")]
    public class ShareFileActivity : CommonActivity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            FrameLayout frame = FindViewById<FrameLayout>(Resource.Id.Common_FrameLayout);
            View calendarPage = LayoutInflater.Inflate(Resource.Layout.ShareFilePage, null);
            frame.AddView(calendarPage.FindViewById<LinearLayout>(Resource.Id.ShareFile_Layout));
            SetUpNavBar();




            // Create your application here
        }

        public void AddClick(object sender, EventArgs args) {
            
        }

        public void CancelClickLongPressOverDueGoal(object sender, EventArgs args) {

        }

        public void ConfirmClick(object sender, EventArgs args) {

        }
    }
}