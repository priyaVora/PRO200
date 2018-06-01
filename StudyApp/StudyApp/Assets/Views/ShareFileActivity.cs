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
    [Activity(Label = "ShareFileActivity")]
    public class ShareFileActivity : CommonActivity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetUpLayout(Resource.Layout.ShareFilePage, Resource.Id.ShareFile_Layout);
            SetUpNavBar();

            Button addButton = FindViewById<Button>(Resource.Id.ShareFile_AddButton);
            Button confirmButton = FindViewById<Button>(Resource.Id.ShareFile_ConfirmButton);
            Button cancelButton = FindViewById<Button>(Resource.Id.ShareFile_CancelButton);

            addButton.Click += AddClick;
            confirmButton.Click += ConfirmClick;
            cancelButton.Click += CancelClick;

            // Create your application here
        }

        public void AddClick(object sender, EventArgs args) {
            String username = FindViewById<EditText>(Resource.Id.ShareFile_UsernameInput).Text;
        }

        public void CancelClick(object sender, EventArgs args) {
            OnBackPressed();
            Finish();
        }

        public void ConfirmClick(object sender, EventArgs args) {
            SubmitSharedList();
            OnBackPressed();
            Finish();
        }

        public void SubmitSharedList() {
        }
    }
}