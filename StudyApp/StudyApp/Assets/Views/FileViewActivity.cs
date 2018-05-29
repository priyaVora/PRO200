using System;
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

    [Activity(Label = "FileViewActivity")]
    public class FileViewActivity : Activity {

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FilePage);
            // Create your application here
        }

        public void UploadClick(object sender, EventArgs args) {

        }

        public void LongPress(object sender, EventArgs args) {

        }
    }
}