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
    [Activity(Label = "NoteEditActivity")]
    public class NoteEditActivity : Activity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }

        public void CancelClick(object sender, EventArgs args) {

        }

        public void SaveClick(object sender, EventArgs args) {

        }
    }
}