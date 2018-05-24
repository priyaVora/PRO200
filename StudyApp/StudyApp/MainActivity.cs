﻿using Android.App;
using Android.Widget;
using Android.OS;

using StudyApp.Assets.Views;

namespace StudyApp {

    [Activity(Label = "StudyApp")]
    public class MainActivity : Activity {

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            //SetContentView(Resource.Layout.Main);
            StartActivity(typeof(LoginActivity));
        }
    }
}

