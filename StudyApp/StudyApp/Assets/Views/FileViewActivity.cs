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

    [Activity(Label = "FileViewActivity")]
    public class FileViewActivity : CommonActivity {
        RecyclerView mRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        FileAdapter mAdapter;
        FileAlbum mFileAlbum;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            mFileAlbum = new FileAlbum();
            /*
             * This code is how to replace the placeholder layout that's part of the CommonLayout.
             */
            FrameLayout frame = FindViewById<FrameLayout>(Resource.Id.Common_FrameLayout);
            View file = LayoutInflater.Inflate(Resource.Layout.FilePage, null); // Replace the inside of this method call with your desired layout
            frame.AddView(file.FindViewById<LinearLayout>(Resource.Id.File_Layout));

            /*
             * This method needs to be called on the OnCreate method for any activities inheriting from CommonActivity,
             * since this is what initializes the navbar
             */

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
           
            // Plug in the linear layout manager:
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);


            mAdapter = new FileAdapter(mFileAlbum);
            mRecyclerView.SetAdapter(mAdapter);

            SetUpNavBar();

            
        }

        public void UploadClick(object sender, EventArgs args) {

        }

        public void LongPress(object sender, EventArgs args) {

        }
    }
}