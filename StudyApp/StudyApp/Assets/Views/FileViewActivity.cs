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
using StudyApp.Assets.Controllers;
using StudyApp.RecyclerView_Resources;

namespace StudyApp.Assets.Views
{

    [Activity(Label = "FileViewActivity")]
    public class FileViewActivity : CommonActivity
    {
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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //var file = await CrossFilePicker.Current.PickFile();

            //if (file != null)
            //{
            //    lbl.Text = file.FileName;
            //}
        }
        Intent intent;

        private void PickFile(string folder, string extension)
        {
            intent = new Intent(Intent.ActionGetContent);
            folder = "C:\\Users\\Priya\\Desktop\\FolderName";
            extension = ".txt";
            // intent.SetType(folder + "/" + extension);
            intent.SetType("image/*");
            intent.AddCategory(Intent.CategoryOpenable);
            
            //intent.SetType("file/*");
            
            intent.AddCategory(Intent.CategoryOpenable);
            try
            {
                StartActivityForResult(Intent.CreateChooser(intent, "Select a file"),
                          0);
            }
            catch (System.Exception exAct)
            {
                System.Diagnostics.Debug.Write(exAct);
            }
            StartActivityForResult(intent, 0);
        }

        public void UploadClick(object sender, EventArgs args)
        {
            Toast.MakeText(this, "Add File", ToastLength.Short).Show();
            FileController fileController = new FileController();

            //fileController.UploadFile(new File());
            PickFile(null, null);
        }

        public void LongPress(object sender, EventArgs args)
        {
            Console.WriteLine("File was long pressed");
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);
            //if (requestCode == 0)
            //{
            //    if (resultCode == Result.Ok)
            //    {
           
            string uri = intent.DataString;
                    //intent variable has a Field named Data which is the complete URI for the file. 
                    Android.Net.Uri uris = Android.Net.Uri.FromParts(intent.Data.Scheme, intent.Data.SchemeSpecificPart, intent.Data.Fragment);
                    System.IO.Stream input = ContentResolver.OpenInputStream(intent.Data);
            Toast.MakeText(this, input.ToString(), ToastLength.Short).Show();
            //related tasks
            //}
            //}
        }
    }
}