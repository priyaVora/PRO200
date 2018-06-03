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
using StudyApp.Assets.Controllers;
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
            ActionBar.Hide();
            mFileAlbum = new FileAlbum();
           // mCommonAlbum = new CommonAlbum();
            /*
             * This code is how to replace the placeholder layout that's part of the CommonLayout.
             */ 
            FrameLayout frame = FindViewById<FrameLayout>(Resource.Id.Common_FrameLayout);
            View file = LayoutInflater.Inflate(Resource.Layout.FilePage, null); // Replace the inside of this method call with your desired layout
            frame.AddView(file.FindViewById<LinearLayout>(Resource.Id.File_Layout));
            SetUpNavBar();
            

            Button AddFileBtn = FindViewById<Button>(Resource.Id.FilePage_AddFileButton);
            AddFileBtn.Click += UploadClick;


            /*
             * This method needs to be called on the OnCreate method for any activities inheriting from CommonActivity,
             * since this is what initializes the navbar
             */

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
           
            // Plug in the linear layout manager:
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);

            /*
             * 
             * Setting temporary user for testing purposes until login works.
             * **/

            this.userController.CurrentUser = new Models.UserAccount();
            this.userController.CurrentUser.UserName = "prvora89";
            

            // Plug in my adapter:
            mAdapter = new FileAdapter(mFileAlbum, this, this.userController.CurrentUser);
            mRecyclerView.SetAdapter(mAdapter);


         

        }

        public void UploadClick(object sender, EventArgs args) {
            Toast.MakeText(this, "Add File", ToastLength.Short).Show();
            FileController fileController = new FileController();
            
            //fileController.UploadFile(new File());
        }

        public void LongPress(object sender, EventArgs args) {
            Console.WriteLine("File was long pressed");
        }
    }
}