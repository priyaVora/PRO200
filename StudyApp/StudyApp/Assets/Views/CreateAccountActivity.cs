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
    [Activity(Label = "CreateAccountActivity")]
    public class CreateAccountActivity : Activity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateAccount);

            Button createButton = FindViewById<Button>(Resource.Id.CreateButton);

            createButton.Click += CreateClick;

            // Create your application here
        }

        public void CreateClick(object sender, EventArgs args) {

        }
    }
}