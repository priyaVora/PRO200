using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Provider;
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

        //public String GetRealPathFromURI(Android.Net.Uri uri)
        //{
        //    String[] projection = { MediaStore.Images.Media.Data};

        //    ICursor cursor = ManagedQuery(uri, projection, null, null, null);
        //    int column_index = cursor
        //            .GetColumnIndexOrThrow(MediaStore.Images.Media.DATA);
        //    cursor.MoveToFirst();
        //    return cursor.GetString(column_index);
        //}
        public String getRealPathFromURI(Android.Net.Uri uri)
        {
            ICursor cursor = ManagedQuery(uri, null, null, null, null);
            cursor.MoveToFirst();
            int idx = cursor.GetColumnIndex(MediaStore.Images.ImageColumns.Data);
            return cursor.GetString(idx);
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
        private string GetRealPathFromURI(Android.Net.Uri uri)
        {
            string doc_id = "";
            using (var c1 = ContentResolver.Query(uri, null, null, null, null))
            {
                c1.MoveToFirst();
                string document_id = c1.GetString(0);
                doc_id = document_id.Substring(document_id.LastIndexOf(":") + 1);
            }

            string path = null;

            // The projection contains the columns we want to return in our query.
            string selection = Android.Provider.MediaStore.Images.Media.InterfaceConsts.Id + " =? ";
            using (var cursor = ContentResolver.Query(Android.Provider.MediaStore.Images.Media.ExternalContentUri, null, selection, new string[] { doc_id }, null))
            {
                if (cursor == null) return path;
                var columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
                cursor.MoveToFirst();
                path = cursor.GetString(columnIndex);
            }
            return path;
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);
            if (requestCode == 0)
            {
                if (resultCode == Result.Ok)
                {
                    string uri = intent.DataString;
                    //intent variable has a Field named Data which is the complete URI for the file. 
                    Android.Net.Uri uris = Android.Net.Uri.FromParts(intent.Data.Scheme, intent.Data.SchemeSpecificPart, intent.Data.Fragment);
                    System.IO.Stream input = ContentResolver.OpenInputStream(intent.Data);


                    if(GetRealPathFromURI(uris)== null)
                    {

                        Toast.MakeText(this, "Path is still null", ToastLength.Short).Show();
                    }
                    Toast.MakeText(this, GetRealPathFromURI(uris), ToastLength.Short).Show();
                }
            }
        }


    }
} 